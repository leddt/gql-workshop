using System;
using System.Threading;
using System.Threading.Tasks;
using GqlWorkshop.DbModel;
using GqlWorkshop.Gql.Schema;
using GraphQL.Conventions;
using MediatR;

namespace GqlWorkshop.Handlers.Mutations
{
    public class CreateQuoteHandler : IRequestHandler<CreateQuoteHandler.CreateQuoteInput, QuoteGraphType>
    {
        private readonly AppDbContext db;

        public CreateQuoteHandler(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<QuoteGraphType> Handle(CreateQuoteInput request, CancellationToken cancellationToken)
        {
            var quote = new Quote {
                CreatedAt = DateTime.UtcNow,
                SaidBy = request.SaidBy,
                Text = request.Text
            };

            db.Quotes.Add(quote);
            await db.SaveChangesAsync(cancellationToken);

            return new QuoteGraphType(quote);
        }
        
        [InputType]
        public class CreateQuoteInput : IRequest<QuoteGraphType>
        {
            public NonNull<string> Text { get; set; }
            public NonNull<string> SaidBy { get; set; }
        }
    }
}