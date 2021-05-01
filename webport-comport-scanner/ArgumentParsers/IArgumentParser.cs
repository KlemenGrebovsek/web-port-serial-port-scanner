using System.Threading.Tasks;

namespace webport_comport_scanner.ArgumentParsers
{
    public interface IArgumentParser
    {
        Task ParseAsync (string[] args);
    }
}