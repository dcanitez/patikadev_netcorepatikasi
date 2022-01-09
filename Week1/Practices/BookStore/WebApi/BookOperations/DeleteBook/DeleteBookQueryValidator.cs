using FluentValidation;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookQueryValidator : AbstractValidator<DeleteBookQuery>
    {
        public DeleteBookQueryValidator()
        {
            RuleFor(cmd=>cmd.BookId).GreaterThanOrEqualTo(0);
        }
    }
}