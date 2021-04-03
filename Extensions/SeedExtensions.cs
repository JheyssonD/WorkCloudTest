using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkCloudTest.Contexts;
using WorkCloudTest.IRepositories;
using WorkCloudTest.Seeds;

namespace WorkCloudTest.Extensions
{
    public static class SeedExtensions
	{
		public static async void UseSeed(this IApplicationBuilder app)
		{
			IServiceScope scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
			PgsqlContext context = scope.ServiceProvider.GetService<PgsqlContext>();
            IRepository repository = scope.ServiceProvider.GetService<IRepository>();
            IConfiguration configuration = scope.ServiceProvider.GetService<IConfiguration>();

			using (context)
			{
				// Crea la BD si no existe
				context.Database.Migrate();

                // ejecuta los seeds necesarios en la BD
                // se deben agregar
                bool runSeed = bool.Parse(configuration.GetValue<string>("Seed"));
                if (runSeed)
                {
                    if (!context.Student.Any())
                    {
                        StudentSeed studentSeed = new StudentSeed(repository);
                        await studentSeed.Run();
                    }
                }
			}
		}
	}
}
