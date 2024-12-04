using GraphQL;

namespace HeadlessSharp
{
    public class GraphQLQueries
    {
        public static GraphQLRequest GetHomePageData()
        {
            return new GraphQLRequest
            {
                Query = @"
                    query {
                      shop {
                      id
                      name
                      brand{
                        shortDescription
                      }
                     }
                   }"
            };
        }
    }
}