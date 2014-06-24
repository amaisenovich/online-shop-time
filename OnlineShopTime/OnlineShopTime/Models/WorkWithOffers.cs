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

        public WorkWithOffers()
        {
            Db = new ShopDBEntities();
        }
        public string GetActiveUserID(string UserName)
        {
            return (from rec in Db.Users where rec.UserName == UserName select rec.UserID).FirstOrDefault();
        }
        public void GetTopOffers()
        {
            //Empty for now.
        }
        public Offers CompleteOfferWithData(Offers newOffer, String UserName)
        {
            String UserID = GetActiveUserID(UserName);
            newOffer.Users = (from rec in Db.Users where rec.UserID == UserID select rec).FirstOrDefault();
            newOffer.DateAndTime = DateTime.Now;
            newOffer.OfferedBy = UserID;
            newOffer.OfferID = Guid.NewGuid().ToString();
            return newOffer;
        }
        public IQueryable<Offers> GetNewOffers()
        {
            return (from OfferRecord in Db.Offers orderby OfferRecord.DateAndTime descending select OfferRecord).Take(12);
        }
        public Offers GetOfferByID(String OfferID)
        {   
            return (from OfferRecods in Db.Offers where OfferRecods.OfferID == OfferID select OfferRecods).FirstOrDefault();
        }
        public void AddNewOffer(Offers newOffer)
        {
            Db.Offers.Add(newOffer);
            Db.SaveChanges();
        }
        public IQueryable<Offers> GetUserOffers(String UserID, int PageNumber = 1)
        {
            return (from OffersRecords in Db.Offers orderby OffersRecords.DateAndTime descending where OffersRecords.OfferedBy == UserID select OffersRecords).Skip((PageNumber - 1) * 20).Take(20);
        }
    }
}