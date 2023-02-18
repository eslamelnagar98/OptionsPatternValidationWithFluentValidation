using System.ComponentModel.DataAnnotations;

namespace OptionsPatternStudy;
public class ExampleOptions
{
    public const string SectionName = "Example";
    [EnumDataType(typeof(LogLevel))]
    public required string LogLevel { get; init; }
    [Range(1,9)]
    public required int Retries { get; init; }
}
