using FluentValidation;
using Microsoft.Extensions.Options;
using OptionsPatternStudy;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);
builder.Services
    .AddOptions<ExampleOptions>()
    .Bind(builder.Configuration.GetSection(ExampleOptions.SectionName))
    .ValidateFluently()
    //.Validate(validateRetries)
    .ValidateOnStart();
var app = builder.Build();

app.MapGet("hello", (IOptions<ExampleOptions> options) => options.Value);

app.Run();


bool validateRetries(ExampleOptions exampleOptions)
{
    return exampleOptions.Retries is < 0 or > 10 ? false : true;
}
