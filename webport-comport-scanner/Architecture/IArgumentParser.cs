namespace webport_comport_scanner.Architecture
{
    public interface IArgumentParser
    {
        /// <summary>
        /// Parses given arguments and starts exectuting commands.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        void Parse(string[] args);
    }
}