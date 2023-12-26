using MajorTest.Models;
using MajorTest.Services.OrderService;
using Microsoft.EntityFrameworkCore;

namespace MajorTest
{
	public class Program
	{
		public static void Main()
		{
			var builder = WebApplication.CreateBuilder();

			string connection = builder.Configuration.GetConnectionString("DefaultConnection");

			builder.Services.AddDbContext<MajorContext>(options => options.UseNpgsql(connection));
			builder.Services.AddMvc();
			builder.Services.AddScoped<IOrderService, OrderService>();

            var app = builder.Build();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            app.UseStatusCodePages();
			app.UseRouting();
			app.UseStaticFiles();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("default", "{controller=Order}/{action=Index}/{id?}");
			});

			app.Run();
		}
	}
}