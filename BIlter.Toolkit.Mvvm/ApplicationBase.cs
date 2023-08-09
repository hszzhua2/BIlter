using Autodesk.Revit.UI;
using BIlter.Toolkit.Mvvm.Interfaces;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Toolkit.Mvvm
{
    public abstract class ApplicationBase : IExternalApplication
    {
        public abstract void RegisterTypes(SimpleIoc container);
        public Result OnShutdown(UIControlledApplication application)
        {
            var events = ServiceLocator.Current.GetInstance<IEventManager>();
            if (events != null)
            {
                events.Unsubscribe();
            }
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            if (!ServiceLocator.IsLocationProviderSet)
            {
                ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            }
             
            SimpleIoc.Default.Register<UIControlledApplication>(() => application);
            SimpleIoc.Default.Register<IUIProvider,UIProvider>();
            SimpleIoc.Default.Register<IDataContext,DataContext>();

            RegisterTypes(SimpleIoc.Default);

            //订阅事件
            var events = ServiceLocator.Current.GetInstance<IEventManager>();
            if(events != null)
            {
                events.Subscribe();
            }

            //创建Ribbon UI
            var appUI = ServiceLocator.Current.GetInstance<IApplicationUI>();
            return appUI == null ? Result.Cancelled : appUI.Initial();
        }
    }
}
