
using FluentValidation;
using LibrarySystem.Shared.Books.Validators;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LibrarySystem.Application;

public static class Extension
{
	public static IServiceCollection AddShared(this IServiceCollection services)
	{
		
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

		services.AddValidatorsFromAssemblyContaining<CreateBookValidator>();
		services.AddValidatorsFromAssemblyContaining<UpdateBookValidator>();
		//services.AddFluentValidationClientsideAdapters();


		return services;
	}
}
