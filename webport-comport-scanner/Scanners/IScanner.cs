using webport_comport_scanner.Options;

namespace webport_comport_scanner.Scanners
{
    interface IScanner
    {
        public void Scan(ProgramOptions options);
    }
}
