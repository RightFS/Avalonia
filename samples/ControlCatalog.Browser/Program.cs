using System;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;
using Avalonia.Logging;
using ControlCatalog;
using ControlCatalog.Browser;

[assembly:SupportedOSPlatform("browser")]

internal partial class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Before 1 {0}", SynchronizationContext.Current?.GetType());
        await Task.Delay(100);
        Console.WriteLine("After 1 {0}", SynchronizationContext.Current?.GetType());
        
        Trace.Listeners.Add(new ConsoleTraceListener());

        await BuildAvaloniaApp()
            .LogToTrace(LogEventLevel.Warning)
            .AfterSetup(async _ =>
            {
                ControlCatalog.Pages.EmbedSample.Implementation = new EmbedSampleWeb();
                
                Console.WriteLine("Before 2 {0}", SynchronizationContext.Current?.GetType());
                await Task.Delay(100);
                Console.WriteLine("After 2 {0}", SynchronizationContext.Current?.GetType());
            })
            .StartBrowserAppAsync("out");
    }

    // Example without a ISingleViewApplicationLifetime
    // private static AvaloniaView _avaloniaView;
    // public static async Task Main(string[] args)
    // {
    //     await BuildAvaloniaApp()
    //         .SetupBrowserApp();
    //
    //     _avaloniaView = new AvaloniaView("out");
    //     _avaloniaView.Content = new TextBlock { Text = "Hello world" };
    // }
    
    public static AppBuilder BuildAvaloniaApp()
           => AppBuilder.Configure<App>();
}
