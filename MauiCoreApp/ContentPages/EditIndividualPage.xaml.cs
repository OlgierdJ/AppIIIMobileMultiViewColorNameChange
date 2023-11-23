using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MauiCoreApp.ViewModels;

namespace MauiCoreApp.ContentPages;

public partial class EditIndividualPage : ContentPage
{
	public EditIndividualPage(EditIndividualViewModel editIndividualViewModel)
	{
		InitializeComponent();
		this.BindingContext = editIndividualViewModel;
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        ((EditIndividualViewModel)this.BindingContext).Initialize();
    }
}