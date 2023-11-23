using MauiCoreApp.ViewModels;

namespace MauiCoreApp.ContentPages;

public partial class AddIndividualPage : ContentPage
{
	public AddIndividualPage(AddIndividualViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}