using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json.Linq;

namespace HeadlessSharp;

// observable storefront API interface
public class SfapiSubject : ISfapiSubject
{
    // fields
    private List<IObserver> observers = new List<IObserver>();
    private List<IObserver> cartObservers = new List<IObserver>();
    // private Dictionary<string, string> tokens = new Dictionary<string, string>;
    private readonly GraphQLHttpClient graphqlClient;
    private static SfapiSubject _instance;
    

    // getters and setters
    private string ResponseBody { get; set; }
    public List<IObserver> Observers { get; set; }
    public List<IObserver> CartObservers { get; set; }
    // public Dictionary<string, string> Tokens { get; set; }


    public static SfapiSubject GetInstance(string apiKey = "", string domain = "", string apiVersion = "")
    {
        if (_instance == null)
        {
            // make a singleton because there is only one API
            _instance = new SfapiSubject(apiKey, domain, apiVersion);
        }
        return _instance;
    }

    //  private constructor
    private SfapiSubject(string apiKey, string domain, string apiVersion)
    {
        graphqlClient = new GraphQLHttpClient($"https://{domain}/api/{apiVersion}/graphql.json", new NewtonsoftJsonSerializer());
        graphqlClient.HttpClient.DefaultRequestHeaders.Add("X-Shopify-Storefront-Access-Token", apiKey);
        Observers = new List<IObserver>();
        CartObservers = new List<IObserver>();
    }

    // Get data for shop data observers
    public async Task GetShopInfo()
    {
        try
        {
            var shopQuery = GraphQlQueries.GetHomePageData();
            var response = await graphqlClient.SendQueryAsync<dynamic>(shopQuery);
            JObject shopInfo = response.Data;
            NotifyObservers(shopInfo);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
    
    // Get data for Cart observers
    public async Task GetCartInfo()
    {
        // TODO: check for a cart cookie
        // var cartId = that cookie
        try
        {
            
            // var cartQuery = GraphQlQueries.GetCartData(cartId);
            // var response = await graphqlClient.SendQueryAsync<dynamic>(cartQuery);
            // JObject cartInfo = response.Data;
            // NotifyCartObservers(cartInfo);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Getting Cart Data: {e.Message}");
        }
    }
    
    public async Task CartCreate(string variantId)
    {
        // cartCreate
        Console.WriteLine("CREATING CART");
        try
        {
            var request = GraphQlMutations.CartCreate(variantId);
            var response = await graphqlClient.SendQueryAsync<dynamic>(request);
            JObject cartInfo = response.Data;
            Console.WriteLine(cartInfo);
            NotifyCartObservers(cartInfo);
            // cartId = cartInfo["cartCreate"]?["cart"]?["id"]?.ToString();
            
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating cart: {e.Message}");
        }
        
        
    }
    
    // public async Task AddToCart(string variantId, string cartIdOrToken)
    // {
    //     //
    //     bool isCart = cartIdOrToken.Contains("gid://shopify/Cart/");
    //     
    //     if (isCart)
    //     {
    //         
    //         // TODO: CartLinesAdd
    //
    //         
    //     }
    //     else
    //     {
    //         // cartCreate
    //         Console.WriteLine("CREATING CART");
    //         try
    //         {
    //             var request = GraphQlMutations.CartCreate(variantId);
    //             var response = await graphqlClient.SendQueryAsync<dynamic>(request);
    //             JObject cartInfo = response.Data;
    //             Console.WriteLine(cartInfo);
    //             NotifyCartObservers(cartInfo);
    //             cartId = cartInfo["cartCreate"]?["cart"]?["id"]?.ToString();
    //             
    //             Tokens[cartIdOrToken] = cartInfo["cartCreate"]?["token"]?.ToString();
    //             
    //             // TODO: How to set a cookie from here?
    //             // https://stackoverflow.com/questions/77751794/i-am-trying-to-create-cookie-while-login-with-a-user-in-blazor-web-server-app-bu
    //             
    //             
    //         }
    //         catch (Exception e)
    //         {
    //             Console.WriteLine($"Error creating cart: {e.Message}");
    //         }
    //     }
    //     
    // }
    // register with the SfapiSubject using the following method
    public void RegisterObserver(IObserver observer)
    {
        Console.WriteLine($"ADDING OBSERVER: {observer}");
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }
    
    public void RegisterCartObserver(IObserver observer)
    {
        Console.WriteLine($"ADDING CART OBSERVER: {observer}");
        if (!CartObservers.Contains(observer))
        {
            CartObservers.Add(observer);
        }
    }

    // unregister from the storefront API subject using the following method
    public void UnregisterObserver(IObserver observer)
    {
        try
        {
            Console.WriteLine($"REMOVING OBSERVER: {observer}");
            Observers.Remove(observer);
        }
        catch (Exception e)
        {
            Console.WriteLine($"ERROR IN UNREGISTER: {e}");
        }
    }
    
    public void UnregisterCartObserver(IObserver observer)
    {
        try
        {
            Console.WriteLine($"REMOVING CART OBSERVER: {observer}");
            CartObservers.Remove(observer);
        }
        catch (Exception e)
        {
            Console.WriteLine($"ERROR IN UNREGISTER Carts: {e}");
        }
    }

    public void NotifyObservers(JObject data)
    {
        foreach (var observer in observers)
        {
            observer.Update(data);
        }
    }
    public void NotifyCartObservers(JObject data)
    {
        foreach (var observer in CartObservers)
        {
            observer.Update(data);
        }
    }
}