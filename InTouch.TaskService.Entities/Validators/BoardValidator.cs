using FluentValidation;
using InTouch.TaskService.Common.Entities.TaskBoards.InputModels;

namespace InTouch.TaskService.Common.Entities.Validators;

public class BoardValidator : AbstractValidator<BoardInputModel>
{
    public BoardValidator()
    {
        RuleFor(bv => bv.Name).NotNull().NotEmpty();
        RuleFor(bv => bv.Author).NotNull().NotEmpty();
    }
}