using FluentValidation;
using InTouch.TaskService.Common.Entities.TaskModels.InputModels;

namespace InTouch.TaskService.Common.Entities.Validators;

public class TaskValidator : AbstractValidator<TaskInputModel>
{
    public TaskValidator()
    {
        RuleFor(tv => tv.Name).NotNull().NotEmpty();
        RuleFor(tv => tv.Description).NotNull().NotEmpty();
        RuleFor(tv => tv.Author).NotNull().NotEmpty();
    }
}