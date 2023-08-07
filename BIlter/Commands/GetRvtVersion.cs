using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Commands
{
    [Transaction(TransactionMode.ReadOnly)]
    [Regeneration(RegenerationOption.Manual)]
    public class GetRvtVersion : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                // Open a file dialog to select a Revit file
                string filePath = ShowFileDialog();

                if (string.IsNullOrEmpty(filePath))
                {
                    TaskDialog.Show("File Selection", "No file selected.");
                    return Result.Cancelled;
                }

                // Get the Revit file version without opening the document
                string version = GetRevitFileVersion(filePath);

                TaskDialog.Show("Revit File Version", $"The selected Revit file version is: {version}");
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }

        private string ShowFileDialog()
        {
            var openFileDialog = new System.Windows.Forms.OpenFileDialog
            {
                Filter = "Revit Files (*.rvt)|*.rvt",
                Title = "Select a Revit file",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return openFileDialog.FileName;
            }

            return null;
        }

        private string GetRevitFileVersion(string filePath)
        {
            try
            {
                // Get the file version without actually opening the document
                var fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {
                    FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(filePath);
                    string version = versionInfo.ProductVersion;
                    return version;
                }
                else
                {
                    return "File not found";
                }
            }
            catch
            {
                return "Error reading file version";
            }
        }
    }
}
