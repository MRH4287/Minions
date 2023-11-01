using DataAccess.Contracts;
using DataAccess.Models;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Repository
{
    internal class FileBasedRepository<T> : IRepository<T> where T : class, IModel
    {

        private string _typeName;
        private string _basePath;


        public FileBasedRepository(IOptions<MinionsDataOptions> options)
        {
            _typeName = GetTypeName();

            _basePath = (options.Value.BasePath ?? "./data") + "/" + _typeName;
        }

        private void ValidatePath()
        {
            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }
        }

        public async Task<T?> Get(string id)
        {
            ValidatePath();
            var file = GetFilePath(id);

            if (!File.Exists(file))
            {
                return null;
            }

            T? result = await GetItemByFilePath(file);

            return result;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            ValidatePath();
            var files = Directory.GetFiles(_basePath, "*.json");
            var result = new List<T>();
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

        public async Task Save(T item)
        {
            ValidatePath();
            var json = System.Text.Json.JsonSerializer.Serialize(item);
            var filePath = GetFilePath(item.Id);

            await File.WriteAllTextAsync(filePath, json);
        }

        public void Delete(string id)
        {
            ValidatePath();
            var filePath = GetFilePath(id);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private string GetFilePath(string id)
        {
            return Path.Combine(_basePath, GetFileName(id));
        }

        private static string GetFileName(string id)
         => id + ".json";

        private static async Task<T?> GetItemByFilePath(string file)
        {
            var content = await File.ReadAllTextAsync(file);
            var result = System.Text.Json.JsonSerializer.Deserialize<T>(content);
            return result;
        }

        private static string GetTypeName()
        {
            var type = typeof(T);
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
