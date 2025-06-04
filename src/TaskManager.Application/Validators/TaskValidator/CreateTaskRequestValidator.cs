using FluentValidation;
using TaskManager.Communication.Request.TaskRequest;

namespace TaskManager.Application.Validators.CreateValidator
{
    public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskRequestValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage("Title is mandatory.")
               .MaximumLength(100).WithMessage("The title must be a maximum of 100 characters."); 

            RuleFor(x => x.Description)
                .MaximumLength(int.MaxValue).WithMessage("Description too long."); 

            RuleFor(x => x.TaskType)
                .IsInEnum().WithMessage("Invalid task type.");

            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("ProjectId is mandatory.");
        }
    }
}
