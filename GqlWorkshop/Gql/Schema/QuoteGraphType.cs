using GqlWorkshop.DbModel;
using GraphQL.Conventions;

namespace GqlWorkshop.Gql.Schema
{
    public class QuoteGraphType
    {
        private readonly Quote data;

        public QuoteGraphType(Quote data)
        {
            this.data = data;
        }

        public Id Id => Id.New<Quote>(data.Id);
        public string Text => data.Text;
        public string SaidBy => data.SaidBy;
    }
}