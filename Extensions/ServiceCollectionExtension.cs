using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace PalpinApi.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddSerilogServices(this IServiceCollection services)
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
				.WriteTo.Console()
				.CreateLogger();
			return services.AddSingleton(Log.Logger);
		}
	}
}