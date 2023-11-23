using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoreLib.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiCoreApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        //[NotifyPropertyChangedFor(nameof(IsNotBusy), nameof(CanScrape))]
        private bool isBusy;
        public bool IsNotBusy => !IsBusy;
        /// <summary>
        /// List of all the individuals
        /// </summary>
        [ObservableProperty]
        List<Individual> individuals;

        [RelayCommand]
        async Task RemoveIndividual(Individual individual)
        {
            await Task.CompletedTask;
        }

        [RelayCommand]
        async Task NavigateToEditIndividual()
        {
            await Task.CompletedTask;
        }

        [RelayCommand]
        async Task NavigateToAddIndividual()
        {
            await Task.CompletedTask;
        }

        public MainViewModel()
        {

        }
    }
}
