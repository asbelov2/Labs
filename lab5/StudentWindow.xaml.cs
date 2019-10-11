using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace lab5
{
    /// <summary>
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        public Student Student { get; private set; }
        public StudentWindow(Student s)
        {
            InitializeComponent();
            Student = s;
            DataContext = Student;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Math_score_GotFocus(object sender, RoutedEventArgs e)
        {
            Math_score.Text = "";
        }

        private void Math_score_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key > 33) && ((int)e.Key < 40))
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void Math_score_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Math_score.Text.Length > 1)
            {
                Math_score.Text = Math_score.Text.Substring(0, 1);
            }
        }

        private void English_score_GotFocus(object sender, RoutedEventArgs e)
        {
            English_score.Text = "";
        }

        private void English_score_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key > 33) && ((int)e.Key < 40))
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void English_score_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (English_score.Text.Length > 1)
            {
                English_score.Text = English_score.Text.Substring(0, 1);
            }
        }

        private void Programming_score_GotFocus(object sender, RoutedEventArgs e)
        {
            Programming_score.Text = "";
        }

        private void Programming_score_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key > 33) && ((int)e.Key < 40))
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void Programming_score_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Programming_score.Text.Length > 1)
            {
                Programming_score.Text = Programming_score.Text.Substring(0, 1);
            }
        }

        private void PE_score_GotFocus(object sender, RoutedEventArgs e)
        {
            PE_score.Text = "";
        }

        private void PE_score_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key > 33) && ((int)e.Key < 40))
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void PE_score_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PE_score.Text.Length > 1)
            {
                PE_score.Text = PE_score.Text.Substring(0, 1);
            }
        }

        private void Physics_score_GotFocus(object sender, RoutedEventArgs e)
        {
            Physics_score.Text = "";
        }

        private void Physics_score_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key > 33) && ((int)e.Key < 40))
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void Physics_score_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Physics_score.Text.Length > 1)
            {
                Physics_score.Text = Physics_score.Text.Substring(0, 1);
            }
        }

        private void Art_score_GotFocus(object sender, RoutedEventArgs e)
        {
            Art_score.Text = "";
        }

        private void Art_score_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key > 33) && ((int)e.Key < 40))
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void Art_score_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Art_score.Text.Length > 1)
            {
                Art_score.Text = Art_score.Text.Substring(0, 1);
            }
        }

        private void Chemistry_score_GotFocus(object sender, RoutedEventArgs e)
        {
            Chemistry_score.Text = "";
        }

        private void Chemistry_score_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key > 33) && ((int)e.Key < 40))
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void Chemistry_score_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Chemistry_score.Text.Length > 1)
            {
                Chemistry_score.Text = Chemistry_score.Text.Substring(0, 1);
            }
        }

        private void Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key > 43) && ((int)e.Key < 70))
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void Group_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key > 33) && ((int)e.Key < 70) || (e.Key==Key.OemMinus))
            {

            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
