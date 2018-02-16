using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR.Models
{
    public static class FolderExtention
    {
        public static string GetFolder(string currentDirectory)
        {
            var dlg = new CommonOpenFileDialog
            {
                Title = "Select Folder",
                IsFolderPicker = true,
                InitialDirectory = currentDirectory,

                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                DefaultDirectory = currentDirectory,
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false,
                ShowPlacesList = true
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var folder = dlg.FileName;
                Console.WriteLine(folder);
                return folder;
            }
            return null;
        }
    }
}
