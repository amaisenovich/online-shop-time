using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopTime.Models
{
    public class WorkWithTags
    {
        ShopDBEntities Db;
        public WorkWithTags()
        {
            Db = new ShopDBEntities();
        }
        public List<WeightTags> GetWeightTags()
        {
            List<WeightTags> Result = new List<WeightTags>();
            IQueryable<Tags> Tags = from TagsRecords in Db.Tags select TagsRecords;
            foreach (Tags Tag in Tags)
            {
                WeightTags WeightTag = new WeightTags();
                WeightTag.TagName = '#' + Tag.Name;
                WeightTag.Weight = Tag.Offers.Count;
                WeightTag.TagID = Tag.TagID;
                Result.Add(WeightTag);                
            }
            return Result;
        }
        public IQueryable<string> GetTagsNamesList()
        {
            return (from TagsRecords in Db.Tags select TagsRecords.Name);
        }
        public int GetTagInOffersCount()
        {
            int Result = 0;
            IQueryable<Tags> Tags = from TagsRecords in Db.Tags select TagsRecords;
            foreach (Tags Tag in Tags)
            {
                Result += Tag.Offers.Count;
            }
            return Result;
        }
    }
}