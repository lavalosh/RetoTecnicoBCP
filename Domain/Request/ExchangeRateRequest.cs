using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto.Layer.Request
{
    public class ExchangeRateRequest
    {
        public double Amount { get; set; }
        public string OriginCurrency { get; set; }
        public string DestinationCurrency { get; set; }
    
    }
}
