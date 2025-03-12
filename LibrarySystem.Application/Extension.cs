using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Mapster;
using MapsterMapper;


namespace LibrarySystem.Application;

public static class Extension
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

		var config = TypeAdapterConfig.GlobalSettings;
		config.Scan(Assembly.GetExecutingAssembly());
		services.AddSingleton(config);
		services.AddScoped<IMapper, ServiceMapper>();

		return services;
	}
}
