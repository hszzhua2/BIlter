using BIlter.Entity;
using BIlter.Interfaces;
using BIlter.IServices;
using GalaSoft.MvvmLight;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel; // for .xls format (Excel 2003)
using NPOI.SS.UserModel;
using GalaSoft.MvvmLight.Command;

namespace BIlter.ViewModels
{
    public class PathViewModel : ViewModelBase
    {
        private readonly IPathService _service;
        private readonly IProgressBarService _progressBarService;
        private ObservableCollection<BOX_Path> _pathOfTravels;
        private string _propertyNameForGrouping;
        private string _propertyNameForSorting;

        public PathViewModel(IPathService service, IProgressBarService progressBarService)
        {
            _service = service;
            this._progressBarService = progressBarService;
            GetElements();
        }

        private void GetElements()
        {
            PathofTravels = new ObservableCollection<BOX_Path>(_service.GetElements(e => string.IsNullOrEmpty(Keyword) || e.PathName.ToString().Contains(Keyword)));
        }

        public RelayCommand QueryElementsCommand { get => new RelayCommand(GetElements); }

        #region 删除
        public RelayCommand<IList> DeleteElementsCommand
        {
            get => new RelayCommand<IList>((selectedElements) =>
            {
                this._progressBarService.Start(selectedElements.Count);
                _service.DeleteElements(selectedElements.Cast<BOX_Path>());
                GetElements();
                this._progressBarService.Stop();
            });
        }
        #endregion

        /*#region 添加路网
        public RelayCommand<IList> AddPathWebCommand
        {
            get => 
        }

        #endregion*/

        #region ZoomToElementCommand
        public RelayCommand<IList> ZoomToElementCommand
        {
            get => new RelayCommand<IList>((selectedElements) =>
            {
                foreach (var element in selectedElements.Cast<BOX_Path>())
                {
                    _service.ZoomToElement(element);
                }
                GetElements();
            });
        }

        #endregion

        public RelayCommand SubmitCommand { get => new RelayCommand(() => { MessengerInstance.Send(true, Contacts.Tokens.PathsDialog); }); }

        public ObservableCollection<BOX_Path> PathofTravels
        {
            get { return _pathOfTravels; }
            set { Set(ref _pathOfTravels, value); }
        }

        public RelayCommand ExportCommand
        {
            get => new RelayCommand(() =>
            {
                var dialog = new Microsoft.Win32.SaveFileDialog
                {
                    FileName = "PathData", // Default file name
                    DefaultExt = ".xls", // Default file extension
                    Filter = "Excel Files|*.xls" // Filter files by extension
                };

                if (dialog.ShowDialog() == true)
                {
                    ExportToExcel(dialog.FileName);
                }
            });
        }
        private void ExportToExcel(string filePath)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("PathData");

            // Create header row
            IRow headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("Path Name");
            // Add more header cells for other properties

            int rowIndex = 1;
            foreach (var path in PathofTravels)
            {
                IRow row = sheet.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(path.PathName);
                // Set cell values for other properties

                rowIndex++;
            }
            // Write workbook to the provided file path
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fileStream);
            }

        }


        public string Keyword { get; set; }

        public string PropertyNameForGrouping
        {
            get { return _propertyNameForGrouping; }
            set { Set(ref _propertyNameForGrouping, value); }
        }

        public string PropertyNameForSorting
        {
            get { return _propertyNameForSorting; }
            set { Set(ref _propertyNameForSorting, value); }
        }
    }
}
