using Autodesk.Revit.UI;
using Autodesk.Windows;
using Serilog.Events;
using UIFramework;
using BIlter.Extension.Extensions;

namespace BIlter
{
    [UsedImplicitly]
    public class Application : IExternalApplication
    {
        private const string _tab = "BIlter";
        private const string panelName1 = "BIlter";
        /*public override void OnStartup()
        {
            CreateLogger();
            CreateRibbon();
        }

        public override void OnShutdown()
        {
            Log.CloseAndFlush();
        }

        private void CreateRibbon()
        {
            var panel = Application.CreatePanel("Commands", "BIlter");

            var showButton = panel.AddPushButton<Command>("Execute");
            showButton.SetImage("/BIlter;component/Resources/Icons/RibbonIcon16.png");
            showButton.SetLargeImage("/BIlter;component/Resources/Icons/RibbonIcon32.png");


            var winButton = panel.AddPushButton<RelayCommand>("SimpleMVVM");
            winButton.SetImage("/BIlter;component/Resources/Icons/Windows16.png");
            winButton.SetLargeImage("/BIlter;component/Resources/Icons/Windows32.png");
            //ExtensionVideo = new Uri("C:\\Program Files\\Autodesk\\Revit 2023\\videos\\tooltip.mp4")
        }*/

        private static void CreateLogger()
        {
            const string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Debug(LogEventLevel.Debug, outputTemplate)
                .MinimumLevel.Debug()
                .CreateLogger();

            AppDomain.CurrentDomain.UnhandledException += (_, args) =>
            {
                var e = (Exception)args.ExceptionObject;
                Log.Fatal(e, "Domain unhandled exception");
            };
        }

        public Result OnStartup(UIControlledApplication application)
        {
            RibbonControl ribbonControl = RevitRibbonControl.RibbonControl;
            application.CreateRibbonTab(_tab);
            Autodesk.Revit.UI.RibbonPanel panelFamily = application.CreateRibbonPanel(_tab, panelName1);

            panelFamily.CreatePushButton<Commands.Command>((Oo) =>
            {
                Oo.Text = "BIlter";
                Oo.LargeImage = Properties.Resources.Family_32.ConvertToBitmapSource();
                Oo.Image = Properties.Resources.Family_16.ConvertToBitmapSource();
                Oo.ToolTip = "";
                Oo.ToolTipImage = Properties.Resources.Windows32.ConvertToBitmapSource(); ;
                Oo.LongDescription = "BIMObject.com is a global marketplace for the construction industry.We provide design inspiration and digital product information to the world's architects and engineers while giving building product manufacturers a better way to reach, influence, and understand them.";
            });

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}