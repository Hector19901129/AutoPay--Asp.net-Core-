using System.Threading.Tasks;

namespace AutoPay.Infrastructure.DataLayer
{
    public interface IUnitOfWork
    {
        void BeginTransaction();

        int SaveChanges();

        Task<int> SaveChangesAsync();

        void Commit();

        void Rollback();
    }
}

