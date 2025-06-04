using FluentValidation;
using TaskManager.Communication.Request.TaskRequest;

namespace TaskManager.Application.Validators.TaskValidator
{
    public class UpdateTaskValidator : AbstractValidator<UpdateTaskRequest>
    {
        public UpdateTaskValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage("Title is required.")
               .MaximumLength(100).WithMessage("Title must have at most 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must have at most 500 characters.");

            RuleFor(x => x.TaskType)
                .IsInEnum().WithMessage("Invalid task type.");
        }
    }
}
