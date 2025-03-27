using FluentValidation;
using ToDoApp.Application.DTOs;

namespace ToDoApp.Application.Validations;

public class TaskValidator : AbstractValidator<TaskDto>
{
    public TaskValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El título es obligatorio")
            .MinimumLength(3).WithMessage("El título debe tener al menos 3 caracteres");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres");

        RuleFor(x => x.IsCompleted)
            .NotNull().WithMessage("El estado de la tarea es obligatorio");

        RuleFor(x => x.CompletedAt)
            .GreaterThanOrEqualTo(x => x.CreatedAt)
            .When(x => x.IsCompleted)
            .WithMessage("La fecha de finalización no puede ser menor a la de creación.");
    }
}
