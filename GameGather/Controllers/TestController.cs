using Microsoft.AspNetCore.Mvc;

namespace GameGather.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<string> GetHello()
        {
            await Task.Yield();
            return "Hello from TestController";
        }
    }
}
