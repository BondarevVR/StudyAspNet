using System.Threading.Tasks;

namespace App.Persistance
{
    public interface IUnitOfwork
    {
        Task CompleteAsync();
    }
}