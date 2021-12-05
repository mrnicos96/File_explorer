using System.IO;

namespace File_explorer.ViewModels
{
    public sealed class FileViewModel : FileEnttyViewModel
    {
        public FileViewModel(string name, string writeTime, string size) : base(name, writeTime, size)
        {
            WriteTime = writeTime;
            Size = size;
        }

        public FileViewModel(FileInfo fileInfo, string writeTime, string size) : base(fileInfo.Name, writeTime, size)
        {
            FullName = fileInfo.FullName;
            WriteTime = writeTime;
            Size = size;
        }
    }
}
