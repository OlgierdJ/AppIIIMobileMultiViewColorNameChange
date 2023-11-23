using MauiCoreApp.ViewModels;

namespace MauiCoreApp.ContentPages
{
    public partial class MainPage : ContentPage
    {
        //int count = 0;

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = vm;
        }

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}

        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            await ((MainViewModel)BindingContext).RefreshListCommand.ExecuteAsync(null);
            //if (this.Handler != null)
            //{
            //    BindingContext = null;
            //    BindingContext = this.Handler?.MauiContext.Services.GetService<MainViewModel>();
            //    //((MainViewModel)BindingContext).ResetCanScrape();
            //}
            //BindingContext = ;
        }

        
    }

}
