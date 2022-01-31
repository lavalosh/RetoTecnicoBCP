using Microsoft.AspNetCore.Mvc;
using RetoTecnicoBCP.Domain.Request;
using RetoTecnicoBCP.Util;
using System;

namespace RetoTecnicoBCP.Controllers
{
    [Route("api/Default")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly ITokenAuth _tokenAuth;

        public DefaultController(ITokenAuth tokenAuth)
        {
            _tokenAuth = tokenAuth;
        }

        [HttpGet("GenerateToken")]
        public IActionResult GenerateToken([FromBody] GenerateTokenRequest userLoginDto)
        {
            try
            {
                string token = _tokenAuth.GenerateToken(userLoginDto.User);
                return Ok(token);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
