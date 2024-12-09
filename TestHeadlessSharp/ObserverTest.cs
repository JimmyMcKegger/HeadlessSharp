using HeadlessSharp;

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
}