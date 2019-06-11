using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SwissKnife.API.DataStorage;
using SwissKnife.API.Helpers;
using SwissKnife.API.Hubs;

namespace SwissKnife.API.Controllers
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