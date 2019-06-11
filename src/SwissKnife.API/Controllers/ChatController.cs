using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RealTimeMessenger.DataStorage;
using RealTimeMessenger.Helpers;
using RealTimeMessenger.Hubs;

namespace SwissKnifeDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hub;

        public ChatController(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }

        public IActionResult Get()
        {
            var timerManager = new TimerManager(() => 
                _hub.Clients.All.SendAsync("transferchatdata", DataManager.GetData()));

            return Ok(new {Message = "request completed"});
        }
    }
}
