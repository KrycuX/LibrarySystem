
using LibrarySystem.Application;
using LibrarySystem.Infrastructure;
using Microsoft.OpenApi.Models;

namespace LibrarySystem.Api;
public class Start
{
	private readonly IConfiguration _configuration;

	public Start(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddControllers();
		services.AddControllersWithViews();
		services.AddRazorPages();
		services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc(
				"v1",
				new OpenApiInfo
				{
					Title = "Library API",
					Version = "v1"
				});
		});
		services.AddApplication();
		services.AddShared();
		services.AddInfrastructure(_configuration);
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseWebAssemblyDebugging();
			app.UseSwagger();
			app.UseSwaggerUI();
		}
		app.UseBlazorFrameworkFiles();
		app.UseStaticFiles();
		app.UseRouting();

		app.UseEndpoints(endpoints => { 
			endpoints.MapControllers(); 
			endpoints.MapRazorPages();
			endpoints.MapFallbackToFile("index.html");
		});
	

	}
}
