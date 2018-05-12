using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AADx.TodoApi
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var services = new ServiceCollection()
        .AddLogging(config => config.AddConsole())
        .BuildServiceProvider();

      services
        .GetRequiredService<ILogger<Program>>()
        .LogInformation("Starting ....");

      BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args)
    {
      return WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .Build();
    }
  }
}
