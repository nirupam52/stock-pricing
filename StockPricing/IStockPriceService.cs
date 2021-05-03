using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPricing {
  public interface IStockPriceService {
    public double getStockPrice(string companyName);
  }
}
