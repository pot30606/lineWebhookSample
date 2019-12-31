using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication6.Controllers
{
    public class LineController : isRock.LineBot.LineWebHookControllerBase
    {
        private static string ChannelAccessToken = "改成你的ChannelAccessToken";
        private static string AdminUserId = "改成你的AdminUserId";

        [Route("api/WebHookController")]
        [HttpPost]
        public IActionResult Post()
        {
            //取得Line Event(範例，只取第一個)

            try
            {
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                //配合Line verify 
                if (LineEvent.replyToken == "00000000000000000000000000000000") return Ok();

                var ret = new List<isRock.LineBot.MessageBase>();

                if (LineEvent.type == "message")
                {
                    var text = new isRock.LineBot.TextMessage("你說了" + LineEvent.message.text);
                    ret.Add(text);
                }

                var bot = new isRock.LineBot.Bot(ChannelAccessToken);
                bot.ReplyMessage(LineEvent.replyToken, ret);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return Ok();
            }
        }
    }
}