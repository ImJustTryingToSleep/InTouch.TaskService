using InTouch.Authorization.DI;
using InTouch.TaskService.Api.Extensions;
using InTouch.TaskService.BLL.Logic.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapperConfig));

var configuration = builder.Configuration;
builder.Services.AuthConfigure(configuration);

builder.Services.ConfigureDALDependecies();
builder.Services.ConfigureBLLDependecies();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
