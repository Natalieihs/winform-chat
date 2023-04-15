using Microsoft.AspNetCore.SignalR;

namespace Telegram.Server
{
    public class ChatHub : Hub
    {
      

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("connected success");
            //发送消息
            await base.OnConnectedAsync();
        }
    }
}