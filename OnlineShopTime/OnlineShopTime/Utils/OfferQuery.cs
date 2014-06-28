using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using SimpleLucene.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopTime.Utils
{
    public class OfferQuery : QueryBase
    {
        public OfferQuery(Query query) : base(query) { }

        public OfferQuery() { }

        public OfferQuery WithKeywords(string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                string[] fields = { "Name", "Description" };
                var parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_29,
                        fields, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29));
                Query multiQuery = parser.Parse(keywords);

                this.AddQuery(multiQuery);
            }
            return this;
        }
    }
}