using System;

namespace Career_Hub.My_Exception
{
    public class FileUploadException : Exception
    {
        public FileUploadException()
            : base("An error occurred while uploading the file.")
        {
        }

        public FileUploadException(string message)
            : base(message)
        {
        }
    }

}
