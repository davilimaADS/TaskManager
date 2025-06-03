using FluentValidation;
using TaskManager.Communication.Request.ProjectRequest;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Validators.ProjectValidator
{
    public class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequest>
    {

        public UpdateProjectRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do projeto é obrigatório.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("A descrição do projeto é obrigatória.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Status inválido. Valores permitidos: InProgress, Completed, Cancelled.");
        }

    }
}
