using Repository.Interfaces;
using Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.AddScoped<IMachineRepository, MachineRepository>();

app.Run();
