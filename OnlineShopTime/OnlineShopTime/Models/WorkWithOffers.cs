using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopTime.Models;
using Microsoft.AspNet.Identity;
using System.Web.Security;


namespace OnlineShopTime.Models
{
    public class WorkWithOffers
    {
        ShopDBEntities Db;
        string UserID;
        public WorkWithOffers(string UserName)
        {
            Db = new ShopDBEntities();
            GetActiveUserID(UserName);
        }
        public void GetActiveUserID(string UserName)
        {
            UserID = (from rec in Db.Users where rec.UserName == UserName select rec.UserID).FirstOrDefault();
            Offers of = (from rec in Db.Offers select rec).FirstOrDefault();
            if (of.Name == "LOL") { }
        }
        public void GetTopOffers()
        {
        }
        public void AddNewOffer(Offers newOffer)
        {
            Users user = (from rec in Db.Users where rec.UserID == UserID select rec).FirstOrDefault();
            newOffer.DateAndTime = DateTime.Now;
            newOffer.OfferedBy = UserID;
            newOffer.Users = user;
            newOffer.OfferID = Guid.NewGuid().ToString();
            Db.Offers.Add(newOffer);
            //FINALY BLYAT'
            Db.SaveChanges();
        }
    }
}