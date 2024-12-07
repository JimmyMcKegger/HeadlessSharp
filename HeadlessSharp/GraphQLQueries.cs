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
                    query shopInfo{
                shop {
                id
                name
                brand{
                  shortDescription
                  slogan
                  colors{
                    primary{
                      ... colorInfo
                    }
                    secondary{
                      ... colorInfo
                    }
                  }
                }
                }
                products(first:6)
              {
                nodes{
                  id
                  handle
                  description
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
              }

              fragment colorInfo on BrandColorGroup{
                background
                foreground
              }"
            };
        }
    }
}