using FluentValidation;
using InTouch.TaskService.Common.Entities.ColumnModels.InputModels;

namespace InTouch.TaskService.Common.Entities.Validators;

public class ColumnValidator : AbstractValidator<ColumnInputModel>
{
    public ColumnValidator()
    {
        RuleFor(cv => cv.Name).NotNull().NotEmpty();
    }
}