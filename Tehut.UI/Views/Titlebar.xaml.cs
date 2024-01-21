using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tehut.UI.Views
{
    /// <summary>
    /// Interaction logic for Titlebar.xaml
    /// </summary>
    public partial class Titlebar : UserControl
    {
        private bool restoreForDragMove;

        private MainWindow mainWindow;

        public Titlebar()
        {
            InitializeComponent();
        }

        public void SetMainWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        #region Dragging

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                mainWindow.WindowState = mainWindow.WindowState is WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
                return;
            }

            restoreForDragMove = mainWindow.WindowState is WindowState.Maximized;

            mainWindow.DragMove();
        }

        private void TitleBar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            restoreForDragMove = false;
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (restoreForDragMove)
            {
                restoreForDragMove = false;

                var screenLocation = PointToScreen(e.MouseDevice.GetPosition(this));

                mainWindow.Left = screenLocation.X <= SystemParameters.WorkArea.Width * 0.5 ? 0 : SystemParameters.WorkArea.Width - mainWindow.RestoreBounds.Width;
                mainWindow.Top = 0;

                mainWindow.WindowState = WindowState.Normal;

                mainWindow.DragMove();
            }
        }

        #endregion 

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow.WindowState is WindowState.Maximized)
            {
                mainWindow.WindowState = WindowState.Normal;
            }
            else
            {
                mainWindow.WindowState = WindowState.Maximized;
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.WindowState = WindowState.Minimized;
        }
    }
}
