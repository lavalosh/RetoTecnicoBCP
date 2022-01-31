using Domain.Dto.Layer.Request;
using Domain.Dto.Layer.Response;
using RetoTecnicoBCP.DataContext;
using RetoTecnicoBCP.Domain;
using System;
using System.Linq;

namespace RetoTecnicoBCP.Service
{
    public interface IExchangeRateService
    {
        ExchangeRateResponse GetExchangeRate(ExchangeRateRequest request);

        bool ExchangeRateUpdate(ExchangeRate request);
    }

    public class ExchangeRateService : IExchangeRateService
    {
        private readonly AppDbContext _context;
        public ExchangeRateService(AppDbContext context)
        {
            _context = context;
        }

        public ExchangeRateResponse GetExchangeRate(ExchangeRateRequest request)
        {
            try
            {
                ExchangeRate obj =  _context.ExchangeRate.SingleOrDefault(x => x.Name.Equals(request.DestinationCurrency));

                ExchangeRateResponse objResponse = new ExchangeRateResponse()
                {
                    AmountExchangeRate = request.Amount * obj.ExchangeRateMoney,
                    OriginCurrency = request.OriginCurrency,
                    DestinationCurrency = request.DestinationCurrency,
                    ExchangeRateMoney = obj.ExchangeRateMoney
                };

                return objResponse;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ExchangeRateUpdate(ExchangeRate request)
        {
            try
            {
                if (request.Name == null || string.IsNullOrEmpty(request.Name) || request.ExchangeRateMoney <= 0)
                {
                    return false;
                }

                ExchangeRate obj = _context.ExchangeRate.SingleOrDefault(x => x.Name.Equals(request.Name));

                if (obj == null)
                {
                    return false;
                }

                obj.ExchangeRateMoney = request.ExchangeRateMoney;

                _context.Update(obj);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
