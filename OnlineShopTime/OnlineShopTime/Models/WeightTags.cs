using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopTime.Models
{
    public class WeightTags
    {
        public string TagName;
        public double Weight;
        public string TagID;
        public WeightTags()
        {
            TagName = null;
            TagID = null;
            Weight = 0;
        }
    }
}