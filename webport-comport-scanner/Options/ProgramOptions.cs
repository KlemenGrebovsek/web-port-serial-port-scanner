using MatthiWare.CommandLine.Core.Attributes;
using FluentValidation;


namespace webport_comport_scanner.Options
{
    public class ProgramOptions
    {
        [Name("f", "from")]
        [Description("Program scans ports that are equal or greater than this value (0 - 65535).")]
        [DefaultValue(0)]
        public int MinPort { get; set; }

        [Name("t", "to")]
        [Description("Program scans ports that are equal or less than this value (0 - 65535).")]
        [DefaultValue(65535)]
        public int MaxPort { get; set; }

    }

    public class ProgramOptionValidator : AbstractValidator<ProgramOptions>
    {
        public ProgramOptionValidator()
        {
            RuleFor(o => o.MinPort).InclusiveBetween(0, 65535);
            RuleFor(o => o.MaxPort).InclusiveBetween(0, 65535);
        }
    }
}
