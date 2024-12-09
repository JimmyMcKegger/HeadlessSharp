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
    private readonly GraphQLHttpClient graphqlClient;
    private static SfapiSubject instance;
    // private Dictionary<string, List<IObserver>> cartObservers = new Dictionary<string, List<IObserver>>();
    
    // getters and setters
    private string ResponseBody { get; set; }
    public List<IObserver> Observers { get; set; }
    public Dictionary<string, List<IObserver>> CartObservers { get; set; }

    public static SfapiSubject GetInstance(string apiKey = "", string domain = "", string apiVersion = "")
    {
        if (instance == null)
        {
            // make a singleton because there is only one API
            instance = new SfapiSubject(apiKey, domain, apiVersion);
        }
        return instance;
    }

    //  private constructor
    private SfapiSubject(string apiKey, string domain, string apiVersion)
    {
        graphqlClient = new GraphQLHttpClient($"https://{domain}/api/{apiVersion}/graphql.json", new NewtonsoftJsonSerializer());
        graphqlClient.HttpClient.DefaultRequestHeaders.Add("X-Shopify-Storefront-Access-Token", apiKey);
        Observers = new List<IObserver>();
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
    
    public void RegisterObserver(IObserver observer)
    {
        try
        {
            if (!observers.Contains(observer))
            {
                Console.WriteLine($"ADDING OBSERVER: {observer}");
                observers.Add(observer);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error registering observer: {e.Message}");
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

    public void NotifyObservers(JObject data)
    {
        foreach (var observer in observers)
        {
            observer.Update(data);
        }
    }
}