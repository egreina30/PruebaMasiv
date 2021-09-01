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
    public class UserController : ControllerBase
    {
        private readonly ILogger logger;
        UserBusiness business = new UserBusiness();

        public UserController(ILogger<UserController> logger)
        {
            this.logger = logger;
        }
        
        [HttpPost]
        public ObjectResult Post([FromBody]User user)
        {
            bool result = false;
            try
            {
                result = business.CreateUser(user);
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
