using LibrarySystem.Application;
using LibrarySystem.Infrastructure;

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
		services.AddApplication();
		services.AddInfrastructure(_configuration);		
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.UseRouting();

		app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

	}
}
