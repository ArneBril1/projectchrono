using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Timer = System.Timers.Timer;
using System.IO.Ports;
using System.Windows.Threading;
using System.Xml;

namespace project_in_wpf2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _dispatcherTimer;
        
        private Timer timer;
        Stopwatch stopwatch1;
        SerialPort _serialport = new SerialPort("COM7");
        int a;
        public MainWindow()
        {
            InitializeComponent();
            timedisplay.Text = Class1.starttime;
            _serialport.Open();
            showlcd();
            timer1();
        }
        public void timer1()
        {
            stopwatch1 = new Stopwatch();
            timer = new Timer(interval: 100);
            timer.Elapsed += OnTimerElapse;
        }
        public void showlcd()
        {
            
            _serialport.WriteLine(timedisplay.Text);
        }
        public void OnTimerElapse(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            timedisplay.Text = stopwatch1.Elapsed.ToString(@"mm\:ss\:ff"));
        }
        public void Start_Click(object sender, RoutedEventArgs e)
        {
            DispatchTimer();
            timer.Start();
            stopwatch1.Start();
            Reset.IsEnabled = false;
            Stop.IsEnabled = true;
            Start.IsEnabled = false;
        }
        public void Stop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            Start.IsEnabled = true;
            Stop.IsEnabled = false;
            stopwatch1.Stop();
            Reset.IsEnabled = true;
            Textblock2.Inlines.Add(new Run { Text = timedisplay.Text });
            Textblock2.Inlines.Add(new LineBreak());
            a = a+1;
            if (a==5)
            {
                Textblock2.Text = "";
                a = 0;
            }
        }
        public void Reset_Click(object sender, RoutedEventArgs e)
        {
            stopwatch1.Reset();
            timedisplay.Text = Class1.starttime;
        }
        public void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void DispatchTimer()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);
            _dispatcherTimer.Tick += dispatcherTimer_Tick;
            _dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            showlcd(); 
        }
    }
}
