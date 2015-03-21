namespace Shell.Views
{
    using System.Windows;
    using System.Windows.Input;

    using Microsoft.Windows.Shell;

    using Shell.ViewModels;

    public partial class ShellView : Window
    {
        public ShellView(ShellViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }

        private void Window_OnShowSystemMenuCommand(object sender, ExecutedRoutedEventArgs e)
        {
            const double systemMenuOffset = 24;
            var window = (Window)e.Parameter;
            if (window != null)
            {
                if (window.WindowState == WindowState.Maximized)
                {
                    Microsoft.Windows.Shell.SystemCommands.ShowSystemMenu(window, new Point(systemMenuOffset, systemMenuOffset));
                }
                else if (window.WindowState == WindowState.Normal)
                {
                    Microsoft.Windows.Shell.SystemCommands.ShowSystemMenu(
                        window, 
                        new Point(window.Left + systemMenuOffset, window.Top + systemMenuOffset));
                }
            }
        }

        private void Window_OnSystemCommandCloseWindow(object sender, ExecutedRoutedEventArgs e)
        {
            var window = (Window)e.Parameter;
            if (window != null)
            {
                Microsoft.Windows.Shell.SystemCommands.CloseWindow(window);
            }
        }

        private void Window_OnSystemCommandMaximizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            var window = (Window)e.Parameter;
            if (window != null)
            {
                Microsoft.Windows.Shell.SystemCommands.MaximizeWindow(window);
            }
        }

        private void Window_OnSystemCommandMinimizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            var window = (Window)e.Parameter;
            if (window != null)
            {
                Microsoft.Windows.Shell.SystemCommands.MinimizeWindow(window);
            }
        }

        private void Window_OnSystemCommandRestoreWindow(object sender, ExecutedRoutedEventArgs e)
        {
            var window = (Window)e.Parameter;
            if (window != null)
            {
                Microsoft.Windows.Shell.SystemCommands.RestoreWindow(window);
            }
        }
    }
}