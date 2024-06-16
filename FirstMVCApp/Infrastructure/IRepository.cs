namespace FirstMVCApp.Infrastructure
{
    public interface IRepository<TEntity,TIdentity>
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetBy(string filterCriteria);
        TEntity GetById(TIdentity id);
        void CreateNew(TEntity item);
        void Update(TEntity item);
        void Remove(TIdentity id);
    }
}
