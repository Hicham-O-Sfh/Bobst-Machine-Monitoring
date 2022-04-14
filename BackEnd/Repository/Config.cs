using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Data.Models;
using Repository.Interfaces;
using Repository.Repositories;

namespace Repository;
public class Config
{
    public Config(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMachineRepository, MachineRepository>();
        services.AddScoped<IMachineProductionRepository, MachineProductionRepository>();
        services.AddDbContext<MachineMonitoringContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }
}
