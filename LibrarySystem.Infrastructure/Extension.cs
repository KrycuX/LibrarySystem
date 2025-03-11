using LibrarySystem.Application.Common.Interfaces;
using LibrarySystem.Infrastructure.EntityFramework;
using LibrarySystem.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibrarySystem.Infrastructure;

public static class Extension
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<IBookRepository, BookRepository>();
		services.AddEF(configuration);
		return services;
	}
}
