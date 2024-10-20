namespace Dice_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSignalR();
            var app = builder.Build();

            // Add SignalR services
            // Map the GameHub to a route
            app.MapHub<GameHub>("/gamehub");

            app.Run();

        }
    }
}
