using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

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
        public void OrderOffer(string OfferID, string UserName)
        {
            WorkWithUsers WWU = new WorkWithUsers();
            Orders Order = new Orders();
            Order.ClientID = WWU.GetUserByName(UserName).UserID;
            Order.DateAndTime = DateTime.Now;
            Order.OfferID = OfferID;
            Order.OrderID = Guid.NewGuid().ToString();
            Db.Orders.Add(Order);
            Db.SaveChanges();
        }
        public IQueryable<Orders> GetUserOrders(string UserID)
        {
            return from OrdersRecords in Db.Orders where OrdersRecords.ClientID == UserID orderby OrdersRecords.DateAndTime select OrdersRecords;
        }
        public void CheckUserOrders(string UserID)
        {
            IQueryable<Orders> UserOrders = from OrderRecs in Db.Orders where OrderRecs.ClientID == UserID select OrderRecs;
            foreach (Orders Ord in UserOrders)
            {
                if (Ord.OrderStatus == "Await")
                {
                    TimeSpan age = DateTime.Now.Subtract(Ord.DateAndTime.GetValueOrDefault());
                    if (age.Days >= 1)
                    {
                        this.DelereOrder(Ord);
                    }
                }
            }
        }
        public void DelereOrder(Orders Order)
        {
            Db.Orders.Remove(Order);
            Db.SaveChanges();
        }
        public void DelereOrder(string OrderID)
        {
            Db.Orders.Remove((from OrdRecs in Db.Orders where OrdRecs.OrderID == OrderID select OrdRecs).FirstOrDefault());
            Db.SaveChanges();
        }
    }
}