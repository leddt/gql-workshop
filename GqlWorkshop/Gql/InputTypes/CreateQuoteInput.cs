using GraphQL.Conventions;

namespace GqlWorkshop.Gql.InputTypes
{
    [InputType]
    public class CreateQuoteInput
    {
        public NonNull<string> Text { get; set; }
        public NonNull<string> SaidBy { get; set; }
    }
}