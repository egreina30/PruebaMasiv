using BusinessLogic;
using CommonInterfaces.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        RouletteBusiness business = new RouletteBusiness();
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger logger;

        public RouletteController(IDistributedCache distributedCache, ILogger<UserController> logger)
        {
            _distributedCache = distributedCache;
            this.logger = logger;
        }

        [HttpGet("Test")]
        public string GetTest()
        {
            return "OK - Service Up";
        }

        [HttpPost]
        public ObjectResult Post()
        {
            int result = 0;
            try
            {
                result = business.CreateRoulette();
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpPut]
        public ObjectResult Put([FromBody] Roulette roulette)
        {
            bool result = false;
            try
            {
                result = business.OpeningRoulette(roulette);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest (ex.Message);
            }

            return Ok(result);
        }

        [HttpPost("ClosingRoulette")]
        public ObjectResult ClosingRoulette([FromBody] Roulette roulette)
        {
            List<Bet> result = new List<Bet>();
            try
            {
                result = business.ClosingRoulette(roulette);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest (ex.Message);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<List<Roulette>> GetAsync()
        {
            var listRoulettes = new List<Roulette>();
            var key = "ListRoulettes";
            string serializedListRoulette;
            try
            {
                var redisListRoulette = await _distributedCache.GetAsync(key);
                if (redisListRoulette != null)
                {
                    serializedListRoulette = Encoding.UTF8.GetString(redisListRoulette);
                    listRoulettes = JsonConvert.DeserializeObject<List<Roulette>>(serializedListRoulette);
                }
                else
                {
                    listRoulettes = business.GetRoulettes();
                    serializedListRoulette = JsonConvert.SerializeObject(listRoulettes);
                    redisListRoulette = Encoding.UTF8.GetBytes(serializedListRoulette);
                    var Options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await _distributedCache.SetAsync(key, redisListRoulette, Options);
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
            }

            return listRoulettes;
        }
    }
}
