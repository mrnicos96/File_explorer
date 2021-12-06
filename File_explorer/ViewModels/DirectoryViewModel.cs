using System.IO;
namespace File_explorer.ViewModels
{
    public sealed class DirectoryViewModel : FileEnttyViewModel
    {
        
        public DirectoryViewModel( string directoryName, string writeTime, string size) : base( directoryName, writeTime, size)
        {
            Name = directoryName;
            FullName = directoryName;
            WriteTime = writeTime;
            Size = size;
        }
        public DirectoryViewModel(DirectoryInfo directoryName, string writeTime, string size) 
            : base(directoryName.Name, writeTime, size)
        {
            Name = directoryName.Name;
            FullName = directoryName.FullName;
            WriteTime = writeTime;
            Size = size;
        }
    }
}
