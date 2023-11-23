using MauiCoreApp.ContentPages;

namespace MauiCoreApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddIndividualPage), typeof(AddIndividualPage));
            Routing.RegisterRoute(nameof(EditIndividualPage), typeof(EditIndividualPage));
        }
    }
}
