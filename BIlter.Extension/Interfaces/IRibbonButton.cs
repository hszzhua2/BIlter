using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Extension.Interfaces
{   /// <summary>
    /// Revit ribbon ui push button information
    /// </summary>
    public interface IRibbonButton
    {
        /// <summary>
        /// Ribbon button text
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Ribbon button long description
        /// </summary>
        string LongDescription { get; }

        /// <summary>
        /// Ribbon button tool tip
        /// </summary>
        string ToolTip { get; }

        /// <summary>
        /// Ribbon button small image what size is 16px * 16px
        /// </summary>
        Bitmap Image { get; }

        /// <summary>
        /// Ribbon button large image what size is 32px * 32px
        /// </summary>
        Bitmap LargeImage { get; }

        /// <summary>
        /// Ribbon button tool tip image
        /// </summary>
        Bitmap ToolTipImage { get; }

        /// <summary>
        /// Ribbon button contextual help
        /// </summary>
        ContextualHelp ContextualHelp { get; }

        /// <summary>
        /// Ribbon button ExpandedVideo
        /// </summary>
        Uri ToolTipVideo { get; }

        /// <summary>
        /// Ribbon button ExpendContent
        /// </summary>
        Object ExpendContent { get; }
    }
}
