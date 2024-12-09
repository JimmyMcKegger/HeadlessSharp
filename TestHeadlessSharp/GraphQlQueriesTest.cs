using HeadlessSharp;
using NUnit.Framework;

namespace TestHeadlessSharp;

[TestFixture]
[TestOf(typeof(GraphQlQueries))]
public class GraphQlQueriesTest
{

    [Test]
    public void GetCartDataTest()
    {
        Assert.Throws<ArgumentException>(() => GraphQlQueries.GetCartData("123123asasdasdasd"));
    }
}