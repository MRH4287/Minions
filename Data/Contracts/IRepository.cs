namespace DataAccess.Contracts
{
    public interface IRepository<T> where T : class, IModel
    {
        void Delete(string id);
        Task<T?> Get(string id);
        Task<IEnumerable<T>> GetAll();
        Task Save(T item);
    }
}