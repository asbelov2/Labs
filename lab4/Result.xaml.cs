using System;
using System.IO;
using System.Windows;

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для Result.xaml
    /// </summary>
    public partial class Result : Window
    {
        static public bool isDone = false;
        public Result()
        {
            StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "result.txt");
            InitializeComponent();
            if (sr != null)
            {
                Results.Text = sr.ReadToEnd();
            }
            else
            {
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "result.txt",false);
                sw.Close();
            }
            sr.Close();
            Congrats.Content += MainWindow.killedBats.ToString() + " bats.";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "result.txt",true);
            sw.WriteLine(Nick.Text + " " + MainWindow.killedBats.ToString());
            sw.Close();
            isDone = true;
            Close();
        }
    }
}
