using BIlter.IServices;
using BIlter.Services;
using BIlter.Toolkit.Mvvm;
using BIlter.Toolkit.Mvvm.Interfaces;
using BIlter.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BIlter
{
    public class App : ApplicationBase
    {
        public override void RegisterTypes(SimpleIoc container2)
        {
            //注册UI事件
            container2.Register<IApplicationUI, AppUI>();
            container2.Register<IEventManager, AppEvent>();

            //注册Service
            container2.Register<IMaterialService, MaterialService>();

            container2.Register<IPathService, PathService>();
            container2.Register<IProgressBarService, ProgressBarService>();
            //注册ViewModel
            container2.Register<MaterialsViewModel>();

            container2.Register<PathViewModel>();
            container2.Register<ProgressBarDialogViewModel>();
            //注册View
            container2.Register<Views.Materials>();

            container2.Register<Views.PathWindow>();
            container2.Register<Views.ProgressBarDialog>();
        }
    }
}
