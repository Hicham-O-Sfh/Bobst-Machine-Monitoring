using Microsoft.EntityFrameworkCore;
using Repository.Data.Models;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;
using Service.Services;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Logging config 
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers();
// builder.Services.AddControllers().AddNewtonsoftJson(options =>
//     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
// );
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add IoC Mapping 


builder.Services.AddScoped<IMachineService, MachineService>();
builder.Services.AddScoped<IMachineProductionService, MachineProductionService>();

builder.Services.AddScoped<IMachineRepository, MachineRepository>();
builder.Services.AddScoped<IMachineProductionRepository, MachineProductionRepository>();

//enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            ;
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
