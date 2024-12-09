using HeadlessSharp;
using Moq;
using Newtonsoft.Json.Linq;

namespace TestHeadlessSharp;

[TestFixture]
[TestOf(typeof(Observer))]
public class ObserverTest
{

    [Test]
    public void Name()
    {
        var ob = new Observer("testObserver");
        var expected = "testObserver";
        var actual = ob.Name;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void DisplayMessage()
    {
        var ob = new Observer("testObserver");
        ob.DisplayMessage = "foo";
        var expected = "foo";
        var actual = ob.DisplayMessage;
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void AddSubscriber()
    {
        var ob = new Observer("testObserver");
        var subject = new Mock<SfapiSubject>();
        // TODO: need to mock behaviour
        // ob.AddSubscriber(subject);
        // Assert.IsTrue(subject.Observers.Contains(ob));
        
        Assert.Pass();
    }

    public void RemoveSubscriberTest()
    {
        var ob = new Observer("testObserver");
        var subject = new Mock<SfapiSubject>();
        // TODO: mock a singleton
        
        Assert.Pass();
    }
    
    [Test]
    public void UpdateTest()
    {
        var ob = new Observer("testObserver");
        var expected = "{\n  \"foo\": 1\n}";
        var msg = new JObject();
        msg.Add("foo", 1);
        ob.Update(msg);
        var actual = ob.DisplayMessage;
        
        Assert.AreEqual(expected, actual);
    }
}