using FluentValidation;
using webport_comport_scanner.Models;
using webport_comport_scanner.Options;

namespace webport_comport_scanner.Validators
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
        }
    }
}