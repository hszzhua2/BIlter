using Autodesk.Revit.DB;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BIlter.Toolkit.Mvvm.Extensions
{
    public static class SimpleIocExtension
    {
        public static TView Resolve<TView, TViewModel>(this SimpleIoc container, bool modeless = false) where TView : Window where TViewModel : class
        {
            var view = modeless ? container.GetInstance<TView>() : container.GetInstanceWithoutCaching<TView>();
            view.DataContext = container.GetInstanceWithoutCaching<TViewModel>();
            return view;
        }
    }
}
