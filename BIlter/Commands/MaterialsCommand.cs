using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BIlter.Interfaces;
using BIlter.IServices;
using BIlter.Services;
using BIlter.ViewModels;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace BIlter.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    [Regeneration(RegenerationOption.Manual)]
    public class MaterialsCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uIDocument = commandData.Application.ActiveUIDocument;
            Document document = uIDocument.Document;
            ///构建IOC容器
            ///
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            ///注册服务
            ///接口注入 细节注入，实例注入
            ///
            SimpleIoc.Default.Register<Document>(() => document);
            SimpleIoc.Default.Register<IDataContext, DataContext>();
            SimpleIoc.Default.Register<IMaterialService, MaterialService>();
            ///Singleton\ Scope \ Transient
            ///Autofac\Unity\DI
            ///
            SimpleIoc.Default.Register<MaterialsViewModel>();
            SimpleIoc.Default.Register<Views.Materials>();

            ///使用服务
            ///IOC、Provider拿到服务，依赖注入=》构造函数注入
            
            //do something
            Views.Materials materials = new Views.Materials(document);
            materials.DataContext = ServiceLocator.Current.GetInstance<MaterialsViewModel>();
            TransactionStatus status;
            using (TransactionGroup group = new TransactionGroup(document, "资源管理"))
            {
                group.Start();
                if (materials.ShowDialog().Value)
                {
                    status = group.Assimilate();
                }
                else
                {
                    status = group.RollBack();
                }
            }
            SimpleIoc.Default.Unregister<Document>();
                    if (status == TransactionStatus.Committed)
                {
                    return Result.Succeeded;
                }
            return Result.Succeeded;
        }
    }
}
