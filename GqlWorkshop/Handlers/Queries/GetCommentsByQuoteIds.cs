using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GqlWorkshop.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GqlWorkshop.Handlers.Queries
{
    public class GetCommentsByQuoteIds : IRequestHandler<GetCommentsByQuoteIds.Query, ILookup<long, Comment>>
    {
        private readonly AppDbContext db;

        public GetCommentsByQuoteIds(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<ILookup<long, Comment>> Handle(Query request, CancellationToken cancellationToken)
        {
            var allComments = await db.Comments
                .Where(x => request.Ids.Contains(x.QuoteId))
                .ToListAsync(cancellationToken);

            return allComments.ToLookup(x => x.QuoteId);
        }

        public class Query : IRequest<ILookup<long, Comment>>
        {
            public Query(IEnumerable<long> ids)
            {
                Ids = ids;
            }

            public IEnumerable<long> Ids { get; set; }
        }
    }
}