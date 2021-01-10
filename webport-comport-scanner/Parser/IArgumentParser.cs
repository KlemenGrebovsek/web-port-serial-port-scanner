namespace webport_comport_scanner.Parser
{
    public interface IArgumentParser
    {
        /// <summary>
        /// Parse given arguments and execute command.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        void ParseAsync(string[] args);
    }
}