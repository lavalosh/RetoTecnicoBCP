using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto.Layer.Response
{
    public class ExchangeRateResponse
    {
        public double AmountExchangeRate { get; set; }
        public string OriginCurrency { get; set; }
        public string DestinationCurrency { get; set; }
        public double ExchangeRateMoney { get; set; }

    }
}
