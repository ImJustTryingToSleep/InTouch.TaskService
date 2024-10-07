using InTouch.Authorization.DI;
using InTouch.TaskService.Api.Extensions;
using InTouch.TaskService.BLL.Logic.Config;
using FluentValidation.AspNetCore;
using InTouch.Notification.DI;
using InTouch.SettingService.HubRegistration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddFluentValidationAutoValidation();
//builder.Services.RegisterSettingsService();

// var jwt = builder.Services.GetJwtOptions();
// builder.Services.AuthConfigure(jwt);

builder.Services.ConfigureHttpClients();

builder.Services.ConfigureDALDependecies();
builder.Services.ConfigureBLLDependecies();
builder.Services.ConfigureValidationDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
