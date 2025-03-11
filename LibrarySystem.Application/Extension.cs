using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;

namespace LibrarySystem.Application;

public static class Extension
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		// Rejestracja MediatR
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

		// Rejestracja FluentValidation
		services.

		return services;
	}
}
