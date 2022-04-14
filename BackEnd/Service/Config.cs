using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;
using Service.Services;

namespace Service;
public class Config
{
    public Config(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMachineRepository, MachineRepository>();
        services.AddScoped<IMachineProductionRepository, MachineProductionRepository>();

        services.AddScoped<IMachineService, MachineService>();
        services.AddScoped<IMachineProductionService, MachineProductionService>();
    }
}
