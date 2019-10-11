using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace lab5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
         * Задание: разработать графический интерфейс приложения для работы с
         * БД. Обязательные требования: обеспечить возможность добавления новых
         * записей в базу данных, обеспечить возможность просмотра текущего
         * варианта базы данных, обеспечить синтаксически правильный ввод данных в
         * компоненты редактирования. 
         * 
         * 2) В группе студентов определить средний балл каждого. Упорядочить
         * список студентов по убыванию среднего балла. Вывести ФИО студентов, у
         * которых больше одной тройки. 
         */
        ApplicationContext db;

        ApplicationContext dbtmp;

        public MainWindow()
        {
            InitializeComponent();

            db = new ApplicationContext();
            db.Students.Load();
            dbtmp = new ApplicationContext();
            dbtmp.Students.Load();
            double avg = 0;
            foreach (Student st in db.Students)
            {
                avg += st.Math;
                avg += st.English;
                avg += st.Programming;
                avg += st.Physical_education;
                avg += st.Physics;
                avg += st.Art;
                avg += st.Chemistry;
                st.Average_score = Math.Round(avg / 7,2);
                avg = 0;
            }
            db.SaveChanges();
            var sortedDb = db.Students.OrderByDescending(s => s.Average_score);
            
            foreach (Student st in dbtmp.Students)
            {
                dbtmp.Students.Remove(st);
            }
            foreach (Student st in sortedDb)
            {
                dbtmp.Students.Add(st);
            }

            DataContext = dbtmp.Students.Local.ToBindingList();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            db = new ApplicationContext();
            db.Students.Load();
            dbtmp = new ApplicationContext();
            dbtmp.Students.Load();
            double avg = 0;
            foreach (Student st in db.Students)
            {
                avg += st.Math;
                avg += st.English;
                avg += st.Programming;
                avg += st.Physical_education;
                avg += st.Physics;
                avg += st.Art;
                avg += st.Chemistry;
                st.Average_score = Math.Round(avg / 7, 2);
                avg = 0;
            }
            db.SaveChanges();
            var sortedDb = db.Students.OrderByDescending(s => s.Average_score);

            foreach (Student st in dbtmp.Students)
            {
                dbtmp.Students.Remove(st);
            }
            foreach (Student st in sortedDb)
            {
                dbtmp.Students.Add(st);
            }

            DataContext = dbtmp.Students.Local.ToBindingList();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            StudentWindow studentWindow = new StudentWindow(new Student());
            if (studentWindow.ShowDialog() == true)
            {
                Student student = studentWindow.Student;
                db.Students.Add(student);
                db.SaveChanges();
            }
            Refresh_Click(sender, e);
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (studentsList.SelectedItem == null) return;

            Student student = studentsList.SelectedItem as Student;

            StudentWindow studentWindow = new StudentWindow(new Student
            {
                Id = student.Id,
                Name = student.Name,
                Group = student.Group,
                Math = student.Math,
                English = student.English,
                Programming = student.Programming,
                Physical_education = student.Physical_education,
                Physics = student.Physics,
                Art = student.Art,
                Chemistry = student.Chemistry,
                Average_score = student.Average_score
            });

            if (studentWindow.ShowDialog() == true)
            {
                student = db.Students.Find(studentWindow.Student.Id);
                if (student != null)
                {
                    student.Name = studentWindow.Student.Name;
                    student.Group = studentWindow.Student.Group;
                    student.Math = studentWindow.Student.Math;
                    student.English = studentWindow.Student.English;
                    student.Programming = studentWindow.Student.Programming;
                    student.Physical_education = studentWindow.Student.Physical_education;
                    student.Physics = studentWindow.Student.Physics;
                    student.Art = studentWindow.Student.Art;
                    student.Chemistry = studentWindow.Student.Chemistry;
                    student.Average_score = studentWindow.Student.Average_score;
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            Refresh_Click(sender, e);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (studentsList.SelectedItem == null) return;
            Student student = studentsList.SelectedItem as Student;
            db.Students.Remove(student);
            db.SaveChanges();
            Refresh_Click(sender, e);
        }

        private void Debtors_Click(object sender, RoutedEventArgs e)
        {
            DbSet<Student> tmp = db.Students;
            foreach(Student st in tmp)
            {
                if (Threes(st) <= 1) tmp.Remove(st);
            }
            DataContext = tmp.Local.ToBindingList();
        }

        private int Threes(Student student)
        {
            int threes = 0;
            if (student.Math == 3) threes++;
            if (student.English == 3) threes++;
            if (student.Programming == 3) threes++;
            if (student.Physical_education == 3) threes++;
            if (student.Physics == 3) threes++;
            if (student.Art == 3) threes++;
            if (student.Chemistry == 3) threes++;
            return threes;
        }
    }
}
