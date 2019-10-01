using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        [Description("What was said.")]
        public string Text => data.Text;

        [Description("Who said this.")]
        public string SaidBy => data.SaidBy;

        public IList<CommentGraphType> Comments([Inject] AppDbContext db)
        {
            var comments = db.Comments.Where(x => x.QuoteId == data.Id).ToList();

            return comments.Select(x => new CommentGraphType(x)).ToList();
        }
    }
}