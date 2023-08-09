using Autodesk.Revit.UI;
using Serilog.Events;
using Autodesk.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UIFramework;
using RibbonItem = Autodesk.Revit.UI.RibbonItem;
using BIlter.Commands;
using System.Windows.Shapes;
using System.Resources.Extensions;
using Nice3point.Revit.Toolkit.External;
using Nice3point.Revit.Extensions;
using BIlter.Extension.Extensions;

namespace BIlter
{
    [UsedImplicitly]
    public class Application : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
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

        private const string _tab = "BIlter";
        private const string panelName1 = "BIlter";

        public Result OnStartup(UIControlledApplication application)
        {
            application.CreateRibbonTab(_tab);
            var panelFamily = application.CreateRibbonPanel(_tab, panelName1);

            #region MM 材质管理
            var buttomMM = new PushButtonData("Material Manager", "Material Manager", typeof(Application).Assembly.Location, "BIlter.Commands.MaterialsCommand");

            var buttomMMPlus = panelFamily.AddItem(buttomMM) as PushButton;
            buttomMMPlus.SetLargeImage("/BIlter;component/Resources/Icons/Windows32.png");
            buttomMMPlus.SetImage("/BIlter;component/Resources/Icons/Windows16.png");
            RibbonToolTip toolTip = new RibbonToolTip()
            {
                Title = "Material Manager",
                Content = "A manager for materials editing, creating, deleting, importing and exporting.",
                ExpandedContent = "A manager for materials editing, creating, deleting, importing and exporting. The color editor and creator are support and it is only supported to edit appearance color.",
                ExpandedVideo = new Uri("C:\\Program Files\\Autodesk\\Revit 2023\\videos\\tooltip.mp4"),
            };
            SetRibbonItemToolTip(buttomMMPlus, toolTip);

            #endregion

            //创建一个空白的Stack
            PulldownButtonData stackData = new PulldownButtonData("Stack", "Stack");
            PulldownButton stackButton = panelFamily.AddItem(stackData) as PulldownButton;
            stackButton.AddPushButton(buttomMM);
            return Result.Succeeded;
        }

        public static void SetRibbonItemToolTip(Autodesk.Revit.UI.RibbonItem item, RibbonToolTip toolTip)
        {
            var ribbonItem = GetRibbonItem(item);
            if (ribbonItem == null)
                return;
            ribbonItem.ToolTip = toolTip;
        }

        private static Autodesk.Windows.RibbonItem? GetRibbonItem(Autodesk.Revit.UI.RibbonItem item)
        {
            Type itemType = item.GetType();
            var mi = itemType.GetMethod("getRibbonItem",
              BindingFlags.NonPublic | BindingFlags.Instance);
            var windowRibbonItem = mi.Invoke(item, null);
            return windowRibbonItem as Autodesk.Windows.RibbonItem;
        }
    }
}