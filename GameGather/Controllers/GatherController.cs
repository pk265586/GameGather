using Microsoft.AspNetCore.Mvc;
using GameGather.Interfaces;

namespace GameGather.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GatherController : ControllerBase
    {
        IGatherLogic m_gatherLogic;

        public GatherController(IGatherLogic gatherLogic) 
        {
            m_gatherLogic = gatherLogic;
        }

        [HttpPost]
        public async Task StartGatherData() 
        {
            await m_gatherLogic.StartGatherData();
        }
    }
}
