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
            IQueryable<Orders> UserOrders = from FiltredOrders in
                                                (from OrdersRec in Db.Orders where OrdersRec.OfferID == OfferID select OrdersRec)
                                            where FiltredOrders.ClientID == UserID
                                            select FiltredOrders;
            return IsOfferOrdered(UserOrders);
        }
        private bool IsOfferOrdered(IQueryable<Orders> UserOrders)
        {
            if (UserOrders.Count() == 0)
            {
                return false;
            }
            else
            {
                Orders Order = UserOrders.FirstOrDefault();
                if (Order.OrderStatus == "Completed")
                {
                    return false;
                }
                else
                    return true;
            }
        }
        private void AddToOrders(string OfferID, string UserID)
        {
            Orders Order = new Orders();
            Order.ClientID = UserID;
            Order.DateAndTime = DateTime.Now;
            Order.OfferID = OfferID;
            Order.OrderID = Guid.NewGuid().ToString();
            Order.OrderStatus = "Await Confirmation";
            Db.Orders.Add(Order);
            Db.SaveChanges();
        }
        private void ReorderOffer(Orders Order)
        {
            Order.DateAndTime = DateTime.Now;
            Order.OrderStatus = "Await Confirmation";
            Db.SaveChanges();
        }
        public void OrderOffer(string OfferID, string UserName)
        {
            WorkWithUsers WWU = new WorkWithUsers();
            string UserID = WWU.GetUserByName(UserName).UserID;

            IQueryable<Orders> UserOrders = from FiltredOrders in
                                                (from OrdersRec in Db.Orders where OrdersRec.OfferID == OfferID select OrdersRec)
                                            where FiltredOrders.ClientID == UserID
                                            select FiltredOrders;
            if (UserOrders.Count() == 0)
            {
                AddToOrders(OfferID, UserID);
            }   
            else
            {
                if (UserOrders.FirstOrDefault().OrderStatus == "Completed")
                {
                    ReorderOffer(UserOrders.FirstOrDefault());
                }
                else
                    AddToOrders(OfferID, UserID);
            }
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
        public bool IsDayDone(Orders Ord)
        {
            TimeSpan age = DateTime.Now.Subtract(Ord.DateAndTime.GetValueOrDefault());
            if (age.Milliseconds >= 1)
            {
                return true;
            }
            return false;
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
        public void DeleteOrder(string OrderID)
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