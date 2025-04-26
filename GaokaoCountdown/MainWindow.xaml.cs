using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GaokaoCountdown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DispatcherTimer dispatcherTimer = null;

        public MainWindow()
        {
            Width= Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;
            InitializeComponent();
            DispatcherTimer_Tick(null, null);

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            int year = getYear();
            TimeSpan timeSpan = new DateTime(year, 6, 7, 9, 0, 0) - DateTime.Now;
            if (isJFMode) timeSpan = new DateTime(year, 6, 8, 17, 0, 0) - DateTime.Now;

            Days.Text = timeSpan.Days.ToString();
            if (timeSpan.Days >= 200) Days.Foreground = new SolidColorBrush(Color.FromRgb(54,177,97));
            if (timeSpan.Days < 200 && timeSpan.Days >= 100) Days.Foreground = new SolidColorBrush(Color.FromRgb(255,182,85));
            if (timeSpan.Days < 100 && timeSpan.Days >= 30) Days.Foreground = new SolidColorBrush(Color.FromRgb(224,160,0));
            if (timeSpan.Days < 30) Days.Foreground = Brushes.Red;
            ProgbarYear.Foreground = Days.Foreground;

            Hours.Text = (timeSpan.Hours).ToString().PadLeft(2, '0');
            Mins.Text = (timeSpan.Minutes).ToString().PadLeft(2, '0');
            Secs.Text = (timeSpan.Seconds).ToString().PadLeft(2, '0');
            Millisecs.Text = (timeSpan.Milliseconds).ToString().PadLeft(3, '0');

            if (DateTime.IsLeapYear(year)) ProgbarYear.Maximum = 366; else ProgbarYear.Maximum = 365;
            ProgbarYear.Value = ProgbarYear.Maximum - (timeSpan.Days + timeSpan.Hours / 24.0);
            ProgbarDay.Value = ProgbarDay.Maximum - (timeSpan.Hours + timeSpan.Minutes / 60.0);
            ProgbarHour.Value = ProgbarHour.Maximum - (timeSpan.Minutes + timeSpan.Seconds / 60.0);
            ProgbarMin.Value = ProgbarMin.Maximum - (timeSpan.Seconds + timeSpan.Milliseconds / 1000.0);
            ProgbarSec.Value = ProgbarSec.Maximum - timeSpan.Milliseconds;
        }

        private int getYear()
        {
            TimeSpan timeSpan = new DateTime(DateTime.Now.Year, 6, 7, 9, 0, 0) - DateTime.Now;
            if (timeSpan.TotalSeconds < 0) return DateTime.Now.Year + 1;
            else return DateTime.Now.Year;
        }

        private void Grid_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Top = 20;
            Left = 0;
            Width = SystemParameters.WorkArea.Width;
            if(App.StartArgs != null)
            {
                if (App.StartArgs.Contains("-t"))
                {
                    ShowInTaskbar = true;
                }

                if (App.StartArgs.Contains("-jf"))
                {
                    Title.Text = "距离解放还有";
                    TitleEN.Text = "Time to Final Liberation";
                    isJFMode = true;
                }
            }
        }

        bool isJFMode = false;

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
            }
        }
    }
}