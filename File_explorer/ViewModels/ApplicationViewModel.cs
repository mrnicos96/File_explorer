using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using System.Diagnostics;

namespace File_explorer.ViewModels
{
    partial class ApplicationViewModel : BaseViewModel
    {
        public string FilePath { get; set; }
        public ObservableCollection<FileEnttyViewModel> DirectoriesAndFiles { get; set; } = new ObservableCollection<FileEnttyViewModel>();
        public FileEnttyViewModel SelectedEntity { get; set; }
        public ICommand OpenCommand { get; }
        public ApplicationViewModel()
        {
            OpenCommand = new DelegateCommand(Open);
            foreach (var logicalDrive in Directory.GetLogicalDrives())
            {
                string writeTime = Directory.GetLastWriteTime(logicalDrive).ToString();
                
                DirectoriesAndFiles.Add(new DirectoryViewModel(logicalDrive, writeTime, "Folder"));
            }
        }
        private void Open (object parametr)
        {
            parametr = SelectedEntity;
            if (parametr is DirectoryViewModel directoryViewModel)
            {
                FilePath = directoryViewModel.FullName;
                DirectoriesAndFiles.Clear();
                var directoryInfo = new DirectoryInfo(FilePath);

                foreach (var directory in directoryInfo.GetDirectories())
                {
                    string writeTime = Directory.GetLastWriteTime(directory.FullName).ToString();

                    DirectoriesAndFiles.Add(new DirectoryViewModel(directory, writeTime, "Folder"));
                }
                foreach (var fileInfo in directoryInfo.GetFiles())
                {
                    string writeTime = Directory.GetLastWriteTime(fileInfo.FullName).ToString();
                    string size = ((fileInfo.Length)/1024).ToString();
                    DirectoriesAndFiles.Add(new FileViewModel (fileInfo, writeTime, size));
                }
            }
            if (parametr is FileViewModel fileViewModel)
            {
                Process.Start(fileViewModel.FullName);                
            }
        }
    }
}
