using System.Threading;
using System.Threading.Tasks;
using GraphQL.Conventions;
using GraphQL.DataLoader;

namespace GqlWorkshop.Gql
{
    public class UserContext : IUserContext, IDataLoaderContextProvider
    {
        private readonly DataLoaderContext dataLoaderContext;

        public UserContext(DataLoaderContext dataLoaderContext)
        {
            this.dataLoaderContext = dataLoaderContext;
        }

        public Task FetchData(CancellationToken token)
        {
            return dataLoaderContext.DispatchAllAsync(token);
        }
    }
}