using System;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

namespace KursovayaYP
{
    public partial class Form1 : Form
    {
        private static int Port=8888;//Базовый порт
        public static string id;//Поле, предназначенное для хранения ID студента
        private static Button EXIT = new Button();//Заранее созданный элемент управления Кнопка которую мы будем активировать при помощи клавиши Esc

        public Form1()
        {
            InitializeComponent();
            /*
             Файл с настройками программы. Первая строка является значением, представляющим порт.
             Такой способ считывания, позволит не допустить исключения
             Тем не менее, блок try catch необходим для перестраховки
             */
            try
            {
                if (File.Exists("settings.ini"))
                {
                    string[] set = File.ReadAllLines("settings.ini");
                    Port = Convert.ToInt32(set[0]);
                }
            }
            catch (FormatException)
            {
                Port = 8888;
            }
            EXIT.Click += new EventHandler(Exit_button);//Регистрация события на нажатие клавиши Esc
            this.CancelButton = EXIT;//Меняем параметр выход-кнопки у формы
        }

        private void Exit_button(object sender, EventArgs e)//Метод, обрабатывающий нажатие клавиши Esc
        {
            this.Close();
        }

        private void but_Login_Click(object sender, EventArgs e)//Метод, обрабатывающий нажатие кнопки Войти
        {
            try
            {
                but_Login.Enabled = false;//"Выключаем" кнопку Войти, чтобы пользователь не смог подряд нажать несколько раз на кнопку, тем самым отправив несколько запросов
                TcpClient client = new TcpClient();//Для создания клиентской программы, работающей по протоколу TCP, предназначен класс TcpClient
                if (client.Connected == false)//Проверка на отстутствие соединения, на случай если оно существует
                {
                    client.Connect("127.0.0.1", Port);//Подключем приложение к серверу, при помощи указания IP адреса и порта, по которому ведется прослушивание
                }

                if (mtb_StudNumb.Text.Length == 14)//Проверка на количество символов (заполненность) поля mtb_StudNumb (поле для введения номера студенческого)
                {
                    NetworkStream stream = client.GetStream();//Чтобы взаимодействовать с сервером, создаем объект класса NetworkStream, что позволяет нам отправлять и получать сообщения к\от серверу\а
                     byte[] message = Encoding.UTF8.GetBytes("login_" + mtb_StudNumb.Text);//Программа кодирует в байтовый массив строку с запросом, содержащим комануд запроса и параметр(ы) запроса, разделенные через _ .
                     stream.Write(message, 0, message.Length);//Специальная комбинация "оператор_данные": например, login_20169876654237

                     byte[] data = new byte[256];//Заранее подготовленный байтовый массив для записи в него сообщения ответа от сервера.
                     stream.Read(data, 0, data.Length);//Метод, предназначенный для считывания данных из потока, принимающий 3 параметра: массив в который ведется запись, отступ от начала, размер считываемого отрезка
                     string answ = Encoding.UTF8.GetString(data, 0, data.Length);//Декодируем последовательность байтов в строку из массива

                    //Сервер может вернуть либо login_Имя_Фамилия_Отчество, либо login_NNN что означает что студент по данному ID не найден
                    if (answ.CompareTo("login_NNN")==0)//Разбор ответа. Проверка на отрицательный результат
                    {
                        //Сообщение об ошибке
                        MessageBox.Show("Студент не найден, повторите ввод", "База данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //Управление перейдет этому блоку, если положительный результат у запроса
                        id = mtb_StudNumb.Text;//Запоминаем идентификатор студента
                        string[] buf = answ.Split('_');//Одна из ключевых механик работы этой системы клиент-серверного приложения
                        MessageBox.Show("Добро пожаловать " + buf[1] + " " + buf[3], "База данных", MessageBoxButtons.OK, MessageBoxIcon.Information);//Приветственное сообщение
                        MainScreen screen = new MainScreen(buf[2], buf[1], buf[3], id, Port);//Создание объекта следующего окна-формы
                        screen.Disposed += new EventHandler(IfClosed);
                        /*
                         При закрытии формы, происходит "распыление", удаление объекта, таким образом, отслеживая закрытие\"распыление" нашей второй формы, мы можем контролировать текущую форму (this).
                         Узнаем мы о закрытии формы при помощи события Disposed
                        */
                        screen.Show();//Метод который отображает (отрисовывает) нашу форму
                        this.Hide();//Метод при помощи которого мы прячем текущее окно (this)
                    }
                    //Закрываем потоки. Вся требуемуя информация на данный момент считана
                    stream.Close();
                    client.Close();
                    but_Login.Enabled = true;//Активируем клавишу. Разрешаем считывать события клика по кнопке
                }
                else
                {
                    MessageBox.Show("Заполните пожалуйста поле!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);//Сообщение выводящееся при некорректном заполнении поля с номером студенческого
                    but_Login.Enabled = true;//Активируем клавишу. Разрешаем считывать события клика по кнопке
                }
            }
            catch(SocketException ex)//Исключение, для обработки исключений сокета (TcpClient, NetworkStream и т.п.)
            {
                MessageBox.Show("Ошибка подключения. Сервер не отвечает.\n" + ex.Message, "Подключение", MessageBoxButtons.OK, MessageBoxIcon.Error);//[TEST] +"\n"+ex.StackTrace
                but_Login.Enabled = true;//Активируем клавишу. Разрешаем считывать события клика по кнопке
            }
        }

        private void IfClosed(object sender, EventArgs e)//Событие при распылении (закрытии) второй созданной формы Mainscreen
        {
            this.Close();//Метод закрытия текущей формы (this)
        }
    }
}
