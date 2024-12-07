using Newtonsoft.Json.Linq;

namespace HeadlessSharp;

public class Observer : IObserver
{
    // fields
    private string name;
    private string displayMessage;

    // getters and setters
    public string Name { get; set; }
    public string DisplayMessage { get; set; }

    // constructor
    public Observer(string name)
    {
        Name = name;
    }

    // methods
    public void AddSubscriber(ISfapiSubject subject)
    {
        subject.RegisterObserver(this);
    }

    public void RemoveSubscriber(ISfapiSubject subject)
    {
        subject.UnregisterObserver(this);
    }

    public void Update(JObject message)
    {
        DisplayMessage = message.ToString();
    }
}