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

        UdpClient client;
        bool clientConnected = false;

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            client = new UdpClient(AddressFamily.InterNetwork);
            client.Connect("reiji-matlab.mydns.jp", 810);
            clientConnected = true;

            ListenMessage();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            clientConnected = false;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        public async void SendMessage()
        {
            // メッセージの準備
            var message = Encoding.UTF8.GetBytes("Hello world !");

            // UDPでメッセージ送信
            await client.SendAsync(message, message.Length);
        }

        public async void ListenMessage()
        {
            while (clientConnected)
            {
                // データ受信待機
                var result = await client.ReceiveAsync();

                // 受信したデータを変換
                var data = Encoding.UTF8.GetString(result.Buffer);

                ConsoleTb.Text += data + "\n";
            }
        }
    }
}
