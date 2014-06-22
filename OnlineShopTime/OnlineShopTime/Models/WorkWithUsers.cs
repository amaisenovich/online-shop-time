using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopTime.Models
{
    public class WorkWithUsers
    {
        ShopDBEntities db;
        public int[] GetUserRaiting(string Id)
        {
            //Returns array. First element - number of likes, second - number of dislikes.
            db = new ShopDBEntities();
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
            //Returns IQueryable of Users arranged by (likes - dislikes). Returns only top 12 records.
            db = new ShopDBEntities();
            var UserRaitingArranged = from RaitingRecords in db.UserRaiting
                                      group RaitingRecords by RaitingRecords.UserID
                                          into ResultTable
                                          select new { Key = ResultTable.Key, Raiting = ResultTable.Sum(value => value.Rating) };
            IQueryable<Users> TopUser = (from UserRecord in db.Users
                                        join EachRecord in UserRaitingArranged on UserRecord.UserID equals EachRecord.Key
                                        orderby EachRecord.Raiting descending
                                        select UserRecord).Take(12);
            return TopUser;
        }
        public Users GetUserByName(string UserName)
        {
            db = new ShopDBEntities();
            return (from UserRecord in db.Users where UserRecord.UserName == UserName select UserRecord).FirstOrDefault();
        }
    }
}