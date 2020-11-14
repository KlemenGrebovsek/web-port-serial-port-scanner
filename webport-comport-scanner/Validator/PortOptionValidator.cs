using FluentValidation;
using webport_comport_scanner.Model;
using webport_comport_scanner.Option;

namespace webport_comport_scanner.Validator
{
    /// <summary>
    /// Provides validation for program options.
    /// </summary>
    public class PortOptionValidator: AbstractValidator<ProgramOptions>
    {
        public PortOptionValidator()
        {
            RuleFor(o => o.MinPort).InclusiveBetween(0, 65535);
            RuleFor(o => o.MaxPort).InclusiveBetween(0, 65535);
            RuleFor(o => o.Status).IsEnumName(typeof(PortStatus), false);
        }
    }
}