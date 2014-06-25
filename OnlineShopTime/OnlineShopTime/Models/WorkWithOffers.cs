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
        public IQueryable<Offers> GetTopOffers()
        {
            var OfferRate = from RateRec in Db.OfferRaiting 
                            group RateRec.Raiting by RateRec.OfferID into RecGroup 
                            select new { Key = RecGroup.Key, Rate = RecGroup.ToList().Sum() / RecGroup.Count() };

            IQueryable<Offers> TopOfferrs = (from OfferRec in Db.Offers 
                      join RateRec in OfferRate on OfferRec.OfferID equals RateRec.Key 
                      orderby RateRec.Rate descending 
                      select OfferRec).Take(12);

            return TopOfferrs;
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
        public void AndNewOrModify(Offers Offer, string UserName)
        {
            int count = (from OfferRecords in Db.Offers where OfferRecords.OfferID == Offer.OfferID select OfferRecords).Count();
            if (count == 0)
            {
                AddNewOffer(Offer, UserName);
            }
            else
                ModifyExistedOffer(Offer);
        }
        public void ModifyExistedOffer(Offers EditedOffer)
        { 
            Offers OldOne = (from OfferRecords in Db.Offers where OfferRecords.OfferID == EditedOffer.OfferID select OfferRecords).FirstOrDefault();
            OldOne.Name = EditedOffer.Name;
            OldOne.Description = EditedOffer.Description;
            OldOne.Price = OldOne.Price;
            Db.SaveChanges();
        }
        public void AddNewOffer(Offers newOffer, string UserName)
        {
            newOffer = this.CompleteOfferWithData(newOffer, UserName);
            Db.Offers.Add(newOffer);
            Db.SaveChanges();
        }
        public IQueryable<Offers> GetUserOffers(String UserID, int PageNumber = 1)
        {
            return (from OffersRecords in Db.Offers orderby OffersRecords.DateAndTime descending where OffersRecords.OfferedBy == UserID select OffersRecords).Skip((PageNumber - 1) * 20).Take(20);
        }

        public void DeleteOffer(string OfferID)
        {
            Offers RemoveOffer = (from OfferRecords in Db.Offers where OfferRecords.OfferID == OfferID select OfferRecords).FirstOrDefault();
            DeleteOfferRaiting(OfferID);
            DeleteOfferComments(OfferID);
            Db.Offers.Remove(RemoveOffer);
            Db.SaveChanges();
        }
        private void DeleteOfferComments(string OfferID)
        {
            IQueryable<Comments> Coments = from ComentRec in Db.Comments where ComentRec.OfferID == OfferID select ComentRec;
            foreach (Comments Coment in Coments)
                Db.Comments.Remove(Coment);
            Db.SaveChanges();
        }
        private void DeleteOfferRaiting(string OfferID)
        {
            IQueryable<OfferRaiting> OfferRate = from RateRec in Db.OfferRaiting where RateRec.OfferID == OfferID select RateRec;
            foreach (OfferRaiting OR in OfferRate)
                Db.OfferRaiting.Remove(OR);
            Db.SaveChanges();
        }
    }
}