using Lucene.Net.Documents;
using OnlineShopTime.Models;
using SimpleLucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopTime.Utils
{
    public class OfferResultDefinition : IResultDefinition<Offers>
    {
        public Offers Convert(Document document)
        {
            var offer = new Offers();
            offer.OfferID = document.GetValue<string>("OfferID");
            offer.Name = document.GetValue("Name");
            offer.Price = document.GetValue<string>("Price");
            //offer.CategoryId = document.GetValue<int>("CategoryId");
            offer.DateAndTime = DateTools.StringToDate(document.GetValue("CreationDate"));
            offer.Description = document.GetValue("Description");
            return offer;
        }
    }
}