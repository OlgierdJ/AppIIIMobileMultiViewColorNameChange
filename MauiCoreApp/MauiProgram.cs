using CommunityToolkit.Maui;
using CoreLib.Models;
using CoreLib.WebApi;
using MauiCoreApp.ContentPages;
using MauiCoreApp.ViewModels;
using Microsoft.Extensions.Logging;

namespace MauiCoreApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiCommunityToolkit();
            builder.Services.AddSingleton<IndividualWebApi>();
            builder.Services.AddSingleton<SignalRClientService>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddTransient<AddIndividualPage>();
            //builder.Services.AddTransient<ViewProcessedNodesViewModel>();
            builder.Services.AddTransient<EditIndividualPage>();
            //builder.Services.AddTransient<ViewProcessedNodesViewModel>();

            //builder.Services.AddTransient<EditIndividualPage>();

            //builder.Services.AddTransient<ViewProcessedNodesPage>();
            //builder.Services.AddTransient<ViewProcessedNodesViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
