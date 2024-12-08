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

window.CreateCart = variant => {
    const myHeaders = new Headers();
    myHeaders.append("X-Shopify-Storefront-Access-Token", "e1936fde69a6692d38517d05aab6dd73");
    myHeaders.append("Content-Type", "application/json");

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
        headers: myHeaders,
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