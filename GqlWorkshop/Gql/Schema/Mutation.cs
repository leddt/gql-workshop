using System.Threading.Tasks;
using GqlWorkshop.Gql.InputTypes;
using GraphQL.Conventions;
using MediatR;

namespace GqlWorkshop.Gql.Schema
{
    public class Mutation
    {
        public async Task<QuoteGraphType> CreateQuote([Inject] IMediator mediator, CreateQuoteInput input)
        {
            return await mediator.Send(input);
        }
    }
}