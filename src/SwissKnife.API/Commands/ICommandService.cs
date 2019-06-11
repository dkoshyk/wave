using System.Threading.Tasks;

namespace SwissKnife.API.Commands
{
    public interface ICommandService
    {
        Task SaveProduct(string name);
    }
}