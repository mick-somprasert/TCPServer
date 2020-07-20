using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(ConnectAsClient));
            t.Start();
        }

        private void ConnectAsClient()
        {
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Parse("127.0.0.1"), 5000);
            updatUI("เชื่อมต่อเรียบร้อย");
            NetworkStream stream = client.GetStream();
            string str = "Helloword";
            byte[] message = Encoding.ASCII.GetBytes(str);
            stream.Write(message, 0, message.Length);
            updatUI(str + "Send Complete");
            stream.Close();
            client.Close();
        }

        private void updatUI(string v)
        {
            Func<int> del = delegate ()
            {
                textBox1.AppendText(v + System.Environment.NewLine);
                return 0;
            };
            Invoke(del);
        }
    }
}
