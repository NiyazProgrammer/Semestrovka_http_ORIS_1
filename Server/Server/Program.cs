using System;

namespace Server;
class Program
{
    private static async Task Main(string[] args)
    {
        try
        {
            using (var server = new HttpServer())
            {
                await server.StartAsync();
                server.ProcessStop();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex}");
        }
    }
}