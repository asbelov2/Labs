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
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace lab7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string userName = "";
        static TcpClient client = new TcpClient();
        static internal NetworkStream stream;
        static string recievedText = "";
        Thread recieving;

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateText);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Start();
        }

        private void UpdateText(object sender, EventArgs e)
        {
            if(recievedText!="")
            {
                Text.AppendText(recievedText);
                recievedText = "";
            }
        }

        // отправка сообщений
        private void SendMessage(string text)
        {
            byte[] data = Encoding.Unicode.GetBytes(text);
            stream.Write(data, 0, data.Length);
        }

        // получение сообщений
        private void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    recievedText += message + "\n";
                }
                catch (Exception e)
                {
                    MessageBox.Show("Подключение прервано!"+e.ToString()); //соединение было прервано
                    Disconnect();
                    ToggleButtons();
                }
            }
        }

        void ToggleButtons()
        {
            Connect.IsEnabled = !Connect.IsEnabled;
            DisconnectB.IsEnabled = !DisconnectB.IsEnabled;
            Send.IsEnabled = !Send.IsEnabled;
            if (!Connect.IsEnabled)
            {
                isConnected.Content = "Status: CONNECTED";
            }
            else
            {
                isConnected.Content = "Status: NO CONNECTION";
            }
        }

        void Disconnect()
        {
            recieving.Abort();
            if (stream != null)
                stream.Close();//отключение потока
            if (client != null)
                client.Close();//отключение клиента
        }

        // Connect
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            client = new TcpClient();
            userName = Nickname.Text;
            byte[] IPaddr = new byte[4];
            int i = 0;
            foreach(string s in IP.Text.Split('.'))
            {
                byte.TryParse(s,out IPaddr[i]);
                i++;
            }
            client.Connect(new IPAddress(IPaddr), int.Parse(Port.Text));
            stream = client.GetStream();
            SendMessage(userName);
            recieving = new Thread(new ThreadStart(ReceiveMessage));
            recieving.Start();
            ToggleButtons();
        }

        // Disconnect
        private void DisconnectB_Click(object sender, RoutedEventArgs e)
        {
            Disconnect();
            ToggleButtons();
        }

        //Send
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(Message.Text);
            Message.Text = "";
        }

        private void Message_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                SendMessage(Message.Text);
                Message.Text = "";
            }
        }
    }
}
