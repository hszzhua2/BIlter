﻿using Autodesk.Revit.UI;
using BIlter.Extension.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Extension.Extensions
{
    /// <summary>
    /// Revit ribbon ui extensions
    /// </summary>
    public static class UIExtension
    {
        /// <summary>
        /// Create ribbon push button
        /// </summary>
        /// <typeparam name="T">IExternalCommand</typeparam>
        /// <param name="panel"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static RibbonPanel CreatePushButton<T>(this RibbonPanel panel, Action<PushButtonData> action) where T : class, IExternalCommand, new()
        {
            if (panel == null)
            {
                throw new ArgumentNullException(nameof(panel), "panel can not be null");
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "action can not be null");
            }

            Type commandType = typeof(T);
            string name = commandType.Name;
            PushButtonData pushButtonData = new PushButtonData($"btn_{name}", name, commandType.Assembly.Location, commandType.FullName);
            action.Invoke(pushButtonData);
            panel.AddItem(pushButtonData);
            return panel;
        }

        /// <summary>
        /// Create a revit ribbon push button
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <returns></returns>
        private static PushButtonData CreatePushButton<TCommand>() where TCommand : class, IExternalCommand, IRibbonButton, new()
        {
            IRibbonButton button = Activator.CreateInstance<TCommand>();
            Type commandType = typeof(TCommand);
            PushButtonData pushButtonData = new PushButtonData($"btn_{commandType.Name}", button.Text, commandType.Assembly.Location, commandType.FullName)
            {
                Image = button.Image.ConvertToBitmapSource(),
                LargeImage = button.LargeImage.ConvertToBitmapSource(),
                ToolTipImage = button.ToolTipImage.ConvertToBitmapSource(),
                ToolTip = button.ToolTip,
                LongDescription = button.LongDescription,
                };
            pushButtonData.SetContextualHelp(button.ContextualHelp);
            pushButtonData.SetAvailability(commandType);
            return pushButtonData;
        }

        /// <summary>
        /// Create a revit ribbon push button
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="panel"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static PushButton CreatePushButton<TCommand>(this RibbonPanel panel) where TCommand : class, IExternalCommand, IRibbonButton, new()
        {
            if (panel == null)
            {
                throw new ArgumentNullException(nameof(panel), "panel can not be null");
            }
            return panel.AddItem(CreatePushButton<TCommand>()) as PushButton;
        }

        /// <summary>
        /// Create a revit ribbon push button
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="pulldownButton"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static PushButton CreatePushButton<TCommand>(this PulldownButton pulldownButton) where TCommand : class, IExternalCommand, IRibbonButton, new()
        {
            if (pulldownButton == null)
            {
                throw new ArgumentNullException(nameof(pulldownButton), "pull down button can not be null");
            }
            return pulldownButton.AddPushButton(CreatePushButton<TCommand>());
        }

        /// <summary>
        /// Create a revit ribbon push button
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="splitButton"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static PushButton CreatePushButton<TCommand>(this SplitButton splitButton) where TCommand : class, IExternalCommand, IExternalCommandAvailability, IRibbonButton, new()
        {
            if (splitButton == null)
            {
                throw new ArgumentNullException(nameof(splitButton), "split button can not be null");
            }
            return splitButton.AddPushButton(CreatePushButton<TCommand>());
        }
    }
}
