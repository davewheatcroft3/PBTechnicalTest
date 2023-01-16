using MediatR;
using Microsoft.EntityFrameworkCore;
using PBTechnicalAssignment.API.Endpoints;
using PBTechnicalAssignment.API.Mapping;
using PBTechnicalAssignment.Core.Commands;
using PBTechnicalAssignment.Core.Services;
using PBTechnicalAssignment.Data.Access.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(GetOrderQuery).Assembly);
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<DtoMappingProfile>();
});

builder.Services.AddScoped<ApplicationDbContext>(options => 
    new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("PBTechnicalAssignment")
                .Options));

builder.Services.AddTransient<BinWidthCalculationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapOrderEndpoints();

app.Run();
