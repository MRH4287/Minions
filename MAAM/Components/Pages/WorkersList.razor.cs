using MAAM.Components.Dialog;
using MAAM.Models;
using MAAM.Models.Persistence;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Linq.Expressions;

namespace MAAM.Components.Pages
{
    public partial class WorkersList
    {
#if DEBUG
        public bool IsDebug = true;
#else
        public bool IsDebug = false;
#endif
        [Parameter]
        public string? AssetId { get; set; }

        public Asset Asset { get; set; } = new Asset();

        public WorkerListColumnPersistence WorkerColumnPersistence { get; set; } = new WorkerListColumnPersistence();
        public GenericWorkerListColumnPersistence GenericWorkerColumnPersistence { get; set; } = new GenericWorkerListColumnPersistence();
        private IDisposable? _columnSubscription;

        public double DailyPayment => double.Round(Asset.Payment + Asset.UnnamedSailorPayment + Asset.UnnamedRowerPayment, 2);

        public double CurrentPayment => double.Round(Asset.CurrentPayment + Asset.UnnamedSailorCurrentPayment + Asset.UnnamedRowerCurrentPayment, 2);

        public int Broke => (int)double.Floor((Asset.Money - CurrentPayment) / DailyPayment);


        protected override async Task OnInitializedAsync()
        {
            var assets = await Repo.GetAll();
            await SetAssetById(assets);
        }

        private async Task SetAssetById(IEnumerable<Asset> assets)
        {
            if (string.IsNullOrEmpty(AssetId))
            {
                Asset = assets.First();
                return;
            }

            var found = assets.FirstOrDefault(Asset => Asset.Id == AssetId);
            if (found == null)
            {
                var newAsset = new Asset()
                {
                    Id = AssetId,
                    Name = AssetId
                };
                await Repo.Save(newAsset);
                Asset = newAsset;
                return;
            }

            Asset = found;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                WorkerColumnPersistence = await PersistenceService.GetValue(WorkerColumnPersistence);
                GenericWorkerColumnPersistence = await PersistenceService.GetValue(GenericWorkerColumnPersistence);
                _columnSubscription = PersistenceService.Register(WorkerColumnPersistence);
                _columnSubscription = PersistenceService.Register(GenericWorkerColumnPersistence);
                StateHasChanged();
            }
        }

        public void Dispose()
        {
            _columnSubscription?.Dispose();
        }

        public async Task Fire(Worker element)
        {
            Asset.Workers.Remove(element);

            await Repo.Save(Asset);
            //StateHasChanged();
        }
        public async Task Pay(BaseWorker element)
        {
            Asset.Money = Asset.Money - element.CurrentPayment;
            element.DayWithoutPay = 0;
            await Repo.Save(Asset);
        }

        public async Task Time()
        {

            AssetService.AddTimeCat(Asset, 1);
            await Repo.Save(Asset);
        }

        private async Task EditChar(Worker element)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true, CloseButton = true };
            var parameter = new DialogParameters<CharacterDialog>
            {
                { x => x.Element, element },
                { x => x.AssetId, Asset.Id }
            };


            var dialog = await Dialog.ShowAsync<CharacterDialog>("", parameter, options);
            var result = await dialog.Result;
            await Repo.Save(Asset);
        }
        private async Task EditChar(GenericWorker element)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true, CloseButton = true };
            var parameter = new DialogParameters<GenericCharacterDialog>
            {
                { x => x.Element, element },
                { x => x.AssetId, Asset.Id }
            };


            var dialog = await Dialog.ShowAsync<GenericCharacterDialog>("", parameter, options);
            var result = await dialog.Result;
            await Repo.Save(Asset);
        }

        private async Task AddChar()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true, CloseButton = true };
            var parameter = new DialogParameters<CharacterDialog>();
            var element = new Worker();


            parameter.Add(x => x.Element, element);
            parameter.Add(x => x.AssetId, Asset.Id);




            var dialog = await Dialog.ShowAsync<CharacterDialog>("", parameter, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                Asset.Workers.Add(element);
                await Repo.Save(Asset);
            }

        }

        private async Task AddGenericWorkers()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true, CloseButton = true };
            var parameter = new DialogParameters<GenericCharacterDialog>();
            var element = new GenericWorker();

            parameter.Add(x => x.Element, element);
            parameter.Add(x => x.AssetId, Asset.Id);

            var dialog = await Dialog.ShowAsync<GenericCharacterDialog>("", parameter, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                Asset.GenericWorkers.Add(element);
                await Repo.Save(Asset);
            }
        }

        public async Task AddMoney(double money)
        {
            Asset.Money += money;
            await Repo.Save(Asset);
        }

    }
}
