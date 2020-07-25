using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVB.ApiBlog.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AVB.ApiBlog.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpPost]
        public IActionResult IsAuthenticated(AdminUser adminUser)
        {
            bool status = false;

            if (adminUser.Email == "f@outlook.com" && adminUser.Password == "1234")
            {
                status = true;
            }

            var result = new
            {
                status = status
            };
            return Ok(result);
        }

    }
}
