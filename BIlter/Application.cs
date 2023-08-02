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

            panelFamily.CreatePushButton<Commands.MaterialsCommand>((MM) =>
            {
                MM.Text = "材质管理器";
                MM.LargeImage = Properties.Resources.layer_32 .ConvertToBitmapSource();
                MM.Image = Properties.Resources.layer_16.ConvertToBitmapSource();
                MM.ToolTip = "文件内材质的增删查改。";
                MM.ToolTipImage = Properties.Resources.Windows32.ConvertToBitmapSource(); ;
                MM.LongDescription = "";
                });

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            //pass
            return Result.Succeeded;
        }
    }
}