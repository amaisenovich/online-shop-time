using System;
using System.Linq;


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
        public string GetAndDeleteCurrency(Offers Offer)
        {
            string Result = null;
            if (Offer.Price.Contains("RUB"))
                Result = "RUB";
            if (Offer.Price.Contains("EUR"))
                Result = "EUR";
            if (Offer.Price.Contains("CNY"))
                Result = "CNY";
            if (Offer.Price.Contains("USD"))
                Result = "USD";
            Offer.Price = Offer.Price.Substring(0, Offer.Price.Length - 4);
            return Result;
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
        public void AddTagsToOffer(string OfferID, string TagsString)
        {
            this.AddTagsToOffer((from OfferRecords in Db.Offers where OfferRecords.OfferID == OfferID select OfferRecords).FirstOrDefault(), TagsString);
        }
        public void AddTagsToOffer(Offers Offer, string TagsString)
        {
            string[] InputedTags = TagsString.Split(' ', '#');
            foreach (string str in InputedTags)
            {
                if (str != "")
                {
                    Tags Tag = null;
                    Tag = (from TagsRecords in Db.Tags where TagsRecords.Name == str select TagsRecords).FirstOrDefault();
                    if (Tag == null)
                    {
                        Tag = new Tags();
                        Tag.TagID = Guid.NewGuid().ToString();
                        Tag.Name = str;
                    }
                    Tag.Offers.Add(Offer);
                    Offer.Tags.Add(Tag);
                }
            }
            Db.SaveChanges();
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
        public string AndNewOrModify(Offers Offer, string UserName)
        {
            string Result = null;
            int count = (from OfferRecords in Db.Offers where OfferRecords.OfferID == Offer.OfferID select OfferRecords).Count();
            if (count == 0)
            {
                Result = AddNewOffer(Offer, UserName);
            }
            else
                Result = ModifyExistedOffer(Offer);
            return Result;
        }
        public string ModifyExistedOffer(Offers EditedOffer)
        {
            Offers OldOne = (from OfferRecords in Db.Offers where OfferRecords.OfferID == EditedOffer.OfferID select OfferRecords).FirstOrDefault();
            OldOne.Name = EditedOffer.Name;
            OldOne.Description = EditedOffer.Description;
            OldOne.Price = EditedOffer.Price;
            OldOne.Photo1URL = EditedOffer.Photo1URL;
            OldOne.Photo2URL = EditedOffer.Photo2URL;
            OldOne.Photo3URL = EditedOffer.Photo3URL;
            OldOne.Photo4URL = EditedOffer.Photo4URL;
            Db.SaveChanges();
            return OldOne.OfferID;
        }
        public string AddNewOffer(Offers newOffer, string UserName)
        {
            newOffer = this.CompleteOfferWithData(newOffer, UserName);
            Db.Offers.Add(newOffer);
            Db.SaveChanges();
            return newOffer.OfferID;
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
            DeleteOfferTags(OfferID);
            Db.Offers.Remove(RemoveOffer);
            Db.SaveChanges();
        }
        public void DeleteOfferTags(string OfferID)
        {
            Offers Offer = (from OffersRecords in Db.Offers where OffersRecords.OfferID == OfferID select OffersRecords).FirstOrDefault();
            ICollection<Tags> OfferTags = (from OfferRecords in Db.Offers where OfferRecords.OfferID == OfferID select OfferRecords.Tags).FirstOrDefault();
            foreach (Tags Tag in OfferTags)
            {
                Offer.Tags.Remove(Tag);
                Tag.Offers.Remove(Offer);
            }
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