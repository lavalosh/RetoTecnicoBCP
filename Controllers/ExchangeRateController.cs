using Domain.Dto.Layer.Request;
using Domain.Dto.Layer.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetoTecnicoBCP.Domain;
using RetoTecnicoBCP.Service;
using System;

namespace RetoTecnicoBCP.Controllers
{
    [Authorize]
    [Route("api/ExchangeRate")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        public IExchangeRateService _exchangeRateService;
        public ExchangeRateController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet("ExchangeRateMoney")]
        public IActionResult ExchangeRateMoney([FromBody] ExchangeRateRequest RequestValue)
        {
            try
            {
                ExchangeRateResponse response = _exchangeRateService.GetExchangeRate(RequestValue);
                return Ok(response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost("ExchangeRateMoneyUpdate")]
        public IActionResult ExchangeRateMoneyUpdate([FromBody] ExchangeRate RequestValue)
        {
            try
            {
                bool response = _exchangeRateService.ExchangeRateUpdate(RequestValue);

                if (response)
                {
                    return Ok("Se actualizo el valor de tipo de cambio");
                }
                else 
                {
                    return BadRequest("No se actualizo el valor de tipo de cambio");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
