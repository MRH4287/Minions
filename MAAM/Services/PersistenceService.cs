using Blazored.LocalStorage;
using MAAM.Models.Persistence;
using MudBlazor.Interfaces;
using System.Reactive.Linq;
using System.Text.Json;

namespace MAAM.Services
{
    public class PersistenceService
    {
        private readonly ILocalStorageService _localStorageService;

        public PersistenceService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }


        public IDisposable Register<T>(T item)
            where T : PersistenceBase, new()
        {
            if (item == null || string.IsNullOrEmpty(item.PersistenceKey))
            {
                throw new ArgumentNullException(nameof(item));
            }

            var subscription = item.PropertyChanged.Throttle(TimeSpan.FromSeconds(1)).Subscribe(async (_) =>
            {
                await Save(item);
            });


            return subscription;
        }

        public async Task<T> GetValue<T>(T initial)
            where T : PersistenceBase, new()
        {
            var result = await Load<T>(initial.PersistenceKey);

            return result;
        }

        private async Task Save<T>(T item)
            where T : PersistenceBase
        {
            if (item == null || string.IsNullOrEmpty(item.PersistenceKey))
            {
                throw new ArgumentNullException(nameof(item));
            }

            var key = item.PersistenceKey;
            try
            {
                item.IgnoreChanged = true;

                await _localStorageService.SetItemAsync(key, item);
            }
            finally
            {
                item.IgnoreChanged = false;
            }


        }

        private async Task<T> Load<T>(string key)
            where T : PersistenceBase, new()
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            try
            {
                var item = await _localStorageService.GetItemAsync<T>(key);
                var result = item ?? new T();

                result.IgnoreChanged = false;

                return result;
            }
            catch
            {
                var result = new T();
                result.IgnoreChanged = false;
                return result;
            }
        }
    }
}
