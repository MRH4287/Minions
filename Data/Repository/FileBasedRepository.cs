using DataAccess.Contracts;
using DataAccess.Models;
using Microsoft.Extensions.Options;
using System.Reflection;
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

        private bool _useStrictTypeName = false;
        private readonly IOptions<MinionsDataOptions> _options;

        public TKey? CollectionId { get; protected set; } = default;

        public FileBasedCollectionRepository(IOptions<MinionsDataOptions> options)
        {
            _typeName = GetTypeName();

            _basePath = (options.Value.BasePath ?? "./data") + "/" + _typeName;
            _options = options;
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

        public void SetCollectionId(TKey? collectionId)
        {
            CollectionId = collectionId;
        }

        public void SetStrictTypeName(bool strictTypeName)
        {
            _useStrictTypeName = strictTypeName;

            _typeName = GetTypeName();
            _basePath = (_options.Value.BasePath ?? "./data") + "/" + _typeName;
        }

        public async Task<TValue?> Get(string id, bool includeWebIgnore = false) => await Get(CollectionId, id, includeWebIgnore);

        public async Task<TValue?> Get(TKey? collectionId, string id, bool includeWebIgnore = false)
        {
            ValidatePath(collectionId);
            var file = GetFilePath(collectionId, id);

            if (!File.Exists(file))
            {
                return null;
            }

            TValue? result = await GetItemByFilePath(file, includeWebIgnore);

            return result;
        }

        public async Task<IEnumerable<TValue>> GetAll(bool includeWebIgnore = false) => await GetAll(CollectionId, includeWebIgnore);
        public async Task<IEnumerable<TValue>> GetAll(TKey? collectionId, bool includeWebIgnore = false)
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
                var element = await GetItemByFilePath(item, includeWebIgnore);
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
        {
            var name = new StringBuilder(id);
            foreach (var invalidChar in Path.GetInvalidFileNameChars())
            {
                name.Replace(invalidChar, '_');
            }

            return name.ToString() + ".json";

        }

        private static async Task<TValue?> GetItemByFilePath(string file, bool includeWebIgnore)
        {
            var content = await File.ReadAllTextAsync(file);
            var result = System.Text.Json.JsonSerializer.Deserialize<TValue>(content);

            PatchFields(result, includeWebIgnore);

            return result;
        }

        private string GetTypeName()
        {
            var type = typeof(TValue);
            if (_useStrictTypeName)
            {
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
            return type.Name;
        }

        private static void PatchFields(IModel? item, bool includeWebIgnore)
        {
            if (item == null || includeWebIgnore)
            {
                return;
            }

            var type = item.GetType();
            var properties = type.GetProperties();

            var ignoredProperties = properties.Where(prop => prop.GetCustomAttribute<WebIgnoreAttribute>() != null);
            foreach (var prop in ignoredProperties)
            {
                prop.SetValue(item, null);
            }

            var childModels = properties.Where(prop => typeof(IModel).IsAssignableFrom(prop.PropertyType));
            foreach (var childModel in childModels)
            {
                var child = childModel.GetValue(item) as IModel;
                PatchFields(child, includeWebIgnore);
            }
        }
    }

}
