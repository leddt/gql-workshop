using System.Collections.Generic;
using System.Linq;
using GqlWorkshop.DbModel;
using GraphQL.Conventions;

namespace GqlWorkshop.Gql.Schema
{
    public class Query
    {
        [Description("Returns **all** quotes.")]
        public IList<QuoteGraphType> Quotes([Inject] AppDbContext db)
        {
            var quotes = db.Quotes.ToList();
            return quotes.Select(q => new QuoteGraphType(q)).ToList();
        }

        [Description("Returns a single quote by it's ID.")]
        public QuoteGraphType Quote([Inject] AppDbContext db, Id id)
        {
            var quoteId = long.Parse(id.IdentifierForType<Quote>());
            var quote = db.Quotes.Find(quoteId);

            return new QuoteGraphType(quote);
        }
    }
}