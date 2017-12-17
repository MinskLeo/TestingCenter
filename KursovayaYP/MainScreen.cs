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
        public static string FirstName, Surname, MiddleName;//Переменные, хранящие Имя Фамилию и Отчество
        private static int Port;//Переменная с портом
        private static string ID;//Переменная содержащая ID студента

        public MainScreen(string surname, string name, string middleName, string id, int port)//Конструктор, в который мы передаем из предыдущего окна некоторые параметры
        {
            InitializeComponent();
            FirstName = name;
            Surname = surname;
            MiddleName = middleName;
            ID = id;
            Port = port;

            this.Text = Surname + " " + FirstName + " " + MiddleName;//Меняем заголовок окна

            /*Регистрируемся на событие, после активации окна. (скрытия\появления, переключение с другого окна на это) Это позволяет нам получать свежие данные с сервера при переключении окна, а так же
             исключает возможность работы с окном без соединения с сервером. Так же позволяет обновить данные в таблице после завершения теста
            */
            this.Activated += new EventHandler(AllMarks);
        }

        private void but_NewTest_Click(object sender, EventArgs e)//Метод, вызываемый при нажатии кнопки but_NewTest и отвечающий за появление окна со списком тестов
        {
            TestsList testsList = new TestsList(Port, ID);//Объект окна, предназначенного для вывода пользователю списка тестов
            testsList.Disposed += new EventHandler(TestListClosed);//Метод, вызываемый при распылении\уничтожении\закрытии окна со списком тестов

            this.Hide();//Скрываем окно с таблицей, чтобы не заграмождать обзор пользователю
            testsList.Show();//Показываем окно со списком тестов (объект созданный выше)
        }


        private void TestListClosed (object sender, EventArgs e)//Метод, срабатывающий при закрытии списка тестов
        {
            this.Show();//Отображаем эту форму, возвращая из скрытого вида
        }

        private void AllMarks(object sender, EventArgs e)//Метод, вызываемый при нажатии на кнопку Все оценки, а так же при активации формы (регистрация события произведена в конструкторе
        {
            try
            {
                TcpClient client = new TcpClient();//Для создания клиентской программы, работающей по протоколу TCP, предназначен класс TcpClient
                client.Connect("127.0.0.1", Port);//Подключем приложение к серверу, при помощи указания IP адреса и порта, по которому ведется прослушивание
                NetworkStream stream = client.GetStream();//Чтобы взаимодействовать с сервером, создаем объект класса NetworkStream, что позволяет нам отправлять и получать сообщения к\от серверу\а
                byte[] message = Encoding.UTF8.GetBytes("mainscreen_" + ID);//Программа кодирует в байтовый массив строку с запросом, содержащим комануд запроса и параметр(ы) запроса, разделенные через _ .
                stream.Write(message, 0, message.Length);//Записываем сообщение в поток, тем самым отправляя данные на сервер
                data_DataGrid.DataSource = null;//Обнуляем источник данных элемента-таблицы DataGrid в окне Mainscreen, чтобы заполнить новыми данными

                BinaryFormatter formatter = new BinaryFormatter();//Создаем объект для бинарной сериализации/десериализации
                DataSet set = (DataSet)formatter.Deserialize(stream);//Записываем десериализованные данные в объект типа DataSet
                //DataSet - "расположенное в оперативной памяти представление данных, обеспечивающее согласованную реляционную программную модель независимо от источника данных."
                data_DataGrid.DataSource = set.Tables[0];//Задаем источником данных для элемента DataGrid таблицу из десериализованного материала (переменной set типа DataSet)

                //Закрываем потоки
                stream.Close();
                client.Close();
            }
            catch (SocketException ex)//Сбор исключений типа SocketException
            {
                //Окно MessageBox является диалоговым и его метод .Show(...) может возвращать, в зависимости от параметра перечисления MessageBoxButtons различные значения
                //В данном случае используется MessageBoxButtons.RetryCancel, что означает, что у диалогового окна будет 2 кнопки: Retry(Повторить) и Cancel(Отменить)
                if(MessageBox.Show("Ошибка подключения. Сервер не отвечает.\n" + ex.Message + "\n" + ex.StackTrace, "Подключение", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)==DialogResult.Retry)
                {
                    AllMarks(sender, e);//При нажатии кнопки Повторить управление будет передано этому блоку
                }
                else
                {
                    this.Close();//Иначе - окно (Mainscreen) будет закрыто, чтобы исключить ошибочное взаимодействие пользователя с локальными данными без соединения с сервером
                }
            }
        }

        private void TableUpdating(object sender, EventArgs e)//Метод, вызываемый при нажатии кнопки but_Update и обновляющий данные в таблице в зависимости от данных в полях с датой От и По
        {
            try
            {
                TcpClient client = new TcpClient();
                
                client.Connect("127.0.0.1", Port);
                NetworkStream stream = client.GetStream();
                byte[] message = Encoding.UTF8.GetBytes("dateresult_" + ID+"_"+dateTime_From.Value.Date.ToShortDateString()+"_"+dateTime_To.Value.Date.ToShortDateString());
                stream.Write(message, 0, message.Length);
                data_DataGrid.DataSource = null;

                BinaryFormatter formatter = new BinaryFormatter();
                DataSet set = (DataSet)formatter.Deserialize(stream);
                data_DataGrid.DataSource = null;
                data_DataGrid.DataSource = set.Tables[0];

                stream.Close();
                client.Close();
            }
            catch (SocketException ex)
            {
                //Окно MessageBox является диалоговым и его метод .Show(...) может возвращать, в зависимости от параметра перечисления MessageBoxButtons различные значения
                //В данном случае используется MessageBoxButtons.RetryCancel, что означает, что у диалогового окна будет 2 кнопки: Retry(Повторить) и Cancel(Отменить)
                if (MessageBox.Show("Ошибка подключения. Сервер не отвечает.\n" + ex.Message + "\n" + ex.StackTrace, "Подключение", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                {
                    AllMarks(sender, e);//При нажатии кнопки Повторить управление будет передано этому блоку
                }
                else
                {
                    this.Close();//Иначе - окно (Mainscreen) будет закрыто, чтобы исключить ошибочное взаимодействие пользователя с локальными данными без соединения с сервером
                }
            }
        }
    }
}
