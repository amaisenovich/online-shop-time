using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopTime.Models
{
    public class WorkWithUsers
    {
        ShopDBEntities db;
        public WorkWithUsers()
        {
            db = new ShopDBEntities();
        }
        public int[] GetUserRaiting(string Id)
        {
            //Returns array. First element - number of likes, second - number of dislikes.
            int likes = 0;
            int dislikes = 0;
            var UserRaitingRecords = from Record in db.UserRaiting where Record.UserID == Id select Record;
            foreach (var Record in UserRaitingRecords)
                if (Record.Rating == -1)
                {
                    dislikes++;
                }
                else
                    likes++;
            return new int[2] { likes, dislikes };
        }
        public IQueryable<Users> GetTopUsers()
        {
            //Returns IQueryable of Users arranged by (likes - dislikes). Returns only top 10 records.
            var UserRaitingArranged = from RaitingRecords in db.UserRaiting
                                      group RaitingRecords by RaitingRecords.UserID
                                          into ResultTable
                                          select new { Key = ResultTable.Key, Raiting = ResultTable.Sum(value => value.Rating) };
            IQueryable<Users> TopUser = (from UserRecord in db.Users
                                        join EachRecord in UserRaitingArranged on UserRecord.UserID equals EachRecord.Key
                                        orderby EachRecord.Raiting descending
                                        select UserRecord).Take(10);
            return TopUser;
        }
    }
}