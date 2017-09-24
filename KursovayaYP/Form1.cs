using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**/
using System.Threading;//Еще не юзали потоки
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

/// <summary>
/// ChangeLog:
/// 1.Отправка данных на сервер работает
/// 2.Клиент виснет, до тех пор пока не придет ответ. Придется клепать отдельный поток
/// 3.Пофикшены многократные нажатия на кнопку. Все равно придется стопать мейновый поток, елси сервер не успевает осблужить. Нам не нужно многократное получение
/// ответа. Так можно и хакнуть софт, через какие нибудь автокликеры. тупо забрутфорсить
/// </summary>

//Команды к\от серверу:
//1.login_Id это логин скрин
//2.testslist_Id получение списка тестов
//ВНИМАНИЕ!!!!!!!!!!!
//КЛИЕНТ, при ответе сервера и закрытии прилоежния ОСТАЕТСЯ ВИСЕТЬ В ОПЕРАТИВЕ!!!!!!!!!
namespace KursovayaYP
{
    public partial class Form1 : Form
    {
        //TcpClient client = new TcpClient();//Попробуемс перенсти в метод, дабы небыло косяков с коннектом
        private static int Port=8888;
        public static string id;
        private static Button EXIT = new Button();

        public Form1()
        {
            InitializeComponent();
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
            EXIT.Click += new EventHandler(Exit_button);//Регистрация события на тырк по Esc
            this.CancelButton = EXIT;//Меняем параметр выход-кнопки
        }

        private void Exit_button(object sender, EventArgs e)
        {
            this.Close();//Невидимая кнопка закрытия) Активируется тырком по Esc
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Мб не будем сразу пытаться законнектиться к серверу? Пожалуй так и сделаю. Пока в комментарии, но потомы выкинем //DEBUG

            /*try
            {
                client.Connect("127.0.0.1", Port);
            }
            catch(SocketException ex)
            {
                MessageBox.Show("Troubles with connecting. Server not responding.\n"+ex.Message, "Connecting", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void but_Login_Click(object sender, EventArgs e)
        {
            try
            {
                but_Login.Enabled = false;
                TcpClient client = new TcpClient();
                if (client.Connected == false)
                {
                    client.Connect("127.0.0.1", Port);
                }

                if (mtb_StudNumb.Text.Length == 14)
                {
                    NetworkStream stream = client.GetStream();
                    //попробуемс принять через нормальные потоки        //DEBUG--------------------------------------------НУЖНО ИСПОЛЬЗОВАТЬ ОТДЕЛЬНЫЙ ПОТОК! ПОИСК МОЖЕТ ЗАТЯНУТЬСЯ!
                     //StreamWriter NetWriter = new StreamWriter(stream);//Создали поток для записи
                     //NetWriter.WriteLine("login_" + mtb_StudNumb.Text);
                     //NetWriter.Flush();
                     //StreamReader NetReader = new StreamReader(stream);
                     //string answ=NetReader.ReadLine();
                     //NetReader.Close();
                     //NetWriter.Close();
                    //Ниже до слов ПРОВЕРИТЬ закомментил, для теста //DEBUG //P.S. ВРОДЕ ПОКА ВОРКАЕТ, Но комменты не убираем - "вдруг все грохнется" (С) Демидович
                     byte[] message = Encoding.UTF8.GetBytes("login_" + mtb_StudNumb.Text);
                     stream.Write(message, 0, message.Length);//Специальная комбинация "оператор_данные": например, login_20169876654237
                                                             //DebuG & shit code
                     byte[] data = new byte[256];
                     stream.Read(data, 0, data.Length);
                     string answ = Encoding.UTF8.GetString(data, 0, data.Length);

                    //ПРОВЕРИТЬ
                    //MessageBox.Show("Ans: " + answ);          //DEBUG
                    //NNN - Нету совпадений, [Имя]_[Отчество] - вход
                    if (answ.Equals("login_NNN"))//answ.CompareTo("login_NNN") == 1
                    {
                        MessageBox.Show("Студент не найден, повторите ввод", "База данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //login_Фамилия_Имя_Отчество
                        id = mtb_StudNumb.Text;//Запоминаем идентификатор студента
                        string[] buf = answ.Split('_');
                        MessageBox.Show("Добро пожаловать " + buf[1] + " " + buf[3], "База данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MainScreen screen = new MainScreen(buf[1], buf[2], buf[3], id, Port)
                        {
                            Owner = this//НЕ ВОРКАЕТ "владелецевание"
                        };//Могут быть траблы)
                        screen.Show();
                        this.Hide();
                    }
                    //Офаем прием и клиент-объект
                    stream.Close();
                    client.Close();
                    but_Login.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Заполните пожалуйста поле!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    but_Login.Enabled = true;
                }
            }
            catch(SocketException ex)
            {
                MessageBox.Show("Ошибка подключения. Сервер не отвечает.\n" + ex.Message+"\n"+ex.StackTrace, "Подключение", MessageBoxButtons.OK, MessageBoxIcon.Error);//DEBUG
                but_Login.Enabled = true;
            }
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //client.Close();         //Пока не нид, у нас же открывается закрывается клиент в потоке
        }
    }
}
