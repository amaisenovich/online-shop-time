using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopTime.Models
{
    public class SearchTools
    {
        ShopDBEntities Db;
        public SearchTools()
        {
            Db = new ShopDBEntities();
        }
        public ICollection<Offers> GetOffersByTag(string TagID)
        {
            return (from TagsRecords in Db.Tags where TagsRecords.TagID == TagID select TagsRecords.Offers).FirstOrDefault();
        }
    }
}