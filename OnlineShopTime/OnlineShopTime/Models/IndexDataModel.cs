using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopTime.Models
{
    public class IndexDataModel
    {
        public string ShowString;

        public IQueryable<Offers> NewOffers;

        public IQueryable<Users> TopUsers;

        public IQueryable<Offers> TopOffers;

        public ICollection<WeightTags> WeightTags;

        public IndexDataModel()
        {
            ShowTopOffers();
        }
        public void ShowTopOffers()
        {
            ShowString = "TopOffers";
        }
        public void ShowTopUsers()
        {
            ShowString = "TopUsers";
        }
        public void ShowNewOffers()
        {
            ShowString = "NewOffers";
        }
    }
}