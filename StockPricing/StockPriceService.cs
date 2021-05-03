using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockPricing {
  public class StockPriceService : IStockPriceService {
    public double getStockPrice(string companyName) {
      return ParseText(getResponse(companyName));
    }

    public virtual string getResponse(string companyName) {
      HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(String.Format("http://agile.cs.uh.edu/stock?ticker={0}", companyName));
      try{
        HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
        return new string(new StreamReader(webResponse.GetResponseStream()).ReadToEnd());
      }
      catch (Exception ex){
        return ex.Message;
      }
      
      
    }

    public double ParseText(string inputString) {
      return Double.Parse(inputString);
    }
  }
}
