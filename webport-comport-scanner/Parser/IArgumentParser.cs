using System.Collections.Generic;
using System.Threading.Tasks;

namespace webport_comport_scanner.Parser
{
    public interface IArgumentParser
    {
        /// <summary>
        /// Parses arguments and starts executing command.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        /// <returns>Collection of error messages if any.</returns>
        Task<IEnumerable<string>> ParseAsync (string[] args);
    }
}