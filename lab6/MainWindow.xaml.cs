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
using CsvHelper;
using System.Collections;
using System.IO;
using Microsoft.Win32;

namespace lab6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        double fromSec = 0;
        double toSec = 0;
        double vFrom = -128;
        double vTo = 128;

        OscilloscopeData osc;
        /*
         * В программе должна быть реализована
         * визуализация сигнала осциллографа с возможностью изменения масштаба.
         */
        
        public MainWindow()
        {
            InitializeComponent();
            Up_Border.Content = "128";
            Down_Border.Content = "-128";
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            string filePath;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
            }
            else return;
            List<Channels> data = new List<Channels>();
            int[] beginData = new int[11];
            // Reading from csv to "OscilloscopeData" object
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                using (CsvReader csvReader = new CsvReader(streamReader))
                {
                    csvReader.Configuration.Delimiter = ",";
                    csvReader.Read(); // Read for skipping row
                    for (int i = 0; i < 10; i++)
                    {
                        beginData[i] = csvReader.GetRecord<int>();
                        csvReader.Read();
                    }
                    beginData[10] = csvReader.GetRecord<int>();

                    csvReader.Configuration.HasHeaderRecord = false;

                    IEnumerable records = csvReader.GetRecords<Channels>().ToList();

                    foreach (Channels record in records)
                    {
                        data.Add(record);
                    }

                    osc = new OscilloscopeData(beginData,data);
                    freq.Content = "frequency: " + osc.frequency.ToString();
                }
            }

            toSec = osc.getTime();
            To.Text = toSec.ToString();
            Left_Border.Content = "0";
            Right_Border.Content = toSec.ToString();
            Build.IsEnabled = true;

            
            var pixelWidth = GraphA.ActualWidth;
            var pixelHeight = GraphA.ActualHeight;
            Line[] zeroLine = new Line[4];
            for (int i = 0; i < 4; i++)
            {
                zeroLine[i] = new Line();
                zeroLine[i].X1 = 0; zeroLine[i].X2 = pixelWidth;
                zeroLine[i].Y1 = pixelHeight - osc.zeroLevel[i] * pixelHeight / 256; zeroLine[i].Y2 = pixelHeight - osc.zeroLevel[i] * pixelHeight / 256;
                zeroLine[i].Stroke = Brushes.Black;
                canvas.Children.Add(zeroLine[i]);
            }
            // Drawing grid
            for (int i = 32; i <= 256; i += 32)
            {
                Line tmp = new Line();
                tmp.X1 = 0; tmp.X2 = pixelWidth;
                tmp.Y1 = i * pixelHeight / 256; tmp.Y2 = i * pixelHeight / 256;
                tmp.Stroke = Brushes.Black;
                canvas.Children.Add(tmp);
            }
        }

        public void UpdateGraph()
        {
            var pixelWidth = GraphA.ActualWidth;
            var pixelHeight = GraphA.ActualHeight;
            double Height = vTo - vFrom;


            // Drawing graph
            PointCollection a_points = new PointCollection((int)pixelWidth + 1);
            PointCollection b_points = new PointCollection((int)pixelWidth + 1);
            PointCollection c_points = new PointCollection((int)pixelWidth + 1);
            PointCollection d_points = new PointCollection((int)pixelWidth + 1);
            int frames = (int)((toSec - fromSec) * osc.frequency);
            int currentFrame = (int)(fromSec * osc.frequency);
            for (int i =  currentFrame; i < currentFrame + frames; i++)
            {
                var x = (i-currentFrame) * pixelWidth / frames;
                double y = 0;
                if (A.IsChecked.Value)
                {
                    y = pixelHeight - (((osc.channelsData[i].A - osc.zeroLevel[0]) * (256 / Height) * (pixelHeight / 256)) + osc.zeroLevel[0] * pixelHeight / 256);
                    a_points.Add(new Point(x, y));
                }
                if (B.IsChecked.Value)
                {
                    y = pixelHeight - (((osc.channelsData[i].B - osc.zeroLevel[1]) * (256 / Height) * (pixelHeight / 256)) + osc.zeroLevel[1] * pixelHeight / 256);
                    b_points.Add(new Point(x, y));
                }
                if (C.IsChecked.Value)
                {
                    y = pixelHeight - (((osc.channelsData[i].C - osc.zeroLevel[2]) * (256 / Height) * (pixelHeight / 256)) + osc.zeroLevel[2] * pixelHeight / 256);
                    c_points.Add(new Point(x, y));
                }
                if (D.IsChecked.Value)
                {
                    y = pixelHeight - (((osc.channelsData[i].D - osc.zeroLevel[3]) * (256 / Height) * (pixelHeight / 256)) + osc.zeroLevel[3] * pixelHeight / 256);
                    d_points.Add(new Point(x, y));
                }
            }
            GraphA.Points = a_points;
            GraphB.Points = b_points;
            GraphC.Points = c_points;
            GraphD.Points = d_points;
        }

        public class OscilloscopeData
        {
            public List<Channels> channelsData { get; }
            public int frequency { get;}
            public int[] sensivity { get;}
            public int[] zeroLevel { get;}
            public int prerecordLength { get;}
            public int postrecordLength { get;}

            public OscilloscopeData(int[] beginData, List<Channels> channels)
            {
                sensivity = new int[4];
                zeroLevel = new int[4];
                frequency = freqCode[beginData[0]];
                sensivity[0] = sensCodes[beginData[1]];
                sensivity[1] = sensCodes[beginData[2]];
                sensivity[2] = sensCodes[beginData[5]];
                sensivity[3] = sensCodes[beginData[6]];
                zeroLevel[0] = beginData[3];
                zeroLevel[1] = beginData[4];
                zeroLevel[2] = beginData[7];
                zeroLevel[3] = beginData[8];
                prerecordLength = beginData[9];
                postrecordLength = beginData[10];
                channelsData = channels;
            }

            public double getTime()
            {
                return (double)channelsData.Count / frequency;
            }

            static int[] sensCodes =   // in mV
        {
            2,
            5,
            10,
            20,
            50,
            100,
            200,
            500,
            1000,
            2000,
            5000,
            10000
        };

            static int[] freqCode =    // in KHz
            {
            10000000,
            5000000,
            2000000,
            1000000,
            500000,
            200000,
            100000,
            50000,
            20000,
            10000,
            5000,
            2000,
            1000,
            500,
            200,
            100,
            50,
            20,
            10,
            5,
            2,
            1
        };
        }

        public class Channels
        {
            public int A { get; set; }
            public int B { get; set; }
            public int C { get; set; }
            public int D { get; set; }
        }

        private void To_TextChanged(object sender, TextChangedEventArgs e)
        {
            double.TryParse(To.Text, out toSec);
        }

        private void From_TextChanged(object sender, TextChangedEventArgs e)
        {
            double.TryParse(From.Text, out fromSec);
        }

        private void From_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key > 33) && ((int)e.Key < 44) || e.Key==Key.OemComma)
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void To_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key > 33) && ((int)e.Key < 44) || e.Key == Key.OemComma)
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void Build_Click(object sender, RoutedEventArgs e)
        {
            UpdateGraph();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Add)
            {
                double dif = toSec - fromSec;
                dif = dif > 0.001 ? dif : 0.001;
                fromSec += dif / 4;
                toSec -= dif / 4;
                fromSec = Math.Round(fromSec, 4);
                toSec = Math.Round(toSec, 4);
                To.Text = toSec.ToString();
                From.Text = fromSec.ToString();
                UpdateGraph();
            }
            if (e.Key == Key.Subtract)
            {
                double dif = toSec - fromSec;
                dif = dif > 0.001 ? dif : 0.001;
                if ((fromSec - dif / 2) < 0)
                    fromSec = 0;
                else
                    fromSec -= dif / 2;
                if ((toSec + dif / 2) > osc.getTime())
                    toSec = osc.getTime();
                else
                    toSec += dif / 2;
                fromSec = Math.Round(fromSec, 4);
                toSec = Math.Round(toSec, 4);
                To.Text = toSec.ToString();
                From.Text = fromSec.ToString();
                UpdateGraph();
            }
            Left_Border.Content = fromSec.ToString() + " sec";
            Right_Border.Content = toSec.ToString() + " sec";
        }

        private void VFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            double.TryParse(VFrom.Text, out vFrom);
            Down_Border.Content = VFrom.Text;
        }

        private void VTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            double.TryParse(VTo.Text, out vTo);
            Up_Border.Content = VTo.Text;
        }

        private void VFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key > 33) && ((int)e.Key < 44) || e.Key == Key.OemComma || e.Key == Key.Subtract)
            {

            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
