/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgModerator.Data.Entity;
using TgModerator.Data.Repository.IRepository;

namespace TgModerator.API.Controllers
{
    [ApiController]
    [Route("api/bot")]
    public class BotActionsController : BotMenuController
    {

        private TelegramBotClient _TgClient = new TelegramBotClient("5492109358:AAHzQpHb2PRz9KeLedC0Z59OHJehbhkdHeo");
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BotMenuController> _logger;

        public BotActionsController(ILogger<BotMenuController> logger, IUnitOfWork unitOfWork)
            : base(logger, unitOfWork)
        {
        }
        public bool IsAdminIn { get; set; }
        public long RegisteringId { get; set; }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            

            return Ok();
        }
        }
}
*/