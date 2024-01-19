using Autodesk.Revit.DB.Analysis;
using Autodesk.Revit.DB;
using BIlter.Entity;
using BIlter.Interfaces;
using BIlter.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIlter.Extension.Extensions;
using IDataContext = BIlter.Toolkit.Mvvm.Interfaces.IDataContext;
using GalaSoft.MvvmLight.Messaging;


namespace BIlter.Services
{
    public class PathService : IPathService
    {
        private readonly IDataContext _dataContext;

        public PathService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public BOX_Path CreateELement(string name)
        {
            throw new NotImplementedException();
        }
        public void DeleteElement(BOX_Path element)
        {
            if (element == null)
                return;

            _dataContext.GetDocument().NewTransaction("删除路径", () =>
            {
                _dataContext.GetDocument().Delete(element.Id);
            });
        }

        public void DeleteElements(IEnumerable<BOX_Path> elements)
        {

            _dataContext.GetDocument().NewTransaction("删除材质", () =>
            {
                foreach (var pathoftravel in elements)
                {
                    Messenger.Default.Send<XYZ>(pathoftravel.PathStart, Contacts.Tokens.ProgressBarTitle);
                    _dataContext.GetDocument().Delete(pathoftravel.Id);
                    _dataContext.GetDocument().Regenerate();
                }
            });
        }
        public void ZoomToElement(BOX_Path element)
        {
            //实际执行的任务在这里
            if (element == null)
                return;
            _dataContext.GetDocument().NewTransaction("定位到元素", () =>
            {
                _dataContext.GetUIDocument().ShowElements(element.Id);
            });
        }
        public IEnumerable<BOX_Path> GetElements(Func<BOX_Path, bool> predicate = null)
        {
            FilteredElementCollector pathCollotor = new FilteredElementCollector(_dataContext.GetDocument()).OfClass(typeof(PathOfTravel));
            IEnumerable<BOX_Path> pathOfTravels = pathCollotor.ToList()
                .ConvertAll(x => new BOX_Path(x as PathOfTravel));

            if (predicate != null)
            {
                pathOfTravels = pathOfTravels.Where(predicate);
            }
            return pathOfTravels;
        }
        BOX_Path IDataService<BOX_Path>.CreateLevel(double elevation)
        {
            throw new NotImplementedException();
        }
    }
}

