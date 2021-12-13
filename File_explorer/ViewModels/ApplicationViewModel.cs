using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace File_explorer.ViewModels
{
    partial class ApplicationViewModel : BaseViewModel
    {
        #region Private Properties 

        private string filePath;
        private readonly IDirectoryHistory _history;
        private bool OnCanMoveForward(object obj) => _history.CanMoveForward;
        private bool OnCanMoveBack(object obj) => _history.CanMoveBack;

        #endregion

        #region Properties

        public string FilePath
        {
            get { return filePath; }
            set
            {
                filePath = value;
                OnPropertyChanged("FilePath");
            }
        }

        public FileEnttyViewModel SelectedEntity { get; set; }

        public ICommand OpenCommand { get; }
        public DelegateCommand MoveBackCommand { get; }
        public DelegateCommand MoveForwardCommand { get; }
        public DelegateCommand RefreshCommand { get; }

        #endregion

        #region Collections

        public ObservableCollection<FileEnttyViewModel> DirectoriesAndFiles { get; set; } = new ObservableCollection<FileEnttyViewModel>();

        #endregion

        #region Constructor

        public ApplicationViewModel()
        {
            _history = new DirectoryHistory("Logical Drives");

            OpenCommand = new DelegateCommand(Open);
            MoveBackCommand = new DelegateCommand(OnMoveBack, OnCanMoveBack);
            MoveForwardCommand = new DelegateCommand(OnMoveForward, OnCanMoveForward);
            RefreshCommand = new DelegateCommand(RefreshDiretory);
            foreach (var logicalDrive in Directory.GetLogicalDrives())
            {
                string writeTime = Directory.GetLastWriteTime(logicalDrive).ToString();

                DirectoriesAndFiles.Add(new DirectoryViewModel(logicalDrive, writeTime, "Folder"));

                FilePath = _history.Current.DirectoryPath;
            }

            _history.HistoryChanged += History_Changed;
        }

        #endregion

        #region Private Metods
       
        private void History_Changed(object sender, EventArgs e)
        {
            MoveBackCommand?.RaiseCanExecuteChanged();
            MoveForwardCommand?.RaiseCanExecuteChanged();
        }

        private void OnMoveForward(object obj)
        {
            _history.MoveForward();

            var curent = _history.Current;

            FilePath = curent.DirectoryPath;

            OpenDiretory();
        }

        private void OnMoveBack(object obj)
        {
            _history.MoveBack();

            var curent = _history.Current;

            FilePath = curent.DirectoryPath;

            OpenDiretory();
        }

        private void Open(object parametr)
        {
            parametr = SelectedEntity;
            try
            {
                if (parametr is DirectoryViewModel directoryViewModel)
                {
                    FilePath = directoryViewModel.FullName;

                    _history.Add(FilePath);


                    OpenDiretory();
                }
                if (parametr is FileViewModel fileViewModel)
                {
                    Process.Start(fileViewModel.FullName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Opening failed: " + ex.Message, "Error");
            }

        }

        private void OpenDiretory()
        {
            DirectoriesAndFiles.Clear();

            if (FilePath.Contains("Logical Drives"))
            {
                foreach (var logicalDrive in Directory.GetLogicalDrives())
                {
                    string writeTime = Directory.GetLastWriteTime(logicalDrive).ToString();

                    DirectoriesAndFiles.Add(new DirectoryViewModel(logicalDrive, writeTime, "Folder"));

                    FilePath = _history.Current.DirectoryPath;
                }
                return;
            }

            var directoryInfo = new DirectoryInfo(FilePath);

            foreach (var directory in directoryInfo.GetDirectories())
            {
                string writeTime = Directory.GetLastWriteTime(directory.FullName).ToString();

                DirectoriesAndFiles.Add(new DirectoryViewModel(directory, writeTime, "Folder"));
            }
            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                string writeTime = Directory.GetLastWriteTime(fileInfo.FullName).ToString();
                string size = ((fileInfo.Length) / 1024).ToString();
                DirectoriesAndFiles.Add(new FileViewModel(fileInfo, writeTime, size));
            }
        }

        private void RefreshDiretory(object obj)
        {
            try
            {
                OpenDiretory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Refreshing failed: " + ex.Message, "Error");
            }
        }

        #endregion
    }
}
