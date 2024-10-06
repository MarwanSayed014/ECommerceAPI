namespace ECommerceAPI.Helpers
{
    public static class ServerFile
    {
        public static string GetExtension(string path)
        {
            return System.IO.Path.GetExtension(path);
        }

        public static bool Upload(IFormFile file, string serverFullPath)
        {
            if (file != null)
            {
                using (var localFile = System.IO.File.OpenWrite(serverFullPath))
                using (var uploadedFile = file.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                }
                return true;
            }
            return false;

        }

        public static void Delete(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public static bool CheckFileExtension(IFormFile file, string[] validTypes)
        {
            string FileExtension = ServerFile.GetExtension(file.FileName);
            if (validTypes.Contains(FileExtension))
            {
                return true;
            }
            return false;
        }
    }
}
