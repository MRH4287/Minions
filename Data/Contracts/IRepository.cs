namespace DataAccess.Contracts
{
    public interface IRepository<T> where T : class, IModel
    {
        void Delete(string id);
        bool Exists(string id);
        Task<T?> Get(string id, bool includeWebIgnore = false);
        Task<IEnumerable<T>> GetAll(bool includeWebIgnore = false);
        Task Save(T item);
    }

    public interface ICollectionRepository<TValue, TKey> : IRepository<TValue>
        where TValue: class, IModel
    {
        TKey? CollectionId { get; }

        void SetCollectionId(TKey collectionId);

        void Delete(TKey collectionId, string id);

        bool Exists(TKey collectionId, string id);

        Task<TValue?> Get(TKey collectionId, string id, bool includeWebIgnore = false);

        Task<IEnumerable<TValue>> GetAll(TKey collectionId, bool includeWebIgnore = false);

        Task Save(TKey collectionId, TValue item);
    }

    public interface ICollectionRepository<TValue> : ICollectionRepository<TValue, string>
            where TValue: class, IModel
    {
    }
}