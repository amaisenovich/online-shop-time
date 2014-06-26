using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopTime.Models;

namespace OnlineShopTime.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult OrderConfirm(string OfferID)
        {
            if (OfferID == null)
                OfferID = (string)Session["OfferID"];
            Session["OfferID"] = OfferID;
            WorkWithOffers WWO = new WorkWithOffers();
            return View(WWO.GetOfferByID(OfferID));
        }

        public ActionResult OrderOffer(string OfferID)
        {
            WorkWithOrders WWOR = new WorkWithOrders();
            WWOR.OrderOffer(OfferID, User.Identity.Name);
            return RedirectToAction("TabClick", "Home", new { TabID = 3 });
        }
        public ActionResult ShowOrders(string UserID)
        {
            OrdersDataModel ViewData = (OrdersDataModel)Session["ViewData"];

            CheckUserOrders(UserID);

            if (UserID == null)
            {
                WorkWithUsers WWU = new WorkWithUsers();
                UserID = WWU.GetUserByName(User.Identity.Name).UserID;
            }

            if (ViewData == null)
            {
                ViewData = new OrdersDataModel(UserID);
                ViewData.SetActiveOrders();
            }

            if (ViewData.ActiveUserID == null)
            {
                ViewData.ActiveUserID = UserID;
            }

            Session["ViewData"] = ViewData;
            return View(ViewData);
        }
        private void CheckUserOrders(string UserID)
        {
            if ((string)Session["UserID"] != UserID)
            {
                WorkWithOrders WWOR = new WorkWithOrders();
                WWOR.CheckUserOrders(UserID);
                Session["UserID"] = UserID;
            }
        }
        public ActionResult TabClick(int TabID)
        {
            WorkWithOrders WWOR = new WorkWithOrders();
            string ActiveUserID = (string)Session["UserID"];
            OrdersDataModel ViewData = new OrdersDataModel(ActiveUserID);
            switch (TabID)
            {
                case 1:
                    ViewData.SetOrdersHistory();
                    break;
                case 2:
                    ViewData.SetActiveOrders();
                    break;
                case 3:
                    ViewData.SetUserIncomingOrders();
                    break;
            }
            Session["ViewData"] = ViewData;
            return RedirectToAction("ShowOrders", "Order", new { UserID = ActiveUserID });
        }
        public ActionResult ApplyOrder(string OrderID)
        {
            WorkWithOrders WWO = new WorkWithOrders();
            WWO.ApplyOrder(OrderID);
            return RedirectToAction("TabClick", "Order", new { TabID = 3 });
        }
        public ActionResult DenyOrder(string OrderID)
        {
            WorkWithOrders WWO = new WorkWithOrders();
            WWO.DenyOrder(OrderID);
            return RedirectToAction("TabClick", "Order", new { TabID = 3 });
        }
    }
}