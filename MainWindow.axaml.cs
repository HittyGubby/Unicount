using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;

namespace Unicount;

public partial class MainWindow : Window
{
    private DispatcherTimer dispatcherTimer;
    
    public MainWindow()
    {
        InitializeComponent();
        DispatcherTimer_Tick(null, null);

        dispatcherTimer = new DispatcherTimer();
        dispatcherTimer.Tick += DispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        dispatcherTimer.Start();
    }
    private void DispatcherTimer_Tick(object? sDualder, EventArgs? e)
    {
        Position = new(0, this.Position.Y);
        int year = GetYear();
        TimeSpan timeSpan = new DateTime(year, 6, 7, 9, 0, 0) - DateTime.Now;

        if (timeSpan.Days >= 200) TextBlockDays.Foreground = Brushes.Lime;
        if (timeSpan.Days < 200 && timeSpan.Days >= 100) TextBlockDays.Foreground = Brushes.Yellow;
        if (timeSpan.Days < 100 && timeSpan.Days >= 30) TextBlockDays.Foreground = Brushes.Orange;
        if (timeSpan.Days < 30) TextBlockDays.Foreground = Brushes.Red;

        TextBlockDays.Text = timeSpan.Days.ToString();
        TextBlockDaysDual.Text = timeSpan.Days.ToString();
        TextBlockHours.Text = timeSpan.Hours.ToString().PadLeft(2, '0');
        TextBlockHoursDual.Text = timeSpan.Hours.ToString().PadLeft(2, '0');
        TextBlockMins.Text = timeSpan.Minutes.ToString().PadLeft(2, '0');
        TextBlockMinsDual.Text = timeSpan.Minutes.ToString().PadLeft(2, '0');
        TextBlockSecs.Text = timeSpan.Seconds.ToString().PadLeft(2, '0');
        TextBlockSecsDual.Text = timeSpan.Seconds.ToString().PadLeft(2, '0');
        TextBlockMs.Text = timeSpan.Milliseconds.ToString().PadLeft(3, '0');
        TextBlockMsDual.Text = timeSpan.Milliseconds.ToString().PadLeft(3, '0');
        //TODO: Year
    }

    private static int GetYear()
    {
        TimeSpan timeSpan = new DateTime(DateTime.Now.Year, 6, 7, 9, 0, 0) - DateTime.Now;
        if (timeSpan.TotalSeconds < 0) return DateTime.Now.Year + 1;
        else return DateTime.Now.Year;
    }

    private void MainWindow_Loaded(object? sDualder, EventArgs e)
    {
        Position = new(0, 100);
        Width = Screens.Primary.WorkingArea.Width;
        Icon = new WindowIcon("Resources/icon.ico");
        new ConfigWindow(this).ShowDialog(this);
    }

    private void OnPointerPressed(object sDualder, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsRightButtonPressed)
        {
            new ConfigWindow(this).ShowDialog(this);
        }
    }

    public void SetTextBlockMsVisibility(bool isVisible) => TextBlockMs.IsVisible = isVisible;
    public bool GetTextBlockMsVisibility() => TextBlockMs.IsVisible;
    public void SetTextBlockSecsVisibility(bool isVisible) => TextBlockSecs.IsVisible = isVisible;
    public bool GetTextBlockSecsVisibility() => TextBlockSecs.IsVisible;
    public void SetTextBlockMinsVisibility(bool isVisible) => TextBlockMins.IsVisible = isVisible;
    public bool GetTextBlockMinsVisibility() => TextBlockMins.IsVisible;
    public void SetTextBlockHoursVisibility(bool isVisible) => TextBlockHours.IsVisible = isVisible;
    public bool GetTextBlockHoursVisibility() => TextBlockHours.IsVisible;
    public void SetTextBlockDaysVisibility(bool isVisible) => TextBlockDays.IsVisible = isVisible;
    public bool GetTextBlockDaysVisibility() => TextBlockDays.IsVisible;
    public void SetTextBlockYearsVisibility(bool isVisible) => TextBlockYears.IsVisible = isVisible;
    public bool GetTextBlockYearsVisibility() => TextBlockYears.IsVisible;


    public void SetTextBlockMsDualVisibility(bool isVisible) => TextBlockMsDual.IsVisible = isVisible;
    public bool GetTextBlockMsDualVisibility() => TextBlockMs.IsVisible;
    public void SetTextBlockSecsDualVisibility(bool isVisible) => TextBlockSecsDual.IsVisible = isVisible;
    public bool GetTextBlockSecsDualVisibility() => TextBlockSecs.IsVisible;
    public void SetTextBlockMinsDualVisibility(bool isVisible) => TextBlockMinsDual.IsVisible = isVisible;
    public bool GetTextBlockMinsDualVisibility() => TextBlockMins.IsVisible;
    public void SetTextBlockHoursDualVisibility(bool isVisible) => TextBlockHoursDual.IsVisible = isVisible;
    public bool GetTextBlockHoursDualVisibility() => TextBlockHours.IsVisible;
    public void SetTextBlockDaysDualVisibility(bool isVisible) => TextBlockDaysDual.IsVisible = isVisible;
    public bool GetTextBlockDaysDualVisibility() => TextBlockDays.IsVisible;
    public void SetTextBlockYearsDualVisibility(bool isVisible) => TextBlockYearsDual.IsVisible = isVisible;
    public bool GetTextBlockYearsDualVisibility() => TextBlockYears.IsVisible;

    public void SetTextBlockMainVisibility(bool isVisible) => TextBlockMain.IsVisible = isVisible;
    public bool GetTextBlockMainVisibility() => TextBlockMain.IsVisible;

    public void SetTextBlockDualVisibility(bool isVisible) => TextBlockDual.IsVisible = isVisible;
    public bool GetTextBlockDualVisibility() => TextBlockDual.IsVisible;
}