using Lucene.Net.Documents;
using Lucene.Net.Index;
using OnlineShopTime.Models;
using SimpleLucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopTime.Utils
{
    public class OfferIndexDefinition : IIndexDefinition<Offers>
    {
        public Document Convert(Offers entity)
        {
            var document = new Document();
            document.Add(new Field("offerId", entity.OfferID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("Name", entity.Name, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(entity.Description))
            {
                document.Add(new Field("Description", entity.Description, Field.Store.YES, Field.Index.ANALYZED));
            }

            document.Add(new Field("CreationDate", DateTools.DateToString(entity.DateAndTime.GetValueOrDefault(), DateTools.Resolution.DAY),
                    Field.Store.YES, Field.Index.NOT_ANALYZED));

            if (entity.Price != null)
            {
                var priceField = new NumericField("Price", Field.Store.YES, true);
                priceField.SetIntValue(Int32.Parse(entity.Price.Substring(0, entity.Price.Length - 3)));
                document.Add(priceField);
            }

            //document.Add(new Field("CategoryId", entity.CategoryId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));

            return document;
        }

        public Term GetIndex(Offers entity)
        {
            return new Term("offerId", entity.OfferID.ToString());
        }
    }
}