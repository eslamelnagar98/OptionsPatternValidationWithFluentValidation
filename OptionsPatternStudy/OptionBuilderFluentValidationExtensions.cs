using FluentValidation;
using Microsoft.Extensions.Options;

namespace OptionsPatternStudy;
public static class OptionBuilderFluentValidationExtensions
{
    public static OptionsBuilder<TOptions> ValidateFluently<TOptions>(this OptionsBuilder<TOptions> optionsBuilder)
        where TOptions : class
    {
        optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(servceProvide =>
            new FluentValidationOptions<TOptions>(optionsBuilder.Name, servceProvide.GetRequiredService<IValidator<TOptions>>()));
        return optionsBuilder;
    }
}

public class FluentValidationOptions<TOptions> : IValidateOptions<TOptions>
    where TOptions : class
{
    private readonly IValidator<TOptions> _validator;
    public FluentValidationOptions(string? name, IValidator<TOptions> validator)
    {
        Name = name;
        _validator = validator;
    }

    /// <summary>
    /// The options name.
    /// </summary>
    public string? Name { get; }


    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (Name != null && Name != name)
        {
            return ValidateOptionsResult.Skip;
        }

        ArgumentNullException.ThrowIfNull(options);

        var validatorResult = _validator.Validate(options);
        var errors = validatorResult.Errors.Select(x =>
        $"Options Validation Failed For {x.PropertyName} With Error {x.ErrorMessage}");
        return validatorResult.IsValid ? ValidateOptionsResult.Success : ValidateOptionsResult.Fail(errors);
      
    }
}