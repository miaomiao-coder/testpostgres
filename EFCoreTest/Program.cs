using Device.Api.Mapping;
using EFCoreTest.Bootstrap;
using Microsoft.EntityFrameworkCore;
using Model;
using Repository;
using Repository.Base;
using Repository.Impl;
using Service;
using Service.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IManagerService, ManagerService>();


builder.Services.AddScoped<ICompanyRepo, CompanyRepo>();
builder.Services.AddScoped<IManagerRepo, ManagerRepo>();

//builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
IConfiguration configuration = builder.Configuration;
builder.Services.AddDbContext<EDbContext>(options =>
options
.UseNpgsql(configuration["PostgreConnectionStrings"])
) .AddSession();

builder.Services.RegisterProfiler();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseProfiler();
app.UseAuthorization();

app.MapControllers();

app.Run();
