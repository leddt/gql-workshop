using System.Threading.Tasks;
using GqlWorkshop.Handlers.Mutations;
using GraphQL.Conventions;
using MediatR;

namespace GqlWorkshop.Gql.Schema
{
    public class Mutation
    {
        public async Task<QuoteGraphType> CreateQuote([Inject] IMediator mediator, CreateQuoteHandler.CreateQuoteInput input)
        {
            return await mediator.Send(input);
        }
    }
}