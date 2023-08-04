using Autodesk.Revit.DB;
using BIlter.Entity;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using BIlter.Extension.Extensions;
using BIlter.IServices;
using BIlter.Interfaces;

namespace BIlter.ViewModels
{
    public class MaterialsViewModel : ViewModelBase
    {

        private readonly MaterialsViewModel _service;
        private ObservableCollection<BOX_Material> _materials;

        /*public MaterialsViewModel(IMaterialService service)
        {
            _service = service;
            GetElements();
        }


        #region Methods
        private void GetElements()
        {
            Materials = new ObservableCollection<BOX_Material>(_service.GetElements(equals => string.IsNullOrEmpty(Keyword) || e.Name.Contains(Keyword)));
        }

        #endregion*/




        private Document _document;
        public MaterialsViewModel(Document document)
        {
            this._document = document;
            QueryElements();
            MessengerInstance.Register<BOX_Material>(this, "InsertMaterial", InsertMaterial);
        }

        private void InsertMaterial(BOX_Material obj)
        {
            Materials.Insert(0, obj);
        }

        //定义 事件命令的字段

        //材质列表
        
        public ObservableCollection<BOX_Material> Materials
        {
            get { return _materials; }
            set { Set(ref _materials, value); }
        }


        #region 删除命令/中继命令/字段
        //删除
        private RelayCommand<IList> _deleteElementsCommand;

        //中继命令
        private void DeleteElements(IList selectedElements)
        {
            _document.NewTransaction("删除材质", () =>
            {
                for (int i = selectedElements.Count - 1; i >= 0; i--)
                {
                    BOX_Material? material = selectedElements[i] as BOX_Material;
                    _document.Delete(material.Material.Id);
                    Materials.Remove(material);
                }
            });
        }

        public RelayCommand<IList> DeleteElementsCommand
        {
            get => _deleteElementsCommand ?? new RelayCommand<IList>(DeleteElements);
        }
        #endregion


        #region 搜索功能

        //查询
        private RelayCommand _queryElementsCommand;
        //关键字
        private string _keyword;
        public RelayCommand QueryElementsCommand { get => _queryElementsCommand ??= new RelayCommand(QueryElements); }

        private bool CanQueryElements()
        {
            return string.IsNullOrEmpty(_keyword);
        }
        private void QueryElements()
        {

            FilteredElementCollector elements = new FilteredElementCollector(_document).OfClass(typeof(Material));
            var materials = new ObservableCollection<BOX_Material>(elements.ToList()
                .ConvertAll(x => new BOX_Material(x as Material))
                .Where(e => string.IsNullOrEmpty(Keyword) || e.Name.Contains(Keyword)));

            Materials = new ObservableCollection<BOX_Material>(materials);

        }

        public string Keyword
        {
            get { return _keyword; }
            set { _keyword = value; _queryElementsCommand.RaiseCanExecuteChanged(); }
        }
        #endregion




        #region 创建材质窗口

        //创建
        public RelayCommand CreateElementCommand
        {
            get => new RelayCommand(() =>
            {
                MessengerInstance.Send(new NotificationMessageAction<BOX_Material>(null, _document, "Create", (e) =>
                {
                    if (e != null)
                    {
                        Materials.Insert(0, e);
                    }
                }), Contacts.Tokens.ShowMaterialsDialog);
            });
        }
        #endregion

        #region 编辑窗口

        //编辑
        private RelayCommand<BOX_Material> _editCommand;
        public RelayCommand<BOX_Material> EditMaterialCommand
        {
            get => _editCommand ??= new RelayCommand<BOX_Material>(EditMaterial);
        }
        private void EditMaterial(BOX_Material obj)
        {
            MessengerInstance.Send(new NotificationMessageAction<BOX_Material>(obj, _document, "Edit", (e) =>
            {

            }), Contacts.Tokens.ShowMaterialsDialog);
        }
        #endregion


        #region 提交命令（组）
        //提交命令（组）
        private RelayCommand _sumbitCommand;
        public RelayCommand SubmitCommand { get => _sumbitCommand ??= new RelayCommand(Submit); }

        private void Submit()
        {
            MessengerInstance.Send(true, Contacts.Tokens.MaterialsDialog);
        }
        #endregion




    }
}