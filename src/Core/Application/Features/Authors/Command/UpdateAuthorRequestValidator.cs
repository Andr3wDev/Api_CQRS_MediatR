using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Features.Authors.Command.UpdateAuthor;

public class UpdateAuthorRequestValidator : AbstractValidator<UpdateAuthorRequest>
{
    public UpdateAuthorRequestValidator(IBookRepository bookRepo)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(75);
    }
}