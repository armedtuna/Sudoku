using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace DataParsers.TextFile.Validators
{
    public class ContentsArrayValidator : AbstractValidator<IEnumerable<string>>
    {
        private const int maxRows = 9;
        private const int maxColumns = 9;

        public ContentsArrayValidator()
        {
            RuleFor(contents => contents)
                .NotNull()
                .WithMessage("Contents array is required.");
            RuleFor(contents => contents)
                .Must(contents => contents.Count() <= maxRows)
                .WithMessage(contents => $"Contents array must have {maxRows} or less rows, found: {contents.Count()}");
            RuleForEach(contents => contents)
                .Must(content => content.Length <= maxColumns)
                .WithMessage($"Contents array must have {maxColumns} or less columns.");
            RuleForEach(contents => contents)
                .Must(BeValidValues)
                .WithMessage($"Contents array values must from 1 to 9 or a space.");
        }

        private bool BeValidValues(string content)
        {
            foreach (var c in content)
            {
                if (!BeValidValue(c))
                {
                    return false;
                }
            }

            return true;
        }

        private bool BeValidValue(char c)
        {
            return "123456789 ".Contains(c);
        }
    }
}