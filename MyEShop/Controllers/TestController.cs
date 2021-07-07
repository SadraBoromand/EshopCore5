using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MyEShop.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        [Route("test1")]
        public string Test1()
        {
            
            return "Test 1";
        }

        [Route("test2")]
        [AllowAnonymous]
        public string Test2()
        {
            return "Test 2";
        }
    }
}
