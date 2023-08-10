using BIlter.IServices;
using BIlter.Services;
using BIlter.Toolkit.Mvvm;
using BIlter.Toolkit.Mvvm.Interfaces;
using BIlter.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter
{
    public class App : ApplicationBase
    {
        public override void RegisterTypes(SimpleIoc container)
        {
            //注册UI事件
            container.Register<IApplicationUI, AppUI>();
            container.Register<IEventManager, AppEvent>();

            //注册Service
            container.Register<IMaterialService, MaterialService>();
            container.Register<IProgressBarService, ProgressBarService>();
            //注册ViewModel
            container.Register<MaterialsViewModel>();
            container.Register<ProgressBarDialogViewModel>();
            //注册View
            container.Register<Views.Materials>();
            container.Register<Views.ProgressBarDialog>();
        }
    }
}
