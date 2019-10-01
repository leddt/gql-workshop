using System;
using System.Reflection;
using GraphQL.Conventions;

namespace GqlWorkshop.Gql
{
    public class Injector : IDependencyInjector
    {
        private readonly IServiceProvider serviceProvider;

        public Injector(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public object Resolve(TypeInfo typeInfo)
        {
            return serviceProvider.GetService(typeInfo);
        }
    }


}