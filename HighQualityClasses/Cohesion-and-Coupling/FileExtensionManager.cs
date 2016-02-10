namespace CohesionAndCoupling
{
    using System;

    public class FileExtensionManager
    {
        private string fileName;

        public FileExtensionManager()
        {
            
        }

        public FileExtensionManager(string fileName)
        {
            this.FileName = fileName;
        }

        public string FileName
        {
            get { return this.fileName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("File name cannot be emty", nameof(value));
                }

                this.fileName = value;
            }
        }

        public string GetFileExtension()
        {
            int indexOfLastDot = this.fileName.LastIndexOf(".", StringComparison.Ordinal);
            if (indexOfLastDot == -1)
            {
                return "N/A";
            }

            string extension = this.fileName.Substring(indexOfLastDot + 1);

            return extension;
        }

        public string GetFileNameWithoutExtension()
        {
            int indexOfLastDot = this.fileName.LastIndexOf(".", StringComparison.Ordinal);
            if (indexOfLastDot == -1)
            {
                return this.fileName;
            }

            string extension = this.fileName.Substring(0, indexOfLastDot);

            return extension;
        }
    }
}
