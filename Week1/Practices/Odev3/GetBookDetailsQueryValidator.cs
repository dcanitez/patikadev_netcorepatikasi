using FluentValidation;
using WebApi.BookOperations.GetBookDetails;

namespace WebApi.Odev3
{
    public class GetBookDetailsQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookDetailsQueryValidator()
        {
            RuleFor(cmd => cmd.BookId).GreaterThan(0);
        }
    }
}