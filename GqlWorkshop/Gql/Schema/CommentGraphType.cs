using System;
using GqlWorkshop.DbModel;
using GraphQL.Conventions;

namespace GqlWorkshop.Gql.Schema
{
    public class CommentGraphType
    {
        private readonly Comment data;

        public CommentGraphType(Comment data)
        {
            this.data = data;
        }

        public Id Id => Id.New<Comment>(data.Id);
        public DateTime PostedAt => data.PostedAt;
        public string PostedBy => data.PostedBy;
        public string Message => data.Message;
    }
}