using Autodesk.Revit.UI;
using Autodesk.Windows;
using BIlter.Extension.Extensions;
using BIlter.Toolkit.Mvvm.Interfaces;
using System.Reflection;
using System.Windows.Controls;

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
            Autodesk.Revit.UI.RibbonPanel panelAArch = _uiProvider.GetUIApplication().CreateRibbonPanel(_tab, "建筑功能");
            Autodesk.Revit.UI.RibbonPanel panelData = _uiProvider.GetUIApplication().CreateRibbonPanel(_tab, "数据交互");
            Autodesk.Revit.UI.RibbonPanel panelMEP = _uiProvider.GetUIApplication().CreateRibbonPanel(_tab, "机电功能");

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

            };
            SetRibbonItemToolTip(bimobPlus, bimobtoolTip);
            #endregion

            #region 2.建筑功能>判断点所在房间
            var roombypoint = new PushButtonData("roombypoint", "判断点所在房间", typeof(App).Assembly.Location, "BIlter.Commands.FirePathCommand");
            var roombypointPlus = panelAArch.AddItem(roombypoint) as PushButton;
            roombypointPlus.SetImage("/BIlter;component/Resources/Icons/RibbonIcon16.png");
            roombypointPlus.SetLargeImage("/BIlter;component/Resources/Icons/RibbonIcon32.png");
            RibbonToolTip roombypointbimobtoolTip = new RibbonToolTip()
            {
                Title = "GetRoomByPoint",
                Content = "根据点获取房间",
            };
            SetRibbonItemToolTip(roombypointPlus, roombypointbimobtoolTip);
            #endregion

            #region 2.建筑功能>创建路径网
            var cpw = new PushButtonData("CreatePathWeb", "创建路径网", typeof(App).Assembly.Location, "BIlter.Commands.CreatePathWeb");
            var cpwPlus = panelAArch.AddItem(cpw) as PushButton;
            cpwPlus.SetImage("/BIlter;component/Resources/Icons/RibbonIcon16.png");
            cpwPlus.SetLargeImage("/BIlter;component/Resources/Icons/RibbonIcon32.png");
            RibbonToolTip cpwtoolTip = new RibbonToolTip()
            {
                Title = "CreatePathWeb",
                Content = "选择一定数量防火门，创建模拟逃生路线。",
            };
            SetRibbonItemToolTip(cpwPlus, cpwtoolTip);
            #endregion

            #region 2.建筑功能>清理路网
            var cpww = new PushButtonData("CleanPathWeb", "清理路网", typeof(App).Assembly.Location, "BIlter.Commands.CleanPathWeb");
            var cpwwPlus = panelAArch.AddItem(cpww) as PushButton;
            cpwwPlus.SetImage("/BIlter;component/Resources/Icons/RibbonIcon16.png");
            cpwwPlus.SetLargeImage("/BIlter;component/Resources/Icons/RibbonIcon32.png");
            RibbonToolTip cpwwtoolTip = new RibbonToolTip()
            {
                Title = "CleanPathWeb",
                Content = "基于模拟逃生路网，清除多余路径，显示防火分区。",
            };
            SetRibbonItemToolTip(cpwwPlus, cpwwtoolTip);
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
