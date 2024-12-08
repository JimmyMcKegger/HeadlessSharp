using GraphQL;

namespace HeadlessSharp
{
    public class GraphQlQueries
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

        public static GraphQLRequest GetCartData(string id)
        {
          string cartId = id;

          return new GraphQLRequest
          {
            Query = @"
              query getCartData($id: CartId!){
                cart(id: $id){
                  id
                  checkoutUrl
                  lines(first:10){
                    nodes{
                      merchandise{
                        __typename
                        ...on ProductVariant{
                          id
                        }
                      }
                      quantity
                    }
                  }
                  discountAllocations{
                    __typename
                    ... on CartAutomaticDiscountAllocation{
                      title
                    }
                    discountedAmount{
                      amount
                      currencyCode
                    }
                  }
                  cost{
                    subtotalAmount{
                      amount
                      currencyCode
                    }
                    
                    totalAmount{
                      amount
                      currencyCode
                    }
                  }
                }
              }"
          };
        }
    }
}