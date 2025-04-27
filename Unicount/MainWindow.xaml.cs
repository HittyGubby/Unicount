using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Windows.Media.Effects;

namespace Unicount
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer dispatcherTimer = null;
        private DispatcherTimer millisecsTimer = null;
        private string _lastDays, _lastHours, _lastMins, _lastSecs;

        public MainWindow()
        {
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;
            InitializeComponent();
            DispatcherTimer_Tick(null, null);

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            millisecsTimer = new System.Windows.Threading.DispatcherTimer();
            millisecsTimer.Tick += MillisecsTimer_Tick;
            millisecsTimer.Interval = new TimeSpan(0, 0, 0, 0, 30);
            millisecsTimer.Start();
        }
        private void MillisecsTimer_Tick(object sender, EventArgs e) { Millisecs.Text = (1000-DateTime.Now.Millisecond).ToString().PadLeft(3, '0').Substring(0,3); }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            int year = getYear();
            TimeSpan timeSpan = new DateTime(year, 6, 7, 9, 0, 0) - DateTime.Now;
            if (isJFMode) timeSpan = new DateTime(year, 6, 9, 17, 45, 0) - DateTime.Now;

            string days = timeSpan.Days.ToString();
            string hours = timeSpan.Hours.ToString().PadLeft(2, '0');
            string mins = timeSpan.Minutes.ToString().PadLeft(2, '0');
            string secs = timeSpan.Seconds.ToString().PadLeft(2, '0');

            if (timeSpan.Days >= 200) Days.Foreground = new SolidColorBrush(Color.FromRgb(54, 177, 97));
            else if (timeSpan.Days >= 100) Days.Foreground = new SolidColorBrush(Color.FromRgb(255, 182, 85));
            else if (timeSpan.Days >= 30) Days.Foreground = new SolidColorBrush(Color.FromRgb(224, 160, 0));
            else Days.Foreground = Brushes.Red;
            ProgbarYear.Foreground = Days.Foreground;

            if (days != _lastDays) AnimateDigit(Days, days);
            if (hours != _lastHours) AnimateDigit(Hours, hours);
            if (mins != _lastMins) AnimateDigit(Mins, mins);
            if (secs != _lastSecs) AnimateDigit(Secs, secs);

            if (DateTime.IsLeapYear(year)) ProgbarYear.Maximum = 366; else ProgbarYear.Maximum = 365;

            double targetYearValue = ProgbarYear.Maximum - (timeSpan.Days + timeSpan.Hours / 24.0);
            double targetDayValue = ProgbarDay.Maximum - (timeSpan.Hours + timeSpan.Minutes / 60.0);
            double targetHourValue = ProgbarHour.Maximum - (timeSpan.Minutes + timeSpan.Seconds / 60.0);
            double targetMinValue = ProgbarMin.Maximum - (timeSpan.Seconds + timeSpan.Milliseconds / 1000.0);

            AnimateProgressBar(ProgbarYear, targetYearValue);
            AnimateProgressBar(ProgbarDay, targetDayValue);
            AnimateProgressBar(ProgbarHour, targetHourValue);
            AnimateProgressBar(ProgbarMin, targetMinValue);

            _lastDays = days;
            _lastHours = hours;
            _lastMins = mins;
            _lastSecs = secs;
        }
        private void AnimateProgressBar(System.Windows.Controls.ProgressBar progressBar, double targetValue)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation animation = new DoubleAnimation
            {
                From = progressBar.Value,
                To = targetValue,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(animation, progressBar);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Value"));
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }

        private void AnimateDigit(TextBlock textBlock, string newValue)
        {
            if (textBlock.Text == newValue) return;

            var parent = textBlock.Parent as System.Windows.Controls.Panel;
            int index = parent.Children.IndexOf(textBlock);
            var oldDigit = new TextBlock
            {
                Text = textBlock.Text,
                FontFamily = textBlock.FontFamily,
                FontSize = textBlock.FontSize,
                Width = textBlock.Width,
                Height = textBlock.Height,
                Foreground = textBlock.Foreground,
                RenderTransform = new TranslateTransform { Y = 0 },
                Effect = new DropShadowEffect()
            };

            var newDigit = new TextBlock
            {
                Text = newValue,
                FontFamily = textBlock.FontFamily,
                FontSize = textBlock.FontSize,
                Width = textBlock.Width,
                Height = textBlock.Height,
                Foreground = textBlock.Foreground,
                RenderTransform = new TranslateTransform { Y = -textBlock.ActualHeight },
                Effect = new DropShadowEffect()
            };

            parent.Children.Insert(index, oldDigit);
            parent.Children.Insert(index, newDigit);

            System.Windows.Controls.Panel.SetZIndex(newDigit, 1);
            System.Windows.Controls.Panel.SetZIndex(oldDigit, 0);

            var oldAnimation = new DoubleAnimation
            {
                From = -textBlock.ActualHeight,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            var newAnimation = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            newAnimation.Completed += (s, e) =>
            {
                parent.Children.Remove(oldDigit);
                parent.Children.Remove(newDigit);
                textBlock.Text = newValue;
            };

            oldDigit.RenderTransform.BeginAnimation(TranslateTransform.YProperty, oldAnimation);
            newDigit.RenderTransform.BeginAnimation(TranslateTransform.YProperty, newAnimation);
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
            if (App.StartArgs != null)
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