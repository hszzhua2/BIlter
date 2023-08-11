using Autodesk.Revit.UI;
using Autodesk.Windows;
using BIlter.Extension.Extensions;
using BIlter.Toolkit.Mvvm.Interfaces;
using System.Reflection;

namespace BIlter
{
    public class AppUI : IApplicationUI
    {
        private readonly IUIProvider _uiProvider;

        public AppUI(IUIProvider uIProvider)
        {
            this._uiProvider = uIProvider;
        }
        public Result Initial()
        {
            const string _tab = "BIlter";

            _uiProvider.GetUIApplication().CreateRibbonTab(_tab);

            Autodesk.Revit.UI.RibbonPanel panelFamily = _uiProvider.GetUIApplication().CreateRibbonPanel(_tab, "项目资源管理");

            #region MM 材质管理
            var buttomMM = new PushButtonData("Material Manager", "材质管理器", typeof(App).Assembly.Location, "BIlter.Commands.MaterialsCommand");

            var buttomMMPlus = panelFamily.AddItem(buttomMM) as PushButton;
            buttomMMPlus.SetLargeImage("/BIlter;component/Resources/Icons/RibbonIcon32.png");
            buttomMMPlus.SetImage("/BIlter;component/Resources/Icons/RibbonIcon16.png");
            RibbonToolTip toolTip = new RibbonToolTip()
            {
                Title = "Material Manager",
                Content = "A manager for materials editing, creating, deleting, importing and exporting.",
                ExpandedContent = "A manager for materials editing, creating, deleting, importing and exporting. The color editor and creator are support and it is only supported to edit appearance color.",
                ExpandedVideo = new Uri("C:\\Program Files\\Autodesk\\Revit 2023\\videos\\tooltip.mp4"),
            };
            SetRibbonItemToolTip(buttomMMPlus, toolTip);

            #endregion

            #region BIMObject
            var bimob = new PushButtonData("BIMObject", "BIMObject", typeof(App).Assembly.Location, "BIlter.Commands.OpenURLCommand");

            var bimobPlus = panelFamily.AddItem(bimob) as PushButton;
            bimobPlus.SetImage("/BIlter;component/Resources/Icons/RibbonIcon16.png");
            bimobPlus.SetLargeImage("/BIlter;component/Resources/Icons/RibbonIcon32.png");
            RibbonToolTip bimobtoolTip = new RibbonToolTip()
            {
                Title = "打开BIMObject网页",
                Content = "公开免费族库网站BIMObject",
                ExpandedContent = "BIMObject.com is a global marketplace for the construction industry. We provide design inspiration and digital product information to the world's architects and engineers while giving building product manufacturers a better way to reach, influence, and understand them.",
                ExpandedVideo = new Uri("C:\\Program Files\\Autodesk\\Revit 2023\\videos\\tooltip.mp4"),
            };
            SetRibbonItemToolTip(bimobPlus, bimobtoolTip);

            #endregion



            return Result.Succeeded;
        }
        public static void SetRibbonItemToolTip(Autodesk.Revit.UI.RibbonItem item, RibbonToolTip toolTip)
        {
            var ribbonItem = GetRibbonItem(item);
            if (ribbonItem == null)
                return;
            ribbonItem.ToolTip = toolTip;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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
