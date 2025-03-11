using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

using Mapster;
using MapsterMapper;
using LibrarySystem.Application.Books.Validators;

namespace LibrarySystem.Application;

public static class Extension
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		services.AddValidatorsFromAssemblyContaining<CreateBookValidator>();
        var config = TypeAdapterConfig.GlobalSettings;
		config.Scan(Assembly.GetExecutingAssembly());
		services.AddSingleton(config);
		services.AddScoped<IMapper, ServiceMapper>();

		return services;
	}
}
