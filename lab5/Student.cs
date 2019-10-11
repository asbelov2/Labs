using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lab5
{
    public class Student : INotifyPropertyChanged
    {
        private string name;
        private string group;
        private int math;
        private int english;
        private int programming;
        private int physical_education;
        private int physics;
        private int art;
        private int chemistry;
        private double avg;
        public int Id { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Group
        {
            get { return group; }
            set
            {
                group = value;
                OnPropertyChanged("Group");
            }
        }

        public int Math
        {
            get { return math; }
            set
            {
                math = value;
                OnPropertyChanged("Math");
            }
        }

        public int English
        {
            get { return english; }
            set
            {
                english = value;
                OnPropertyChanged("English");
            }
        }

        public int Programming
        {
            get { return programming; }
            set
            {
                programming = value;
                OnPropertyChanged("Programming");
            }
        }

        public int Physical_education
        {
            get { return physical_education; }
            set
            {
                physical_education = value;
                OnPropertyChanged("Physical_education");
            }
        }
        public int Physics
        {
            get { return physics; }
            set
            {
                physics = value;
                OnPropertyChanged("Physics");
            }
        }

        public int Art
        {
            get { return art; }
            set
            {
                art = value;
                OnPropertyChanged("Art");
            }
        }

        public int Chemistry
        {
            get { return chemistry; }
            set
            {
                chemistry = value;
                OnPropertyChanged("Chemistry");
            }
        }

        public double Average_score
        {
            get { return avg; }
            set
            {
                avg = value;
                OnPropertyChanged("Average_score");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
