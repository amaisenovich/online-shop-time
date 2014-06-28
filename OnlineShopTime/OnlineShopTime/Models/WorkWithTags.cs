using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineShopTime.Models;

namespace OnlineShopTime.Models
{
    public class WorkWithTags
    {
        ShopDBEntities Db;
        public WorkWithTags()
        {
            Db = new ShopDBEntities();
        }
    }
}