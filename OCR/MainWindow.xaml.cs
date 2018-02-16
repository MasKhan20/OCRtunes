using OCR.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace OCR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            if (!Directory.Exists(@"C:\Teach\OCRsongs\"))
            {
                Directory.CreateDirectory(@"C:\Teach\OCRsongs\");
            }

            InitializeComponent();
            var viewmodel = new MainViewModel();
            DataContext = viewmodel;

            this.SizeToContent = SizeToContent.Height;
            this.SizeToContent = SizeToContent.Width;

        }

        private void CloseMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show(lstView.SelectedItem.ToString());
        }
    }
}
