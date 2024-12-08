using GraphQL;
using System.Collections.Generic;

namespace HeadlessSharp
{
    public class GraphQlMutations
    {
        public static GraphQLRequest CartCreate(string productVariant)
        {
            var cartInput = new
            {
                lines = new[]
                {
                    new
                    {
                        merchandiseId = productVariant,
                        quantity = 1
                    }
                }
            };

            return new GraphQLRequest
            {
                Query = @"
                  mutation cartCreate($input: CartInput) {
                    cartCreate(input: $input) {
                      cart {
                        id
                        checkoutUrl
                        note
                        lines(first: 5) {
                          nodes {
                            id
                            merchandise {
                              __typename
                              ...on ProductVariant {
                                id
                                price {
                                  amount
                                  currencyCode
                                } 
                              }
                            }
                            quantity
                          }
                        }
                      }
                      userErrors {
                        field
                        message
                      }
                      warnings {
                        message
                        code
                      }
                    }
                  }",
                Variables = new
                {
                    input = cartInput
                }
            };
        }
    }
}