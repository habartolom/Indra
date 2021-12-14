using Indra.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

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

            var response = NeighborhoodStatus((int[])request.LstCasas.Clone(), request.Dias);
            
            var result = new FirstResponse
            {
                Dias = request.Dias,
                Entrada = arrayToString(request.LstCasas),
                Salida = arrayToString(response)
            };

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Second([FromBody] SecondRequest request)
        {
            var response = MaximizePacketsSize(request.LstPaquetes, request.TamanioCamion);
            return Ok(JsonConvert.SerializeObject(response));
        }

        private int[] NeighborhoodStatus(int [] request, int days)
        {
            var response = new int[request.Length];

            if (request.Length > 0)
            {
                for (int day = 0; day < days; day++)
                {
                    for (int i = 0; i < request.Length; i++)
                    {
                        int firstNeighbor = 0;
                        int secondNeighbor = 0;

                        if (i > 0 && request.Length > 1)
                            firstNeighbor = request[i - 1];

                        if (i < request.Length - 1 && request.Length > 1)
                            secondNeighbor = request[i + 1];

                        response[i] = (firstNeighbor + secondNeighbor) % 2;
                    }
                    response.CopyTo(request, 0);
                }
            }

            return response;
        }

        private int[] MaximizePacketsSize(int [] packageList, int truckSize)
        {
            var selectedPackages = new int[2];
            var maximizedSize = 0;
            
            var sizeAllowed = truckSize - 30;
            if (sizeAllowed < 0)
                return selectedPackages;

            for (int i = 0; i < packageList.Length - 1; i++)
            {
                for (int j = i + 1; j < packageList.Length; j++)
                {
                    if(packageList[i] + packageList[j] <= sizeAllowed && packageList[i] + packageList[j] > maximizedSize)
                    {
                        selectedPackages[0] = packageList[i];
                        selectedPackages[1] = packageList[j];
                        maximizedSize = packageList[i] + packageList[j];
                    }
                }
            }

            return selectedPackages;
        }

        private string arrayToString(int[] array)
        {
            var arrayString = String.Join(", ", array);
            return $"[{arrayString}]";
        }

    }
}
