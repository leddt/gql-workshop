using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GqlWorkshop.DbModel;
using GraphQL.Conventions;
using GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IList<CommentGraphType>> Comments([Inject] AppDbContext db, [Inject] DataLoaderContext loaderContext)
        {
            var loader = loaderContext.GetOrAddCollectionBatchLoader<long, Comment>("QuoteComments", async (ids, ct) => {
                var allComments = await db.Comments.Where(x => ids.Contains(x.QuoteId)).ToListAsync(ct);
                return allComments.ToLookup(x => x.QuoteId);
            });

            var comments = await loader.LoadAsync(data.Id);

            return comments.Select(x => new CommentGraphType(x)).ToList();
        }
    }
}