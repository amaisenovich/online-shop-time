using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineShopTime.Models;

namespace OnlineShopTime.Models
{
    public class TopOffers
    {
        //Offers 
        public TopOffers()
        {
            GetTopOffers();
        }
        public void GetTopOffers()
        {
            ShopDBEntities db = new ShopDBEntities();
                        
        }
    }
}