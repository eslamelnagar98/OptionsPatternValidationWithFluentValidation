using FluentValidation;

namespace OptionsPatternStudy;
public class ExampleOptionsValidator : AbstractValidator<ExampleOptions>
{
	public ExampleOptionsValidator()
	{
		RuleFor(x => x.LogLevel)
			.IsEnumName(typeof(LogLevel), caseSensitive: false);

		RuleFor(x => x.Retries)
			.InclusiveBetween(1, 9);

	}
}
