using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoreLib.Models;
using CoreLib.WebApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiCoreApp.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// List of all the individuals
        /// </summary>
        [ObservableProperty]
        List<Individual> individuals;
        [ObservableProperty]
        Individual individual;

       

        [RelayCommand]
        async Task NavigateToEditIndividual(object param)
        {
            await Shell.Current.GoToAsync("EditIndividualPage", new Dictionary<string, object>() 
            {
                { "Individual", JsonSerializer.Deserialize<Individual>(JsonSerializer.Serialize(param)) }
            });
        }

        [RelayCommand]
        async Task NavigateToAddIndividual()
        {
            await Shell.Current.GoToAsync("AddIndividualPage");
        }

        public MainViewModel(SignalRClientService signalRClientService, IndividualWebApi webApi) : base(signalRClientService, webApi)
        {
            Task.Run(RefreshList);
            Disposing = () =>
            {
                signalRClientService.UpdateIndividualMessageReceived -= SignalRClientService_UpdateIndividualMessageReceived;
                signalRClientService.Dispose();
            };
            signalRClientService.UpdateIndividualMessageReceived += SignalRClientService_UpdateIndividualMessageReceived;
            Task.Run(signalRClientService.Connect);
        }

        [RelayCommand]
        async Task RefreshList()
        {
            var res = await webApi.GetAll();
            if (res != null) 
            { 
                Individuals = res; 
            }
            else
            {
                Individuals = new();
            }
        }

        private void SignalRClientService_UpdateIndividualMessageReceived(Individual arg1, CoreLib.Enums.EntityAction arg2)
        {
            switch (arg2)
            {
                case CoreLib.Enums.EntityAction.Create:
                    Individuals.Add(arg1);
                    break;
                case CoreLib.Enums.EntityAction.Update:
                    Individuals.Remove(Individuals.First(e=>e.Id == arg1.Id));
                    Individuals.Add(arg1);
                    break;
                case CoreLib.Enums.EntityAction.Delete:
                    Individuals.Remove(arg1);
                    break;
            }
        }
    }
}
