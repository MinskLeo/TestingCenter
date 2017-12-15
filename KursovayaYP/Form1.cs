using System;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

namespace KursovayaYP
{
    public partial class Form1 : Form
    {
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
                     byte[] message = Encoding.UTF8.GetBytes("login_" + mtb_StudNumb.Text);
                     stream.Write(message, 0, message.Length);//Специальная комбинация "оператор_данные": например, login_20169876654237

                     byte[] data = new byte[256];
                     stream.Read(data, 0, data.Length);
                     string answ = Encoding.UTF8.GetString(data, 0, data.Length);

                    //NNN - Нету совпадений, [Имя]_[Отчество] - вход
                    if (answ.CompareTo("login_NNN")==0)//[TEST] answ.ToString().Equals("login_NNN")
                    {
                        MessageBox.Show("Студент не найден, повторите ввод", "База данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //login_Фамилия_Имя_Отчество
                        id = mtb_StudNumb.Text;//Запоминаем идентификатор студента
                        string[] buf = answ.Split('_');
                        MessageBox.Show("Добро пожаловать " + buf[1] + " " + buf[3], "База данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MainScreen screen = new MainScreen(buf[2], buf[1], buf[3], id, Port);
                        screen.Disposed += new EventHandler(IfClosed);//----------Наше событие на закрытие формы
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

        private void IfClosed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
