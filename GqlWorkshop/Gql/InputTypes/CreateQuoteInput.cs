using GqlWorkshop.Gql.Schema;
using GraphQL.Conventions;
using MediatR;

namespace GqlWorkshop.Gql.InputTypes
{
    [InputType]
    public class CreateQuoteInput : IRequest<QuoteGraphType>
    {
        public NonNull<string> Text { get; set; }
        public NonNull<string> SaidBy { get; set; }
    }
}