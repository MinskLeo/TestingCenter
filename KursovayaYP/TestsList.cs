using System;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace KursovayaYP
{
    public partial class TestsList : Form
    {

        private static int Port = 8888;
        private static string ID;
        private static string[] TEST;
        public static string TestName;

        public TestsList(int port, string id)
        {
            InitializeComponent();
            Port = port;
            ID = id;
        }

        private void TestsList_Load(object sender, EventArgs e)
        {
            try
            {
                TcpClient tcp=new TcpClient();
                tcp.Connect("127.0.0.1", Convert.ToInt32(Port));
                NetworkStream stream = tcp.GetStream();
                string request = "testslist_" + ID;

                stream.Write(Encoding.UTF8.GetBytes(request), 0, request.Length);

                while (!stream.DataAvailable)
                {
                    //Просто чтобы клиент подождал пока придет обработка с сервера
                }

                BinaryFormatter formatter = new BinaryFormatter();
                List<string> testslist = (List<string>)formatter.Deserialize(stream);
                for(int i=0;i<testslist.Count;i++)
                {
                    string[] buf = testslist[i].Split('\\','_','.');
                    testslist[i] = buf[1] + "_" + buf[2] + "_" + buf[3];
                }
                list_Tests.Items.AddRange(testslist.ToArray());

                stream.Close();
                tcp.Close();
                list_Tests.SelectedIndex = 0;
            }
            catch(SocketException ex)
            {
                MessageBox.Show("Ошибка подключения. Сервер не отвечает.\n" + ex.Message + "\n" + ex.StackTrace, "Подключение", MessageBoxButtons.OK, MessageBoxIcon.Error);//DEBUG
            }
        }

        private void but_Choose_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect("127.0.0.1", Port);
                NetworkStream stream = client.GetStream();
                byte[] message = Encoding.UTF8.GetBytes("test_"+list_Tests.SelectedItem.ToString());
                TestName = list_Tests.SelectedItem.ToString();//Название нашего теста
                stream.Write(message,0,message.Length);

                while(!stream.DataAvailable)
                {
                    
                }

                BinaryFormatter formatter = new BinaryFormatter();
                TEST = ((string[])formatter.Deserialize(stream));
                stream.Close();
                client.Close();

                //Обработка полученных данных
                if(TEST.Length!=0 || TEST!=null)
                {
                    Test test = new Test(ID, TEST, Port);
                    test.Disposed += new EventHandler(IfClosed);//При закрытии окна, происходит "распыление" объекта с помощью автоматически вызываемого метода Dispose. Мы можем уловить событие, возникающее при этом
                    this.Hide();
                    test.Show();
                }
                else
                {
                    if(MessageBox.Show("Некорректный тест!","Тест", MessageBoxButtons.RetryCancel,MessageBoxIcon.Error)==DialogResult.Retry)
                    {
                        but_Choose_Click(sender, e);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch(SocketException ex)
            {
                MessageBox.Show("Ошибка подключения. Сервер не отвечает.\n" + ex.Message + "\n" + ex.StackTrace, "Подключение", MessageBoxButtons.OK, MessageBoxIcon.Error);//DEBUG
            }
        }

        private void IfClosed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void but_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
