using BusinessLogic;
using CommonInterfaces.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetController : ControllerBase
    {
        BetBusiness business = new BetBusiness();
        private readonly ILogger logger;

        public BetController(ILogger<BetController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public ObjectResult Post([FromBody]Bet bet)
        {
            bool result = false;
            try
            {
                result = business.CreateBet(bet);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }
    }
}
