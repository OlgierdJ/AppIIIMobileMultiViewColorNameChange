using CommunityToolkit.Mvvm.ComponentModel;
using CoreLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiCoreApp.ViewModels
{
    public partial class AddIndividualViewModel : ObservableObject
    {
        [ObservableProperty]
        Individual individual;
        public AddIndividualViewModel()
        {
        }
    }
}
