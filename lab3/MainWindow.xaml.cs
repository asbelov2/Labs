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
using Microsoft.Win32;
namespace lab3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string FilePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)       // NEW FILE
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)      // OPEN FILE
        {

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)      // SAVE FILE
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)      // EXIT
        {

        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)      // CHANGE COLOR
        {

        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)      // CHANGE FONT
        {

        }
    }
}
