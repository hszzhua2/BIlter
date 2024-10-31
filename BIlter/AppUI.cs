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
            const string _tab = "BIlter防火通规辅助工具";

            _uiProvider.GetUIApplication().CreateRibbonTab(_tab);

            Autodesk.Revit.UI.RibbonPanel panelAArch = _uiProvider.GetUIApplication().CreateRibbonPanel(_tab, "疏散距离审查工具");

           
            #region 2.建筑功能>创建路径网
            var cpw = new PushButtonData("CreatePathWeb", "创建路径网", typeof(App).Assembly.Location, "BIlter.Commands.CreatePathWebNew");
            var cpwPlus = panelAArch.AddItem(cpw) as PushButton;
            cpwPlus.SetLargeImage("/BIlter;component/Resources/Icons/Windows32.png");
            RibbonToolTip cpwtoolTip = new RibbonToolTip()
            {
                Title = "选择防火门（逃生出口）并且创建路径：",
                Content = "选择一定数量防火门（逃生出口），点击完成，创建各个门到防火门的距离" +
                "" +
                "房间内部疏散距离要求单独检查，参照《GB 55037-2022》" +
                "解释权归有关单位所有",
            };
            SetRibbonItemToolTip(cpwPlus, cpwtoolTip);
            #endregion PathCommand

            #region 2.建筑功能>创建路径网
            var cpew = new PushButtonData("CreatePathWebe", "路径检查器", typeof(App).Assembly.Location, "BIlter.Commands.PathCommand");
            var cpewPlus = panelAArch.AddItem(cpew) as PushButton;
            cpewPlus.SetLargeImage("/BIlter;component/Resources/Icons/Windows32.png");
            RibbonToolTip cpewtoolTip = new RibbonToolTip()
            {
                Title = "路径检查器：",
                Content = "根据已经生成的路径网，挑选出对于每个防火门（或逃生出口）的最长和最短通道" +
                "本插件仅提供可选及删除功能" +
                "房间内部疏散距离要求单独检查，参照《GB 55037-2022》" +
                "解释权归有关单位所有",
            };
            SetRibbonItemToolTip(cpewPlus, cpewtoolTip);
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
