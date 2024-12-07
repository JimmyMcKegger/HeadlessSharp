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
                      products(first:3)
                    {
                      nodes{
                        id
                        handle
                        title
                        featuredImage{
                          url
                        }
                        variants(first:1){
                          nodes{
                            id
                            title
                            sku
                            price{
                              amount
                              currencyCode
                            }
                          }
                        }
                      }
                    }
                }"
            };
        }
    }
}