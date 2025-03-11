using LibrarySystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibrarySystem.Infrastructure.EntityFramework;

public static class Extension
{
	public static IServiceCollection AddEF(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<LibraryDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

		return services;
	}
}
