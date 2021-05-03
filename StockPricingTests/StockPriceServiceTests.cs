using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockPricing;
using NUnit.Framework;
using Moq;

namespace StockPricingTests {
  public class StockPriceServiceTests {

    private StockPricing.StockPriceService stockPriceServiceRef;
    
    [SetUp]
    public void setup(){
      stockPriceServiceRef = new StockPriceService();
    }

    [Test]
    public void CheckResponseReceived(){
      Assert.IsInstanceOf(typeof(string), stockPriceServiceRef.getResponse("GOOG"));
    }


    [Test]
    public void ParseValidStringToDouble(){
      Assert.AreEqual(125.2, stockPriceServiceRef.ParseText("125.2"));
    }

    [Test]
    public void ThrowExceptionWhenINvalidStringGivenToParse(){
      Assert.Throws<FormatException>(() => stockPriceServiceRef.ParseText("125a"));
    }

    [Test]
    public void GetStockPriceWhenGetResponseRetursAValue(){
      var stockPriceService = new Mock<StockPriceService>();
      stockPriceService.Setup(_ => _.getResponse(It.IsAny<string>())).Returns("123.456");
      var price = stockPriceService.Object.getStockPrice("GOOG");
      Assert.AreEqual(123.456, price);
    }
  }
}
