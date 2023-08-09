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

        private readonly IMaterialService _service;
        private readonly IProgressBarService _progressBarService;
        private ObservableCollection<BOX_Material> _materials;

        public MaterialsViewModel(IMaterialService service, IProgressBarService progressBarService)
        {
            _service = service;
            this._progressBarService = progressBarService;
            GetElements();
        }

        #region Methods
        private void GetElements()
        {
            Materials = new ObservableCollection<BOX_Material>(_service.GetElements(e => string.IsNullOrEmpty(Keyword) || e.Name.Contains(Keyword)));
        }
        #endregion

        public RelayCommand QueryElementsCommand { get => new RelayCommand(GetElements); }

        #region 创建材质窗口

        //创建
        public RelayCommand CreateElementCommand
        {
            get => new RelayCommand(() =>
            {
                MessengerInstance.Send(new NotificationMessageAction<BOX_Material>(null,
                    new MaterialDialogViewModel(this._service),
                    "Create",
                    (e) =>
                    {
                        Materials.Insert(0, e);
                    }), Contacts.Tokens.ShowMaterialDialog);
            });
        }
        #endregion

        #region 编辑

        //编辑
        public RelayCommand<BOX_Material> EditMaterialCommand
        {
            get => new RelayCommand<BOX_Material>((m) =>
            {
                MessengerInstance.Send(new NotificationMessageAction<BOX_Material>(m,
                    new MaterialDialogViewModel(this._service),
                    "Edit",
                    (e) =>
                    {

                    }), Contacts.Tokens.ShowMaterialDialog);

            });
        }
        #endregion

        #region 删除

        public RelayCommand<IList> DeleteElementsCommand
        {
            get => new RelayCommand<IList>((selectedElements) =>
            {
                this._progressBarService.Start(selectedElements.Count);
                _service.DeleteElements(selectedElements.Cast<BOX_Material>());
                GetElements();
                this._progressBarService.Stop();
            });
        }
        #endregion

        public RelayCommand ExportExcelCommand
        {
            get => new RelayCommand(() => _service.Export(Materials));
        }

        public RelayCommand ImportExcelCommand
        {
            get => new RelayCommand(() => _service.Import());
        }

        public RelayCommand SubmitCommand { get => new RelayCommand(() => { MessengerInstance.Send(true, Contacts.Tokens.MaterialsDialog); }); }

        public ObservableCollection<BOX_Material> Materials
        {
            get { return _materials; }
            set { Set(ref _materials, value); }
        }

        public string Keyword { get; set; }
    }
}