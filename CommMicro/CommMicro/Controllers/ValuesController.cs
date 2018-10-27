using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CommMicro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        // GET api/values/5
        [HttpGet]
        public async Task<IActionResult> Get(int i)
        {
            Debug.WriteLine($"Got {i}");
            if (i % 2 == 0)
            {
                Debug.WriteLine("ups, losted");
            }
            else
            {
                Debug.WriteLine($"Received &Processed {i}");
            }

            return Ok(i);
        }
    }
}
