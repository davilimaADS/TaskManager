using FluentValidation;
using System.Data;

namespace TaskManager.Application.Validators.ProjectValidator
{
    public class GetProjectByIdValidator : AbstractValidator<Guid>
    {
        public GetProjectByIdValidator()
        {
            RuleFor(id => id)
           .NotEmpty().WithMessage("The project ID is mandatory.")
           .NotEqual(Guid.Empty).WithMessage("The ID cannot be empty.");
        }
    }
}
