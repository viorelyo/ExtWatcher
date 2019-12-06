using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace WpfAppDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const String APP_ID = "Microsoft.Samples.DesktopToastsSample";
        private Toast toast;

        public MainWindow()
        {
            InitializeComponent();
            ShowToastButton.Click += ShowToastButton_Click;
            toast = new Toast();
        }

        private void ShowToastButton_Click(object sender, RoutedEventArgs e)
        {
            toast.ShowNotification();
        }
    }
}
