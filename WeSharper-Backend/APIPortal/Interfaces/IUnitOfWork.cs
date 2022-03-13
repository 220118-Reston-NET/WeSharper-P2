using System.Threading.Tasks;

namespace WeSharper.APIPortal.Interfaces
{
    public interface IUnitOfWork
    {
        IMessageManagement MessageManagement { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}