using System;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace KursovayaYP
{
    public partial class MainScreen : Form
    {
        public static string FirstName, Surname, MiddleName;
        private static int Port;
        private static string ID;

        public MainScreen(string surname, string name, string middleName, string id, int port)
        {
            InitializeComponent();
            FirstName = name;
            Surname = surname;
            MiddleName = middleName;
            ID = id;
            Port = port;

            this.Text = Surname + " " + FirstName + " " + MiddleName;

            this.Activated += new EventHandler(AllMarks);
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect("127.0.0.1", Port);
                NetworkStream stream = client.GetStream();
                byte[] message = Encoding.UTF8.GetBytes("mainscreen_" + ID);
                stream.Write(message, 0, message.Length);

                BinaryFormatter formatter = new BinaryFormatter();
                DataSet set = (DataSet)formatter.Deserialize(stream);
                data_DataGrid.DataSource = set.Tables[0];//Десериализируем объект из потока

                stream.Close();
                client.Close();
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Ошибка подключения. Сервер не отвечает.\n" + ex.Message + "\n" + ex.StackTrace, "Подключение", MessageBoxButtons.OK, MessageBoxIcon.Error);//DEBUG
            }
        }

        private void but_NewTest_Click(object sender, EventArgs e)
        {
            TestsList testsList = new TestsList(Port, ID);
            testsList.Disposed += new EventHandler(TestListClosed);

            this.Hide();
            testsList.Show();
        }

        private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void but_Update_Click(object sender, EventArgs e)
        {
            TableUpdating(sender,e); //Прошлый вариант - запрос на сервер актуальной таблицы
        }

        private void TestListClosed (object sender, EventArgs e)
        {
            this.Show();
        }

        private void AllMarks(object sender, EventArgs e)//DEBUG
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect("127.0.0.1", Port);
                NetworkStream stream = client.GetStream();
                byte[] message = Encoding.UTF8.GetBytes("mainscreen_" + ID);
                stream.Write(message, 0, message.Length);
                data_DataGrid.DataSource = null;

                BinaryFormatter formatter = new BinaryFormatter();
                DataSet set = (DataSet)formatter.Deserialize(stream);
                data_DataGrid.DataSource = set.Tables[0];//Десериализируем объект из потока. Пытаемся емае

                stream.Close();
                client.Close();
            }
            catch (SocketException ex)
            {
                if(MessageBox.Show("Ошибка подключения. Сервер не отвечает.\n" + ex.Message + "\n" + ex.StackTrace, "Подключение", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)==DialogResult.Retry)
                {
                    AllMarks(sender, e);
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void TableUpdating(object sender, EventArgs e)//DEBUG
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect("127.0.0.1", Port);
                NetworkStream stream = client.GetStream();
                byte[] message = Encoding.UTF8.GetBytes("dateresult_" + ID+"_"+dateTime_From.Value.Date.ToShortDateString()+"_"+dateTime_To.Value.Date.ToShortDateString());
                stream.Write(message, 0, message.Length);
                data_DataGrid.DataSource = null;//DEBUG

                BinaryFormatter formatter = new BinaryFormatter();
                DataSet set = (DataSet)formatter.Deserialize(stream);
                data_DataGrid.DataSource = null;
                data_DataGrid.DataSource = set.Tables[0];//Десериализируем объект из потока. Пытаемся емае

                stream.Close();
                client.Close();
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Ошибка подключения. Сервер не отвечает.\n" + ex.Message + "\n" + ex.StackTrace, "Подключение", MessageBoxButtons.OK, MessageBoxIcon.Error);//DEBUG
            }
        }
    }
}
