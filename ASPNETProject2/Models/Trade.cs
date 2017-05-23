using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETProject2.Models
{
    public class Trade
    {
        //PK
        public int TradeID { get; set; }
        //TradeType
        public string TradeType { get; set; }

        //==========Navigation properties========//
        public ICollection<Contractor> Contractors { get; set; } //One Trade meny contractors
    }
}
