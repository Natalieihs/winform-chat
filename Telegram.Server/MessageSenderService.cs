using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using Bogus;
using System;

namespace Telegram.Server
{
    public class MessageSenderService : BackgroundService
    {
        private readonly IHubContext<ChatHub> hubContext;

        public MessageSenderService(IHubContext<ChatHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Random random = new Random();
                    var num = random.Next(-100, 99);
                    var faker = new Faker();
                    var fakerName = faker.Name;
                    // 向所有客户端发送一条消息
                    await hubContext.Clients.All.SendAsync("ReceiveMessage", new
                    {
                        Name = faker.Name.FullName(),
                        MessagePreview = faker.Name.FirstName(),
                        DateTime = DateTime.Now.AddMinutes(num),
                    });

                    await Console.Out.WriteLineAsync("sent success");
                }
                catch (Exception ex)
                {
                    // 处理异常
                    Debug.WriteLine(ex.Message);
                }

                // 等待 5 秒
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }
    }
}
