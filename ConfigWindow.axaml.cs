using Avalonia.Controls;

namespace Unicount
{
    public partial class ConfigWindow : Window
    {
        public ConfigWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            TextBlockMainBtn.IsChecked = mainWindow.GetTextBlockMainVisibility();
            TextBlockDualBtn.IsChecked = mainWindow.GetTextBlockDualVisibility();

            TextBlockMsBtn.IsChecked = mainWindow.GetTextBlockMsVisibility();
            TextBlockSecsBtn.IsChecked = mainWindow.GetTextBlockSecsVisibility();
            TextBlockMinsBtn.IsChecked = mainWindow.GetTextBlockMinsVisibility();
            TextBlockHoursBtn.IsChecked = mainWindow.GetTextBlockHoursVisibility();
            TextBlockDaysBtn.IsChecked = mainWindow.GetTextBlockDaysVisibility();
            TextBlockYearsBtn.IsChecked = mainWindow.GetTextBlockYearsVisibility();
            
            TextBlockMsDualBtn.IsChecked = mainWindow.GetTextBlockMsDualVisibility();
            TextBlockSecsDualBtn.IsChecked = mainWindow.GetTextBlockSecsDualVisibility();
            TextBlockMinsDualBtn.IsChecked = mainWindow.GetTextBlockMinsDualVisibility();
            TextBlockHoursDualBtn.IsChecked = mainWindow.GetTextBlockHoursDualVisibility();
            TextBlockDaysDualBtn.IsChecked = mainWindow.GetTextBlockDaysDualVisibility();
            TextBlockYearsDualBtn.IsChecked = mainWindow.GetTextBlockYearsDualVisibility();

            OkButton.Click += (s, e) => { Apply(mainWindow); Close(); };
        }

        private void Apply(MainWindow mainWindow)
        {
            mainWindow.SetTextBlockMainVisibility(TextBlockMainBtn.IsChecked ?? false);
            mainWindow.SetTextBlockDualVisibility(TextBlockDualBtn.IsChecked ?? false);

            mainWindow.SetTextBlockDaysVisibility(TextBlockDaysBtn.IsChecked ?? false);
            mainWindow.SetTextBlockHoursVisibility(TextBlockHoursBtn.IsChecked ?? false);
            mainWindow.SetTextBlockMinsVisibility(TextBlockMinsBtn.IsChecked ?? false);
            mainWindow.SetTextBlockSecsVisibility(TextBlockSecsBtn.IsChecked ?? false);
            mainWindow.SetTextBlockMsVisibility(TextBlockMsBtn.IsChecked ?? false);
            mainWindow.SetTextBlockYearsVisibility(TextBlockYearsBtn.IsChecked ?? false);
            
            mainWindow.SetTextBlockDaysDualVisibility(TextBlockDaysDualBtn.IsChecked ?? false);
            mainWindow.SetTextBlockHoursDualVisibility(TextBlockHoursDualBtn.IsChecked ?? false);
            mainWindow.SetTextBlockMinsDualVisibility(TextBlockMinsDualBtn.IsChecked ?? false);
            mainWindow.SetTextBlockSecsDualVisibility(TextBlockSecsDualBtn.IsChecked ?? false);
            mainWindow.SetTextBlockMsDualVisibility(TextBlockMsDualBtn.IsChecked ?? false);
            mainWindow.SetTextBlockYearsDualVisibility(TextBlockYearsDualBtn.IsChecked ?? false);
        }
    }
}