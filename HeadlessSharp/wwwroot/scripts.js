window.setCookie = (name, value) => {
    const d = new Date();
    d.setTime(d.getTime() + (24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = name + "=" + value + ";" + expires + ";path=/";
};

window.getCookie = cookieName => {
    const name = cookieName + "=";
    const decodedCookie = decodeURIComponent(document.cookie);
    const ca = decodedCookie.split(';');

    for(let i = 0; i <ca.length; i++) {
        let c = ca[i];

        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }

        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

window.myHeaders = () => {
    const h = new Headers();
    h.append("X-Shopify-Storefront-Access-Token", "e1936fde69a6692d38517d05aab6dd73");
    h.append("Content-Type", "application/json");
    return h;
}

window.CreateCart = variant => {
    const graphql = JSON.stringify({
        query: `
            mutation cartCreate($input: CartInput) {
              cartCreate(input: $input) {
                cart {
                  id
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
            }
        `,
        variables: {"input":{"lines":[{"merchandiseId": variant,"quantity":1}]}}
    })
    const requestOptions = {
        method: "POST",
        headers: myHeaders(),
        body: graphql,
        redirect: "follow"
    };

    return fetch("https://317b61-f3.myshopify.com/api/2024-10/graphql.json", requestOptions)
        .then(response => {
            if (!response.ok) {
                throw new Error(response.statusText);
            }
            return response.json();
        })
        .then(json => {
            console.log(json);
            const cartId = json['data']['cartCreate']['cart']['id'];
            console.log(cartId);
            setCookie("cart", cartId);
        });
};

window.getCartData = cartId => {
    console.log("getCartData firing with: ");
    console.log(cartId);
    const graphql = JSON.stringify({
        query: `
            query getCartData($id: ID!){
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
            }
        `,
        variables: { "id": cartId }
    })
    const requestOptions = {
        method: "POST",
        headers: myHeaders(),
        body: graphql,
        redirect: "follow"
    };

    return fetch("https://317b61-f3.myshopify.com/api/2024-10/graphql.json", requestOptions)
        .then(response => {
            if (!response.ok) {
                throw new Error(response.statusText);
            }
            return response.json();
        })
        .then(json => {
            console.log(json);
        });
};