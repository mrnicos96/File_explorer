using File_explorer.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using System.ComponentModel;


namespace File_explorer
{
    public partial class MainWindow : Window
    {
        bool forceClose = false;

        public MainWindow()
        {
            InitializeComponent();
            if (IsAdmin())
            {
                RestartPanel.Visibility = System.Windows.Visibility.Collapsed;
            }
            this.DataContext = new ApplicationViewModel();
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!forceClose)
            {
                e.Cancel = true;
                Hide();
            }
        }
        private void MenuItem1_Click(object sender, RoutedEventArgs e)
        {
            Show();
        }

        private void MenuItem2_Click(object sender, RoutedEventArgs e)
        {
            forceClose = true;
            Close();
        }

        public static bool IsAdmin()
        {
            System.Security.Principal.WindowsIdentity id = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal p = new System.Security.Principal.WindowsPrincipal(id);

            return p.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            RestartAsAdmin();
        }

        private void RestartAsAdmin()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            startInfo.Arguments = "restart " + this.Left + " "
                + this.Top + " " + this.Width + " " + this.Height;

            startInfo.Verb = "runas";
            try
            {
                Process p = Process.Start(startInfo);
                forceClose = true;
                this.Close();
            }
            catch (Exception ex)
            {
                // UAC elevation failed
            }
        }
    }
}
