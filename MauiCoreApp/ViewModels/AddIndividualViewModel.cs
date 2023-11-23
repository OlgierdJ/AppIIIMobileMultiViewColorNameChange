using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoreLib.Enums;
using CoreLib.Models;
using CoreLib.WebApi;

namespace MauiCoreApp.ViewModels
{
    public partial class AddIndividualViewModel : ViewModelBase
    {
        [ObservableProperty]
        Individual newIndividual;
        [ObservableProperty]
        List<RelativeType> relativeTypes;
        public AddIndividualViewModel(SignalRClientService signalRClientService, IndividualWebApi webApi) : base(signalRClientService, webApi)
        {
            Disposing = () =>
            {
                signalRClientService.UpdateIndividualMessageReceived -= SignalRClientService_UpdateIndividualMessageReceived;
                signalRClientService.Dispose();
            };
            signalRClientService.UpdateIndividualMessageReceived += SignalRClientService_UpdateIndividualMessageReceived;
            RelativeTypes = new List<RelativeType>() { RelativeType.Mother, RelativeType.Father, RelativeType.Dog, RelativeType.Cat };
            NewIndividual = new()
            {
                Color = new Color().ToHex()
            };
        }

        [RelayCommand]
        async Task NavigateBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task Create()
        {
            var individual = new Individual()
            {
                 Color = NewIndividual.Color,
                  Name = NewIndividual.Name,
                   RelativeType = NewIndividual.RelativeType,
            };
            var res = await webApi.Create(individual);
            Reset();
            if (res == null)
            {

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                string text = $"Failed to create {individual.Name}";
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;

                var toast = Toast.Make(text, duration, fontSize);

                await toast.Show(cancellationTokenSource.Token);
            }
        }

        private async void Reset()
        {
            NewIndividual = new() { Color = new Color().ToHex()};
        }

        private async void SignalRClientService_UpdateIndividualMessageReceived(Individual arg1, CoreLib.Enums.EntityAction arg2)
        {
            switch (arg2)
            {
                case CoreLib.Enums.EntityAction.Create:
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                    string text = $"Successfully created {arg1.Name}";
                    ToastDuration duration = ToastDuration.Short;
                    double fontSize = 14;

                    var toast = Toast.Make(text, duration, fontSize);

                    await toast.Show(cancellationTokenSource.Token);
                    break;
                case CoreLib.Enums.EntityAction.Update:
                    break;
                case CoreLib.Enums.EntityAction.Delete:
                    break;
            }
        }
    }
}
