using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

using Mapster;
using MapsterMapper;
using LibrarySystem.Application.Books.Validators;
using FluentValidation.AspNetCore;

namespace LibrarySystem.Application;

public static class Extension
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

		services.AddValidatorsFromAssemblyContaining<CreateBookValidator>();
		services.AddValidatorsFromAssemblyContaining<UpdateBookValidator>();
		services.AddFluentValidationClientsideAdapters();

		var config = TypeAdapterConfig.GlobalSettings;
		config.Scan(Assembly.GetExecutingAssembly());
		services.AddSingleton(config);
		services.AddScoped<IMapper, ServiceMapper>();

		return services;
	}
}
