namespace AppointmentSystem.Core.Interfaces.Repository
{
    using AppointmentSystem.Core.Entities.Base;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        Task<TEntity> GetByIdWithDeletedAsync(CancellationToken cancellationToken, params object[] id);

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
