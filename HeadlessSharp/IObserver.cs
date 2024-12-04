using Newtonsoft.Json.Linq;

namespace HeadlessSharp;

public interface IObserver
{
    // Receive Notification from Subject
    void Update(JObject shopInfo);
}