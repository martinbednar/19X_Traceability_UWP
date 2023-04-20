using System;
using System.IO;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using _19X_Traceability_UWP.BL;

namespace _19X_Traceability_UWP
{
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer _driveConnectedTimer;
        private string _connectedDriveName = string.Empty;

        public MainPage()
        {
            this.InitializeComponent();
            From.Date = DateTime.Now;
            To.Date = DateTime.Now;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Watch example: https://learn.microsoft.com/en-us/uwp/api/windows.ui.xaml.dispatchertimer
            DriveConnectedTimerSetup();
        }
        
        private void DriveConnectedTimerSetup()
        {
            _driveConnectedTimer = new DispatcherTimer();
            _driveConnectedTimer.Tick += DriveConnectedTimerTick;
            _driveConnectedTimer.Interval = new TimeSpan(0, 0, 2);
            _driveConnectedTimer.Start();
        }

        void DriveConnectedTimerTick(object sender, object e)
        {
            bool driveConnected = false;
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.Name != "C:\\")
                {
                    driveConnected = true;
                    _driveConnectedHandler(drive.Name);
                }
            }

            if (!driveConnected)
            {
                _driveDisconnectedHandler();
            }
        }

        private void _driveConnectedHandler(string driveName)
        {
            _connectedDriveName = driveName;
            TextDriveConnected.Text = "PŘIPOJEN";
            ImgDriveConnected.Visibility = Visibility.Visible;
            ImgDriveDisconnected.Visibility = Visibility.Collapsed;
            EnableButtons();
        }

        private void _driveDisconnectedHandler()
        {
            _connectedDriveName = String.Empty;
            TextDriveConnected.Text = "NEPŘIPOJEN";
            ImgDriveConnected.Visibility = Visibility.Collapsed;
            ImgDriveDisconnected.Visibility = Visibility.Visible;
            DisableButtons();
        }

        private void EnableButtons()
        {
            BtnExportLast.IsEnabled = true;
            BtnExportDate.IsEnabled = true;
            BtnExportAll.IsEnabled = true;
        }

        private void DisableButtons()
        {
            BtnExportLast.IsEnabled = false;
            BtnExportDate.IsEnabled = false;
            BtnExportAll.IsEnabled = false;
        }

        private void ExportStartHandler()
        {
            ExportProgressBar.Visibility = Visibility.Visible;
            DisableButtons();
        }

        private void ExportFinishHandler()
        {
            EnableButtons();
            ExportProgressBar.Visibility = Visibility.Collapsed;
        }

        private async void BtnExportLast_Click(object sender, RoutedEventArgs e)
        {
            ExportStartHandler();
            ExportService exportService = new ExportService();
            await exportService.ExportLastKeys(_connectedDriveName);
            ExportFinishHandler();
        }

        private async void BtnExportDate_Click(object sender, RoutedEventArgs e)
        {
            ExportStartHandler();
            ExportService exportService = new ExportService();
            await exportService.ExportDateKeys(From.Date.Date, To.Date.Date.AddDays(1), _connectedDriveName);
            ExportFinishHandler();
        }

        private async void BtnExportAll_Click(object sender, RoutedEventArgs e)
        {
            ExportStartHandler();
            ExportService exportService = new ExportService();
            await exportService.ExportAllKeys(_connectedDriveName);
            ExportFinishHandler();
        }
    }
}
