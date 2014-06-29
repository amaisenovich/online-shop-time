using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopTime.Models
{
    public class WorkWithComments
    {
        ShopDBEntities Db;
        public WorkWithComments()
        {
            Db = new ShopDBEntities();
        }
        public IQueryable<Comments> GetUserComments(string UserID)
        {
            Users User = (from UsersRecords in Db.Users where UsersRecords.UserID == UserID select UsersRecords).FirstOrDefault();
            return GetUserComments(User);
        }
        public IQueryable<Comments> GetUserComments(Users User)
        {
            IQueryable<Comments> UserComments = from CommentsRecords in Db.Comments orderby CommentsRecords.DateAndTime where CommentsRecords.PostedBy == User.UserID select CommentsRecords;
            return UserComments;
        }
    }
}