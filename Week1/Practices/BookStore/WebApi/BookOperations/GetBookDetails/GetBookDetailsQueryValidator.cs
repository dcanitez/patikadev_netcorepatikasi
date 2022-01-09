using FluentValidation;

namespace WebApi.BookOperations.GetBookDetails
{
    public class GetBookDetailsQueryValidator:AbstractValidator<GetBookByIdQuery>
    {
        public GetBookDetailsQueryValidator()
        {
            RuleFor(cmd=>cmd.BookId).GreaterThan(0);
        }
    }
}