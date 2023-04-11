using System;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _19X_Traceability_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer _driveConnectedTimer;
        private string _connectedDriveName = string.Empty;

        public MainPage()
        {
            this.InitializeComponent();
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
                if (drive.Name == "I:\\")
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
            DriveConnected.Visibility = Visibility.Visible;
            BtnExport.IsEnabled = true;
        }

        private void _driveDisconnectedHandler()
        {
            _connectedDriveName = String.Empty;
            DriveConnected.Visibility = Visibility.Collapsed;
            BtnExport.IsEnabled = false;
        }
    }
}
