using System.Threading.Tasks;

namespace webport_comport_scanner.ArgumentParsers
{
    public interface IArgumentParser
    {
        /// <summary>
        /// Parses arguments and starts executing command.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        Task ParseAsync (string[] args);
    }
}