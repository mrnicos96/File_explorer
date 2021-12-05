namespace File_explorer.ViewModels
{
    public abstract class FileEnttyViewModel : BaseViewModel
    {
        public string FullName { get; set; }
        public string Name { get; }
        public string WriteTime { get; set; }
        public string Size { get; set; }
        protected FileEnttyViewModel(string name, string writeTime, string size)
        {
            Name = name;
            WriteTime = writeTime;
            Size = size;
        }
    }
}
