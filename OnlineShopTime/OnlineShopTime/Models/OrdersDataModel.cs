using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace OnlineShopTime.Models
{
    public class OrdersDataModel
    {
        public string DataToShow;

        public string ActiveUserID;

        public IQueryable<Orders> Data;

        private bool ChechDayPeriod; 
        public OrdersDataModel(string UserID)
        {
            DataToShow = "ActiveOrders";
            ActiveUserID = UserID;
            ChechDayPeriod = false;
        }
        public void SetActiveOrders()
        {
            WorkWithOrders WWOR = new WorkWithOrders();
            DataToShow = "ActiveOrders";
            if (!ChechDayPeriod)
            {
                WWOR.CheckDayPeriod(ActiveUserID);
                ChechDayPeriod = true;
            }
            Data = WWOR.GetUserActiveOrders(ActiveUserID);
        }
        public void SetOrdersHistory()
        {
            WorkWithOrders WWOR = new WorkWithOrders();
            DataToShow = "OrdersHistory";
            Data = WWOR.GetUserOrdersHistory(ActiveUserID);
        }
        public void SetUserIncomingOrders()
        {
            WorkWithOrders WWOR = new WorkWithOrders();
            DataToShow = "IncomingOrders";
            Data = WWOR.GetUserIncomingOrders(ActiveUserID);
        }
    }
}