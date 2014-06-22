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

        string UserName; //For god know purpose.

        public WorkWithOffers(string UserName)
        {
            this.UserName = UserName;
            GetActiveUserID(UserName);
        }
        public void GetActiveUserID(string UserName)
        {
            Db = new ShopDBEntities();
            UserID = (from rec in Db.Users where rec.UserName == UserName select rec.UserID).FirstOrDefault();
        }
        public void GetTopOffers()
        {
            //Empty for now.
        }
        private Offers CompleteOfferWithData(Offers newOffer)
        {
            //Fill model with data, which user doesn't input.
            Db = new ShopDBEntities();
            newOffer.Users = (from rec in Db.Users where rec.UserID == UserID select rec).FirstOrDefault();
            newOffer.DateAndTime = DateTime.Now;
            newOffer.OfferedBy = UserID;
            newOffer.OfferID = Guid.NewGuid().ToString();
            return newOffer;
        }
        public IQueryable<Offers> GetNewOffers()
        {
            //Returns 12 latest Offers
            Db = new ShopDBEntities();
            return (from OfferRecord in Db.Offers orderby OfferRecord.DateAndTime descending select OfferRecord).Take(12);
        }
        public void AddNewOffer(Offers newOffer)
        {
            //Add new offer to DB.
            CompleteOfferWithData(newOffer);
            Db = new ShopDBEntities();
            Db.Offers.Add(newOffer);
            Db.SaveChanges();
        }
    }
}