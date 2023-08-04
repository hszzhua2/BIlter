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
                MM.ToolTip = "文件内材质的增删查改。";
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