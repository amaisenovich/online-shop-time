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
            Order.OrderStatus = "Await Confirmation";
            Db.Orders.Add(Order);
            Db.SaveChanges();
        }
        public IQueryable<Orders> GetUserIncomingOrders(string UserID)
        {
            IQueryable<Orders> UserOrders = from OrdersRecords in Db.Orders where OrdersRecords.Offers.OfferedBy == UserID select OrdersRecords;
            return (from EachRecord in UserOrders orderby EachRecord.DateAndTime select EachRecord).Where(item => (item.OrderStatus == "Active") || (item.OrderStatus == "Await Confirmation"));
        }
        public IQueryable<Orders> GetUserOrdersHistory(string UserID)
        {
            return from OrdersRecords in Db.Orders where OrdersRecords.ClientID == UserID where OrdersRecords.OrderStatus == "Completed" orderby OrdersRecords.DateAndTime select OrdersRecords;
        }
        public IQueryable<Orders> GetUserActiveOrders(string UserID)
        {
            IQueryable<Orders> UserOrders = (from OrdersRecords in Db.Orders orderby OrdersRecords.DateAndTime select OrdersRecords).Where(item => (item.OrderStatus == "Active") || (item.OrderStatus == "Await Confirmation"));
            return from ItemRecords in UserOrders where ItemRecords.ClientID == UserID select ItemRecords;
        }
        public void CheckUserOrders(string UserID)
        {
            IQueryable<Orders> UserOrders = from OrderRecs in Db.Orders where OrderRecs.ClientID == UserID select OrderRecs;
            foreach (Orders Ord in UserOrders)
            {
                if (Ord.OrderStatus == "Await Confirmation")
                {
                    TimeSpan age = DateTime.Now.Subtract(Ord.DateAndTime.GetValueOrDefault());
                    if (age.Days >= 1)
                    {
                        this.DeleteOrder(Ord);
                    }
                }
            }
        }
        public void DenyOrder(string OrderID)
        {
            Orders Order = (from OrderRecords in Db.Orders where OrderRecords.OrderID == OrderID select OrderRecords).FirstOrDefault();
            Db.Orders.Remove(Order);
            Db.SaveChanges();
        }
        public void ApplyOrder(string OrderID)
        {
            Orders Order = (from OrderRecords in Db.Orders where OrderRecords.OrderID == OrderID select OrderRecords).FirstOrDefault();
            Order.OrderStatus = "Active";
            Db.SaveChanges();
        }
        public void DeleteOrder(Orders Order)
        {
            Db.Orders.Remove(Order);
            Db.SaveChanges();
        }
        public void DelereOrder(string OrderID)
        {
            Db.Orders.Remove((from OrdRecs in Db.Orders where OrdRecs.OrderID == OrderID select OrdRecs).FirstOrDefault());
            Db.SaveChanges();
        }
        public void CompleteTrade(string OrderID)
        {
            Orders Order = (from OrderRecs in Db.Orders where OrderRecs.OrderID == OrderID select OrderRecs).FirstOrDefault();
            Order.OrderStatus = "Completed";
            Db.SaveChanges();
        }
    }
}