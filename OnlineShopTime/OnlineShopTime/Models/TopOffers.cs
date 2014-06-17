using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineShopTime.Models;

namespace OnlineShopTime.Models
{
    public class TopOffers
    {
        ShopDBEntities db;
        public TopOffers()
        {
            db = new ShopDBEntities();
            GetTopOffers();
        }
        public void GetTopOffers()
        {
            var query = from offer in db.Offers
                        join offerID in
                            (from rate in db.OfferRaiting
                             group rate.Raiting by rate.OfferID into g
                             orderby (g.ToList().Sum() / g.Count()) descending
                             select g) on offer.OfferID equals offerID.Key orderby offerID.ToList().Sum() / offerID.Count() descending
                        select new { Offer = offer, Raiting = offerID.ToList().Sum() / offerID.Count()};

            foreach (var a in query)
            {

            }
        }
    }
}