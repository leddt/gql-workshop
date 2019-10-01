using System.Threading.Tasks;
using GqlWorkshop.Handlers.Mutations;
using GraphQL.Conventions;
using MediatR;

namespace GqlWorkshop.Gql.Schema
{
    public class Mutation
    {
        public Task<CreateQuoteHandler.CreateQuotePayload> CreateQuote(
            [Inject] IMediator mediator, 
            NonNull<CreateQuoteHandler.CreateQuoteInput> input)
        {
            return mediator.Send(input.Value);
        }

        public Task<PostCommentHandler.PostCommentPayload> PostComment(
            [Inject] IMediator mediator,
            NonNull<PostCommentHandler.PostCommentInput> input)
        {
            return mediator.Send(input.Value);
        }
    }
}