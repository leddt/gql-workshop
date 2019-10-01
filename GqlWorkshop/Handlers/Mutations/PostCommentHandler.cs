using System;
using System.Threading;
using System.Threading.Tasks;
using GqlWorkshop.DbModel;
using GqlWorkshop.Gql.Schema;
using GqlWorkshop.Gql.Schema.GraphTypes;
using GraphQL.Conventions;
using MediatR;

namespace GqlWorkshop.Handlers.Mutations
{
    public class PostCommentHandler : IRequestHandler<PostCommentHandler.PostCommentInput, PostCommentHandler.PostCommentPayload>
    {
        private readonly AppDbContext db;

        public PostCommentHandler(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<PostCommentPayload> Handle(PostCommentInput request, CancellationToken cancellationToken)
        {
            var quoteId = long.Parse(request.QuoteId.IdentifierForType<Quote>());
            
            var quote = await db.Quotes.FindAsync(quoteId);
            if (quote == null) throw new Exception("Quote not found");

            var comment = new Comment {
                PostedAt = DateTime.UtcNow,
                PostedBy = request.Author,
                Message = request.Message,
                QuoteId = quoteId
            };

            db.Comments.Add(comment);
            await db.SaveChangesAsync(cancellationToken);

            return new PostCommentPayload {
                Quote = new QuoteGraphType(quote),
                Comment = new CommentGraphType(comment)
            };
        }

        [InputType]
        public class PostCommentInput : IRequest<PostCommentPayload>
        {
            public Id QuoteId { get; set; }
            public string Author { get; set; }
            public string Message { get; set; }
        }

        public class PostCommentPayload
        {
            public CommentGraphType Comment { get; set; }
            public QuoteGraphType Quote { get; set; }
        }
    }
}