using System.Threading.Tasks;
using GqlWorkshop.Handlers.Mutations;
using GraphQL.Conventions;
using MediatR;

namespace GqlWorkshop.Gql.Schema
{
    public class Mutation
    {
        public async Task<CreateQuoteHandler.CreateQuotePayload> CreateQuote(
            [Inject] IMediator mediator, 
            NonNull<CreateQuoteHandler.CreateQuoteInput> input)
        {
            return await mediator.Send(input.Value);
        }
    }
}