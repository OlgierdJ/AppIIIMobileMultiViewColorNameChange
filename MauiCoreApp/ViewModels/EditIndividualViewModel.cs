using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoreLib.Enums;
using CoreLib.Models;
using CoreLib.WebApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MauiCoreApp.ViewModels
{
    [QueryProperty(nameof(Individual), "Individual")]
    public partial class EditIndividualViewModel : ViewModelBase
    {
        [ObservableProperty]
        Individual individual;
        [ObservableProperty]
        List<RelativeType> relativeTypes;
       Color color;

        public float Red
        {
            get { return color.Red; }
            set
            {
                if (color.Red != value)
                {
                    Color = new Color(value, color.Green, color.Blue);
                }
            }
        }

        public float Green
        {
            get { return color.Green; }
            set
            {
                if (color.Green != value)
                {
                    Color = new Color(color.Red, value, color.Blue);
                }
            }
        }

        public float Blue
        {
            get { return color.Blue; }
            set
            {
                if (color.Blue != value)
                {
                    Color = new Color(color.Red, color.Green, value);
                }
            }
        }
        public Color Color
        {
            get 
            {
                if (color == null)
                {
                    color = new Color();
                }
                return color;
            }
            set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged(nameof(Red));
                    OnPropertyChanged(nameof(Green));
                    OnPropertyChanged(nameof(Blue));
                    OnPropertyChanged(nameof(Color));
                }
            }
        }
        
        public EditIndividualViewModel(SignalRClientService signalRClientService, IndividualWebApi webApi) : base(signalRClientService, webApi)
        {
            Disposing = () =>
            {
                signalRClientService.UpdateIndividualMessageReceived -= SignalRClientService_UpdateIndividualMessageReceived;
                signalRClientService.Dispose();
            };
            signalRClientService.UpdateIndividualMessageReceived += SignalRClientService_UpdateIndividualMessageReceived;
            RelativeTypes = new List<RelativeType>() { RelativeType.Mother, RelativeType.Father, RelativeType.Dog, RelativeType.Cat };
        }
        public async void Initialize()
        {
            if (Individual!=null)
            {
            Color = Color.FromArgb(Individual.Color);

            }
        }

        [RelayCommand]
        async Task Delete()
        {
            var res = await webApi.Delete(Individual);
            if (res == null)
            {

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                string text = $"Failed to delete {Individual.Name}";
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;

                var toast = Toast.Make(text, duration, fontSize);

                await toast.Show(cancellationTokenSource.Token);
            }
        }

        [RelayCommand]
        async Task NavigateBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task SaveChanges()
        {

            Individual.Color = Color.ToHex();
            var res = await webApi.Update(Individual);
            if (res == null) 
            {

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                string text = $"Failed to update {Individual.Name}";
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;

                var toast = Toast.Make(text, duration, fontSize);

                await toast.Show(cancellationTokenSource.Token);
            }
        }

        private async void SignalRClientService_UpdateIndividualMessageReceived(Individual arg1, EntityAction action)
        {
            switch (action)
            {
                case CoreLib.Enums.EntityAction.Create:
                    break;
                case CoreLib.Enums.EntityAction.Update:
                    if (arg1.Id == Individual.Id)
                    {
                        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                        string text = $"Successfully updated {arg1.Name}";
                        ToastDuration duration = ToastDuration.Short;
                        double fontSize = 14;

                        var toast = Toast.Make(text, duration, fontSize);

                        await toast.Show(cancellationTokenSource.Token);
                    }
                    break;
                case CoreLib.Enums.EntityAction.Delete:
                    if (arg1.Id == Individual.Id)
                    {
                        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                        string text = $"Successfully deleted {arg1.Name}";
                        ToastDuration duration = ToastDuration.Short;
                        double fontSize = 14;

                        var toast = Toast.Make(text, duration, fontSize);

                        await toast.Show(cancellationTokenSource.Token);
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await Task.Delay(200);
                            await Shell.Current.GoToAsync("..");
                        });
                    }
                    break;
            }
        }
    }
}
