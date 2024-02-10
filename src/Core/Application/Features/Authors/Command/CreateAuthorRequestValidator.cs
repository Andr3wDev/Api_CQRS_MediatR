using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Features.Authors.Command.CreateAuthor;

public class CreateAuthorRequestValidator : AbstractValidator<CreateAuthorRequest>
{
    public CreateAuthorRequestValidator(IBookRepository bookRepo)
    {
        RuleFor(a => a.Name)
            .NotEmpty()
            .MaximumLength(75);

            /* Lookup already exists..
            .Must(async (name, ct) => await bookRepo....
                .WithMessage((_, name) => T["Author with name {0} already Exists.", name]);
            */
    }
}