using Autodesk.Revit.DB;
using BIlter.Entity;
using BIlter.Extension.Extensions;
using BIlter.Interfaces;
using BIlter.IServices;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIlter.Toolkit.Mvvm.Interfaces;
using IDataContext = BIlter.Toolkit.Mvvm.Interfaces.IDataContext;

namespace BIlter.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IDataContext _dataContext;

        public MaterialService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public BOX_Material CreateELement(string name)
        {
            Element element = null;
            _dataContext.GetDocument().NewTransaction("创建材质", () =>
            {
                ElementId id = Material.Create(_dataContext.GetDocument(), name);
                element = _dataContext.GetDocument().GetElement(id);
            });
            return new BOX_Material(element as Material);
        }

        public void DeleteElement(BOX_Material element)
        {
            if (element == null)
                return;

            _dataContext.GetDocument().NewTransaction("删除材质", () =>
            {
                _dataContext.GetDocument().Delete(element.Id);
            });
        }

        public void DeleteElements(IEnumerable<BOX_Material> elements)
        {

            _dataContext.GetDocument().NewTransaction("删除材质", () =>
            {
                foreach (var material in elements)
                {
                    Messenger.Default.Send<string>(material.Name, Contacts.Tokens.ProgressBarTitle);
                    _dataContext.GetDocument().Delete(material.Id);
                    _dataContext.GetDocument().Regenerate();
                }
            });
        }


        public IEnumerable<BOX_Material> GetElements(Func<BOX_Material, bool> predicate = null)
        {
            FilteredElementCollector elements = new FilteredElementCollector(_dataContext.GetDocument()).OfClass(typeof(Material));
            IEnumerable<BOX_Material> materials = elements.ToList()
                .ConvertAll(x => new BOX_Material(x as Material));

            if (predicate != null)
            {
                materials = materials.Where(predicate);
            }
            return materials;
        }

        public void Export(IEnumerable<BOX_Material> elements)
        {
            throw new NotImplementedException();
        }

        public IExcelTransfer<BOX_Material> Import()
        {
            throw new NotImplementedException();
        }

        void IDataService<BOX_Material>.ZoomToElement(BOX_Material elements)
        {
            throw new NotImplementedException();
        }

        BOX_Material IDataService<BOX_Material>.CreateLevel(double elevation)
        {
            throw new NotImplementedException();
        }
    }
}
