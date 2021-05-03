using System;
using System.Linq;

namespace StockPricing {
  public class StockPricing {

    public IStockPriceService stockPriceServiceReference { get; set; }

    public bool isCompanyValid(string companyName) {
      return companyName.All(letter => char.IsLetter(letter) && char.IsUpper(letter));
    }

    public bool isShareQuantityValid(int shareQuantity) {
      return shareQuantity > 0;
    }

    public int ParseShareQuantity(string shareQuantityString) {
      return int.Parse(shareQuantityString);
    }

    public double getAssetValue(string companyName, int stockQuantity) {
      return isShareQuantityValid(stockQuantity) ? stockPriceServiceReference.getStockPrice(companyName) * stockQuantity : -999;
    }

   
  }
}
