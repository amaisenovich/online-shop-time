using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopTime.Models
{
    public class WorkWithOrders
    {
        ShopDBEntities Db;
        public WorkWithOrders()
        {
            Db = new ShopDBEntities();
        }
        public bool UserHasThisOfferOrdered(string UserID, string OfferID)
        {
            int count = (from FiltredOrders in
                             (from OrdersRec in Db.Orders where OrdersRec.OfferID == OfferID select OrdersRec)
                         where FiltredOrders.ClientID == UserID
                         select FiltredOrders).Count();
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}