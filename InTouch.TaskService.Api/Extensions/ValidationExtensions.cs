using FluentValidation;
using InTouch.TaskService.Common.Entities.Validators;
using Microsoft.AspNetCore.Identity;

namespace InTouch.TaskService.Api.Extensions;

public static class ValidationExtensions
{
    public static IServiceCollection ConfigureValidationDependencies(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<BoardValidator>();
        services.AddValidatorsFromAssemblyContaining<ColumnValidator>();
        services.AddValidatorsFromAssemblyContaining<TaskValidator>();
        services.AddValidatorsFromAssemblyContaining<TaskUpdateValidator>();
        return services;
    }
}