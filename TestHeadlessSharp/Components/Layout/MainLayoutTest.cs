using HeadlessSharp.Components.Layout;
using Newtonsoft.Json.Linq;

namespace TestHeadlessSharp.Components.Layout;

[TestFixture]
[TestOf(typeof(MainLayout))]
public class MainLayoutTest
{
    [Test]
    public void UpdateColors()
    {
        var layout = new MainLayout();
        string json = @"{
    'shop': {
      'id': 'gid://shopify/Shop/82575720782',
      'name': 'James store',
      'brand': {
        'shortDescription': 'Luxury pocket watches made in Sligo.',
        'slogan': 'Its about time.',
        'colors': {
          'primary': [
            {
              'background': '#2C3E50',
              'foreground': '#AF9E57'
            },
            {
              'background': null,
              'foreground': '#AF9E57'
            }
          ],
          'secondary': [
            {
              'background': '#F8F5F0',
              'foreground': '#CD7F32'
            },
            {
              'background': '#707D8F',
              'foreground': '#CD7F32'
            }
          ]
        }
      }
    },
    'products': {
      'nodes': [
        {
          'id': 'gid://shopify/Product/8848058188110',
          'handle': 'gold-pocket-watch',
          'description': 'This pocket watch features a classic gold analog design with elegant roman numerals. Expertly crafted and designed for accuracy, this pocket watch is perfect for those who value timeless style and precision timekeeping. Ideal for formal occasions or daily use, this watch is a versatile and sophisticated accessory.',
          'title': 'Pocket Watch',
          'featuredImage': {
            'url': 'https://cdn.shopify.com/s/files/1/0825/7572/0782/files/pocket-watch-on-black.jpg?v=1713513178'
          },
          'variants': {
            'nodes': []
          }
        },
        {
          'id': 'gid://shopify/Product/8890203930958',
          'handle': 'gift-card',
          'description': '',
          'title': 'Gift card',
          'featuredImage': {
            'url': 'https://cdn.shopify.com/s/files/1/0825/7572/0782/files/gift-card.jpg?v=1718178928'
          },
          'variants': {
            'nodes': [
            ]
          }
        }
      ]
    }
  }";
        
        JObject shopInfo = JObject.Parse(json);
        // layout.TestInitialize(true);
        // layout.Update(shopInfo);
        
        // TODO: Mock rendering
        Assert.Pass();
    }
}