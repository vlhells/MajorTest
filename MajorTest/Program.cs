using MajorTest.Models;
using MajorTest.Services.CouriersService;
using MajorTest.Services.OrdersService;
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
			builder.Services.AddScoped<IOrdersService, OrdersService>();
            builder.Services.AddScoped<ICouriersService, CouriersService>();

            var app = builder.Build();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            app.UseStatusCodePages();
			app.UseRouting();
			app.UseStaticFiles();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("default", "{controller=Orders}/{action=Index}/{id?}");
			});

			app.Run();
		}
	}
}