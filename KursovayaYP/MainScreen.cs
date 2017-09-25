using System;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
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
    public partial class MainScreen : Form
    {
        private static string FirstName, Surname, MiddleName;
        private static int Port;
        private static string ID;
        public static Form1 own;

        public MainScreen(string surname, string name, string middleName, string id, int port)//Не получается открывать то окно (логин скрин), которое мейновое, если закрыть это
        {
            InitializeComponent();
            FirstName = name;
            Surname = surname;
            MiddleName = middleName;
            ID = id;
            Port = port;

            this.Text = Surname + " " + FirstName + " " + MiddleName;

            //Определяем владельца
            own = this.Owner as Form1;
            //P.S.Попытка провалилась
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect("127.0.0.1", Port);
                NetworkStream stream = client.GetStream();
                byte[] message = Encoding.UTF8.GetBytes("mainscreen_"+ID);
                stream.Write(message, 0, message.Length);

                BinaryFormatter formatter = new BinaryFormatter();
                DataSet set = (DataSet)formatter.Deserialize(stream);
                data_DataGrid.DataSource = set.Tables[0];//Десериализируем объект из потока. Пытаемся емае

                stream.Close();
                client.Close();
            }
            catch(SocketException ex)
            {
                MessageBox.Show("Ошибка подключения. Сервер не отвечает.\n" + ex.Message + "\n" + ex.StackTrace, "Подключение", MessageBoxButtons.OK, MessageBoxIcon.Error);//DEBUG
            }
        }

        private void but_NewTest_Click(object sender, EventArgs e)
        {
            TestsList testsList = new TestsList(Port, ID);
            this.Hide();
            testsList.Show();
        }

        private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(own!=null)
            {
                own.Close();//Походу не воркает
            }
        }
    }
}
