using LibrarySystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace LibrarySystem.Persistence;

public static class Extension
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration )
	{
		services.AddDbContext<LibraryDbContext>(options =>
	options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
		return services;
	}
}
