using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GqlWorkshop.DbModel;
using GqlWorkshop.Handlers.Queries;
using GraphQL.Conventions;
using GraphQL.DataLoader;
using MediatR;

namespace GqlWorkshop.Gql.Schema.GraphTypes
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

        public async Task<IList<CommentGraphType>> Comments([Inject] IMediator mediator, [Inject] DataLoaderContext loaderContext)
        {
            var loader = loaderContext.GetOrAddCollectionBatchLoader<long, Comment>(
                nameof(GetCommentsByQuoteIds), 
                (ids, ct) => mediator.Send(new GetCommentsByQuoteIds.Query(ids), ct));

            var comments = await loader.LoadAsync(data.Id);

            return comments.Select(x => new CommentGraphType(x)).ToList();
        }
    }
}