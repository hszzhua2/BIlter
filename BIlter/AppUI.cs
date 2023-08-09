using Autodesk.Revit.UI;
using BIlter.Extension.Extensions;
using BIlter.Toolkit.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            RibbonPanel panel = _uiProvider.GetUIApplication().CreateRibbonPanel(_tab, "资源");

            
            panel.CreatePushButton<Commands.MaterialsCommand>((b) =>
            {
                b.Text = "Material Manager";
                b.LargeImage = Properties.Resources.Windows32.ConvertToBitmapSource();
                b.ToolTip = "This is a material manager.";
                b.LongDescription = "A manager for materials editing, creating, deleting, importing and exporting. The color editor and creator are support and it is only supported to edit appearance color.";
                /*var bPlus = panel.AddItem(b) as PushButton;*/
            });

            return Result.Succeeded;
        }
    }
}
