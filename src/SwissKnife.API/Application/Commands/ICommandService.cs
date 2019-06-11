using System.Threading.Tasks;

namespace SwissKnife.API.Application.Commands
{
    public interface ICommandService
    {
        Task SaveProduct(string name);
    }
}