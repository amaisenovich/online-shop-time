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
        }
    }
}