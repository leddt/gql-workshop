using System;
using GqlWorkshop.DbModel;
using GqlWorkshop.Gql.InputTypes;
using GraphQL.Conventions;

namespace GqlWorkshop.Gql.Schema
{
    public class Mutation
    {
        public QuoteGraphType CreateQuote([Inject] AppDbContext db, CreateQuoteInput input)
        {
            var quote = new Quote {
                CreatedAt = DateTime.UtcNow,
                SaidBy = input.SaidBy,
                Text = input.Text
            };

            db.Quotes.Add(quote);
            db.SaveChanges();

            return new QuoteGraphType(quote);
        }
    }
}