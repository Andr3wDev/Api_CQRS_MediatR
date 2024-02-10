using FluentValidation;

namespace Application.Features.Books.Command.CreateBook;

public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
{
    public CreateBookRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(80);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.PublishDate)
            .NotEmpty();

        RuleFor(x => x.Publisher)
            .NotEmpty()
            .MaximumLength(60);

        RuleFor(x => x.NumPages)
            .NotEmpty();

        RuleFor(x => x.AuthorId)
            .NotEmpty();
    }
}