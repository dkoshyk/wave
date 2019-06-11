using System.Threading.Tasks;

namespace SwissKnifeDotNetCore.Commands
{
    public interface ICommandService
    {
        Task SaveProduct(string name);
    }
}
