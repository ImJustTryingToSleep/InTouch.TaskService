using FluentValidation;
using InTouch.TaskService.Common.Entities.TaskModels.UpdateModels;

namespace InTouch.TaskService.Common.Entities.Validators;

public class TaskUpdateValidator : AbstractValidator<TaskUpdateModel>
{
    public TaskUpdateValidator()
    {
        RuleFor(tv => tv.Name).NotNull().NotEmpty();
        RuleFor(tv => tv.ColumnId).NotNull().NotEmpty();
        RuleFor(tv => tv.Description).NotNull().NotEmpty();
        RuleFor(tv => tv.EndDate).NotNull().NotEmpty();
    }
}