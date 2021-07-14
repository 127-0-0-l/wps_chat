using System.Windows;
using ChatClient.ServiceChat;
using System.Windows.Input;

namespace ChatClient
{
    public partial class MainWindow : Window, IServiceChatCallback
    {
        bool isConnected = false;
        ServiceChatClient client;
        int ID;
        public MainWindow()
        {
            InitializeComponent();
        }

        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                bConDiscon.Content = "Disconnect";
                isConnected = true;
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                tbUserName.IsEnabled = true;
                bConDiscon.Content = "Connect";
                isConnected = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        public void MsgCallback(string msg)
        {
            rtbChat.AppendText(msg);
            for (int i = 0; i < 5; i++)
                rtbChat.LineDown();
        }

        private void Window_Colsing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void tbMessage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null && tbMessage.Text != "")
                {
                    client.SendMsg(tbMessage.Text, ID);
                    tbMessage.Clear();
                }
            }
        }
    }
}
