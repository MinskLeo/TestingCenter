using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursovayaYP
{
    public partial class TestsScreen : Form
    {
        TcpClient client = new TcpClient();
        private static int Port=8888;

        public TestsScreen(int port)
        {
            InitializeComponent();
            Port = port;
        }

        private void TestsScreen_Load(object sender, EventArgs e)
        {
            //Вопрос
            //Кол-во ответов
            //Ответ1
            //ПравильностьОтвета1
            //Ответ2
            //ПравильностьОтвета2
            //Ответ3
            //ПравильностьОтвета3
            client.Connect("127.0.0.1",Port);
            
        }
    }
}
