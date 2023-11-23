using CommunityToolkit.Mvvm.ComponentModel;
using CoreLib.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiCoreApp.ViewModels
{
    public abstract partial class ViewModelBase : ObservableObject, IDisposable
    {
        protected readonly SignalRClientService signalRClientService;
        protected readonly IndividualWebApi webApi;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool isBusy;
        public bool IsNotBusy => !IsBusy;
        public ViewModelBase(SignalRClientService signalRClientService, IndividualWebApi webApi)
        {
            this.signalRClientService = signalRClientService;
            this.webApi = webApi;
        }

        public Action Disposing { get; set; }
        public void Dispose()
        {
            Disposing();
        }
    }
}
