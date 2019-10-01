using System.Collections.Generic;
using System.Linq;
using GqlWorkshop.DbModel;
using GraphQL.Conventions;

namespace GqlWorkshop.Gql.Schema
{
    public class Query
    {
        public IList<QuoteGraphType> Quotes([Inject] AppDbContext db)
        {
            var quotes = db.Quotes.ToList();
            return quotes.Select(q => new QuoteGraphType(q)).ToList();
        }
    }
}