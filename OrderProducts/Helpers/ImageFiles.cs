using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OrderProducts.Helpers
{
    public static class ImageFiles
    {
        public static void DeleteFile(string directoryToDelete, string photoName)
        {
            if (!string.IsNullOrEmpty(photoName))
            {
                string fullPath = Path.Combine(directoryToDelete, photoName);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
        }

        public static string AddFile(string directoryToSave, HttpPostedFileBase uploadedFile)
        {
            string fileName = "";
            if (uploadedFile != null)
            {
                string tempFileName = Path.GetFileName(uploadedFile.FileName);
                fileName = tempFileName;
                string pathToSave;
                int i = 1;
                bool isUpdatedFileName;
                do
                {
                    isUpdatedFileName = false;
                    pathToSave = Path.Combine(directoryToSave, fileName);
                    if (System.IO.File.Exists(pathToSave))
                    {
                        int index = tempFileName.IndexOf('.');
                        fileName = tempFileName.Insert(index, $"({i++})");
                        isUpdatedFileName = true;
                    }
                } while (isUpdatedFileName);

                uploadedFile.SaveAs(pathToSave);
            }
            return fileName;
        }
    }
}