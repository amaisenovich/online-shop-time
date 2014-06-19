using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopTime.Models;
using Microsoft.AspNet.Identity;

namespace OnlineShopTime.Models
{
    public class WorkWithOffers
    {
        ShopDBEntities db;
        Guid userID;
        public WorkWithOffers()
        {
            db = new ShopDBEntities();
        }
        public void GetActiveUserID()
        {
            //userID = from rec in db.Users where rec.UserName == User.Identity.GetUserName() select rec.UserID;
        }
        public void GetTopOffers()
        {
        }
    }
}