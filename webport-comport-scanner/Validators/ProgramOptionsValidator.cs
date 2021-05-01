using FluentValidation;
using webport_comport_scanner.Options;

namespace webport_comport_scanner.Validators
{
    public class ProgramOptionsValidator: AbstractValidator<ProgramOptions>
    {
        public ProgramOptionsValidator()
        {
            RuleFor(o => o.MinPort)
                .InclusiveBetween(0, 65535);
            
            RuleFor(o => o.MaxPort)
                .InclusiveBetween(0, 65535);
            
            RuleFor(o => o)
                .Must(PortRangeValid)
                .WithMessage("Invalid port range.");
        }

        private static bool PortRangeValid(ProgramOptions programOptions)
        {
            return programOptions.MaxPort > programOptions.MinPort;
        }
    }
}