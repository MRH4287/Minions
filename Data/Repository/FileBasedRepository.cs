using DataAccess.Contracts;
using DataAccess.Models;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Repository
{
    internal class FileBasedRepository<TValue> : FileBasedCollectionRepository<TValue, string>, ICollectionRepository<TValue>
        where TValue : class, IModel
    {
        public FileBasedRepository(IOptions<MinionsDataOptions> options) : base(options)
        {
        }
    }

    internal class FileBasedCollectionRepository<TValue, TKey> : ICollectionRepository<TValue, TKey>
        where TValue : class, IModel
    {

        private string _typeName;
        private string _basePath;

        public TKey? CollectionId { get; protected set; } = default;

        public FileBasedCollectionRepository(IOptions<MinionsDataOptions> options)
        {
            _typeName = GetTypeName();

            _basePath = (options.Value.BasePath ?? "./data") + "/" + _typeName;
        }

        private void ValidatePath(TKey? collectionId)
        {
            collectionId ??= CollectionId;

            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }

            if (collectionId is not null)
            {
                var newPath = Path.Combine(_basePath, collectionId.ToString() ?? "");
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
            }
        }

        public void SetCollectionId(TKey collectionId)
        {
            CollectionId = collectionId;
        }

        public async Task<TValue?> Get(string id) => await Get(CollectionId, id);

        public async Task<TValue?> Get(TKey? collectionId, string id)
        {
            ValidatePath(collectionId);
            var file = GetFilePath(collectionId, id);

            if (!File.Exists(file))
            {
                return null;
            }

            TValue? result = await GetItemByFilePath(file);

            return result;
        }

        public async Task<IEnumerable<TValue>> GetAll() => await GetAll(CollectionId);
        public async Task<IEnumerable<TValue>> GetAll(TKey? collectionId)
        {
            ValidatePath(collectionId);
            var basePath = _basePath;
            if (collectionId is not null)
            {
                basePath = Path.Combine(_basePath, collectionId.ToString() ?? "");

            }
            var files = Directory.GetFiles(basePath, "*.json");
            var result = new List<TValue>();
            foreach (var item in files)
            {
                var element = await GetItemByFilePath(item);
                if (element is null)
                {
                    continue;
                }
                result.Add(element);
            }
            return result;
        }

        public async Task Save(TValue item) => await Save(CollectionId, item);
        public async Task Save(TKey? collectionId, TValue item)
        {
            ValidatePath(collectionId);
            var json = System.Text.Json.JsonSerializer.Serialize(item);
            var filePath = GetFilePath(collectionId, item.Id);

            await File.WriteAllTextAsync(filePath, json);
        }

        public void Delete(string id) => Delete(CollectionId, id);
        public void Delete(TKey? collectionId, string id)
        {
            ValidatePath(collectionId);
            var filePath = GetFilePath(collectionId, id);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public bool Exists(string id) => Exists(CollectionId, id);
        public bool Exists(TKey? collectionId, string id)
        {
            ValidatePath(collectionId);
            var filePath = GetFilePath(collectionId, id);
            return File.Exists(filePath);
        }

        private string GetFilePath(TKey? collectionId, string id)
        {
            var basePath = _basePath;
            if (collectionId is not null)
            {
                basePath = Path.Combine(_basePath, collectionId.ToString() ?? "");

            }
            var path = Path.Combine(basePath, GetFileName(id));

            return path;
        }

        private static string GetFileName(string id)
         => id + ".json";

        private static async Task<TValue?> GetItemByFilePath(string file)
        {
            var content = await File.ReadAllTextAsync(file);
            var result = System.Text.Json.JsonSerializer.Deserialize<TValue>(content);
            return result;
        }

        private static string GetTypeName()
        {
            var type = typeof(TValue);
            using var hasher = MD5.Create();
            var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(type.FullName ?? type.Name));
            var hashText = Convert.ToBase64String(hash);
            var name = $"{type.Name}_{hashText}";
            foreach (var item in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(item, '_');
            }
            return name;
        }

    }

}
