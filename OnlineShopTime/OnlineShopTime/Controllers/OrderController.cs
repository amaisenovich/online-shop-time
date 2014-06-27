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
        public ActionResult TabClick(int TabID)
        {
            WorkWithOrders WWOR = new WorkWithOrders();
            WorkWithUsers WWU = new WorkWithUsers();
            string ActiveUserID = WWU.GetUserByName(User.Identity.Name).UserID;
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
            return RedirectToAction("TabClick", "Order", new { TabID = 2 });
        }
        public ActionResult ApplyOrderConfirm(string OrderID)
        {
            if (OrderID == null)
                OrderID = (string)Session["OrderID"];           
            ViewBag.OrderID = OrderID;
            Session["OrderID"] = OrderID;
            return View();
        }
        public ActionResult DenyOrderConfirm(string OrderID)
        {
            if (OrderID == null)
                OrderID = (string)Session["OrderID"];
            ViewBag.OrderID = OrderID;
            Session["OrderID"] = OrderID;
            return View();
        }
        public ActionResult ShowUserInfo(string UserID)
        {
            if (UserID == null)
                UserID = (string)Session["UserID"];
            WorkWithUsers WWU = new WorkWithUsers();
            Session["UserID"] = UserID;
            return View(WWU.GetUserByID(UserID));
        }
        public ActionResult CancelOrderConfurm(string OrderID)
        {
            if (OrderID == null)
                OrderID = (string)Session["OrderID"];
            ViewBag.OrderID = OrderID;
            Session["OrderID"] = OrderID;
            return View();
        }
        public ActionResult TradeCompleteConfurm(string OrderID)
        {
            if (OrderID == null)
                OrderID = (string)Session["OrderID"];
            ViewBag.OrderID = OrderID;
            Session["OrderID"] = OrderID;
            return View(); 
        }
        public ActionResult CompleteTrade(string OrderID)
        {
            WorkWithOrders WWOR = new WorkWithOrders();
            WWOR.CompleteTrade(OrderID);
            return RedirectToAction("TabClick", "Order", new { TabID = 1 });
        }
    }
}