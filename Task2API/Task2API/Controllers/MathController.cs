//MathConrtroller.cs
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MathController : ControllerBase
    {
        //post must be used for this method
        [HttpPost]
        public double Square([FromForm] double x, [FromForm] double y)
        {
            return x * x + y * y;
        }
    }
}