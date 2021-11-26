using System.Threading.Tasks;

namespace Skeleton.Data.UOW
{
    public interface IUnitOfWork
    {
        Task CommmitAsync();

        void Commit();
    }
}
