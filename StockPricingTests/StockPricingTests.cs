using NUnit.Framework;
using StockPricing;
using System;
using Moq;

namespace StockPricingTests {
  public class StockPricingTests {

    private StockPricing.StockPricing stockPricing;
    
    [SetUp]
    public void Setup() {
      stockPricing = new StockPricing.StockPricing();
    }

    [Test]
    public void CanaryTest() {
      Assert.True(true);
    }

    [TestCase (true, "GOOG")]
    [TestCase (false, "G0OG")]
    [TestCase (false, "gOOG")]
    [TestCase(false, "goog")]
    [TestCase (false, " ")]
    public void CompanyNameFollowsStandards(bool result, string companyName){

      Assert.AreEqual(result, stockPricing.isCompanyValid(companyName));
    }

 
    [TestCase (true, 100)]
    [TestCase(false, -100)]
    public void SharesQuanitityIsPositive(bool result, int shareQuantity){

      Assert.AreEqual(result, stockPricing.isShareQuantityValid(shareQuantity));
    }

    [TestCase(100, "100")]
    //[TestCase(false, "10O")]
    public void ShareQuantityisNumeric(int result, string shareQuantityString) {

      Assert.AreEqual(result, stockPricing.ParseShareQuantity(shareQuantityString));
    }

    [TestCase ("10o")]
    [TestCase (" ")]
    public void InvalidShareQuantityThrowsException(string shareQuantityString){
      Assert.Throws<FormatException>(() => stockPricing.ParseShareQuantity(shareQuantityString));
    }

    [TestCase ("GOOG", 10, 1206)]
    [TestCase ("GOOG", -5, -999)]
    public void GetAssetValueGivenCompanyNameandQuantity(string companyName, int stockQuantity, double result){

      var workingStockPriceService = new Mock<IStockPriceService>();
      workingStockPriceService.Setup(_ => _.getStockPrice(It.IsAny<string>())).Returns(120.6);

      stockPricing.stockPriceServiceReference = workingStockPriceService.Object;
      Assert.AreEqual(result, stockPricing.getAssetValue(companyName, stockQuantity));
      
    }

    [Test]
    public void GetAssetValueGivenCompanyNameandQuantityButServiceThrowsException() {

      var nonworkingStockPriceService = new Mock<IStockPriceService>();
      nonworkingStockPriceService.Setup(_ => _.getStockPrice(It.IsAny<string>())).Throws(new ApplicationException("Possible network error"));

      stockPricing.stockPriceServiceReference = nonworkingStockPriceService.Object;
      Assert.Throws<ApplicationException>(() => stockPricing.getAssetValue("GOOG", 10));

    }

  }
}