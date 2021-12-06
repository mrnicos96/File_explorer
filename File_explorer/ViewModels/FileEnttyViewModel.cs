namespace File_explorer.ViewModels
{
    public abstract class FileEnttyViewModel : BaseViewModel
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public string WriteTime { get; set; }
        public string Size { get; set; }
        protected FileEnttyViewModel(string fullName, string writeTime, string size)
        {
            FullName = fullName;
            WriteTime = writeTime;
            Size = size;
        }
    }
}
