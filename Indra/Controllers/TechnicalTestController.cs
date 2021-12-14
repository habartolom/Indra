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
            var aux = request.LstCasas;
            var response = new int[aux.Length];

            if (aux.Length > 0)
            {
                for (int j = 0; j < request.Dias; j++)
                {
                    for (int i = 0; i < aux.Length; i++)
                    {
                        int firstNeighbor = 0;
                        int secondNeighbor = 0;

                        if (i > 0 && aux.Length > 1)
                            firstNeighbor = aux[i - 1];

                        if (i < aux.Length -1 && aux.Length > 1)
                            secondNeighbor = aux[i + 1];

                        response[i] = (firstNeighbor + secondNeighbor) % 2;
                    }

                    response.CopyTo(aux, 0);
                }
            }

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
