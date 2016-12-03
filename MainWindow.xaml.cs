using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace ClientTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        public async void SendMessage()
        {
            // 宛先の作成
            var remote = new IPEndPoint(IPAddress.Parse("192.168.3.30"), 810);

            // メッセージの準備
            var message = Encoding.UTF8.GetBytes("Hello world !");

            // UDPでメッセージ送信
            var client = new UdpClient(810);
            client.Connect(remote);
            await client.SendAsync(message, message.Length);
            client.Close();
        }

    }
}
