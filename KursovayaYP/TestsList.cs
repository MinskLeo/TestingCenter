using System;
using System.IO;
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
        private static string ID;//Идентификатор студента
        private static string[] TEST;//Массив содержащий сам тест целиком
        public static string TestName;//Название теста

        public TestsList(int port, string id)//Конструктор, в который мы передаем из предыдущего окна некоторые параметры
        {
            InitializeComponent();
            Port = port;
            ID = id;
        }

        private void TestsList_Load(object sender, EventArgs e)//Метод вызываемый при полной загрузке окна формы
        {
            try
            {
                TcpClient tcp = new TcpClient() { ReceiveTimeout = 5000 };//Для создания клиентской программы, работающей по протоколу TCP, предназначен класс TcpClient
                tcp.Connect("127.0.0.1", Convert.ToInt32(Port));//Подключем приложение к серверу, при помощи указания IP адреса и порта, по которому ведется прослушивание
                NetworkStream stream = tcp.GetStream();//Чтобы взаимодействовать с сервером, создаем объект класса NetworkStream, что позволяет нам отправлять и получать сообщения к\от серверу\а
                stream.ReadTimeout = 5000;
                string request = "testslist_" + ID;//Переменная, содержащая запрос на получение списка тестов

                stream.Write(Encoding.UTF8.GetBytes(request), 0, request.Length);//Отправка запроса на сервер

                BinaryFormatter formatter = new BinaryFormatter();//Объект для бинарной сериализации\десериализации
                List<string> testslist = (List<string>)formatter.Deserialize(stream);
                for(int i=0;i<testslist.Count;i++)
                {
                    string[] buf = testslist[i].Split('\\','_','.');
                    testslist[i] = buf[1] + "_" + buf[2] + "_" + buf[3];
                }
                list_Tests.Items.AddRange(testslist.ToArray());//Метод, добавляющий в список элементов массив (список приведенный к массиву) с названиями тестов

                stream.Close();
                tcp.Close();
                list_Tests.SelectedIndex = 0;//Выделяем первый элемент на форме
            }
            catch(SocketException)
            {
                ErrorSocket(sender, e);//Передача управления методу, который обработает ошибку
            }
            catch(IOException)
            {
                ErrorSocket(sender, e);
            }
        }

        private void ErrorSocket(object sender, EventArgs e)
        {
            //Окно MessageBox является диалоговым и его метод .Show(...) может возвращать, в зависимости от параметра перечисления MessageBoxButtons различные значения
            //В данном случае используется MessageBoxButtons.RetryCancel, что означает, что у диалогового окна будет 2 кнопки: Retry(Повторить) и Cancel(Отменить)
            if (MessageBox.Show("Ошибка подключения. Сервер не отвечает", "Подключение", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
            {
                TestsList_Load(sender, e);//При нажатии кнопки Повторить управление будет передано этому блоку
            }
            else
            {
                this.Close();//Иначе - окно (Mainscreen) будет закрыто, чтобы исключить ошибочное взаимодействие пользователя с локальными данными без соединения с сервером
            }
        }

        private void but_Choose_Click(object sender, EventArgs e)//Обработка нажатия на кнопку Выбрать
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect("127.0.0.1", Port);
                NetworkStream stream = client.GetStream();
                byte[] message = Encoding.UTF8.GetBytes("test_"+list_Tests.SelectedItem.ToString());
                TestName = list_Tests.SelectedItem.ToString();//Считываем название теста для запроса
                stream.Write(message,0,message.Length);


                BinaryFormatter formatter = new BinaryFormatter();
                TEST = ((string[])formatter.Deserialize(stream));//Формируем массив строк содержащий сам тест
                stream.Close();
                client.Close();

                //Обработка полученных данных в массиве TEST
                if(TEST.Length!=0 || TEST!=null)
                {
                    Test test = new Test(ID, TEST, Port);//Создаем объект окна самого теста
                    test.Disposed += new EventHandler(IfClosed);//При закрытии окна, происходит "распыление" объекта с помощью автоматически вызываемого метода Dispose. Мы можем уловить событие, возникающее при этом
                    this.Hide();
                    test.Show();
                }
                else//Если длина массива 0 или неопределена, значит тест поврежден
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
