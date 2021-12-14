using Indra.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Indra.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TechnicalTestController : ControllerBase
    {
        [HttpPost]
        public IActionResult First( [FromBody] FirstRequest request)
        {
            var response = new int[] { 1, 2 };
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Second([FromBody] SecondRequest request)
        {
            var response = new int[] { 1, 2 };
            return Ok(response);
        }

    }
}
