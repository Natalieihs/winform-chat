namespace Telegram.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSignalR();
            builder.Services.AddHostedService<MessageSenderService>();
            var app = builder.Build();

            app.MapHub<ChatHub>("/ChatHub");

            app.Run();
        }
    }
}