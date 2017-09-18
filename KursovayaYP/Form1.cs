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
/// 3.Уже прогресс)
/// 4.Такс, уже лучше, есть косяк со вторичным нажатием на кнопку вход
/// </summary>

//Команды к\от серверу:
//1.login_ это логин скрин

//ВНИМАНИЕ!!!!!!!!!!!
//КЛИЕНТ, при ответе сервера и закрытии прилоежния ОСТАЕТСЯ ВИСЕТЬ В ОПЕРАТИВЕ!!!!!!!!!
namespace KursovayaYP
{
    public partial class Form1 : Form
    {
        TcpClient client = new TcpClient();
        private static int Port=8888;
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
                //Можно добавить логированние в файлик
                Port = 8888;
                return;
            }
            finally
            {
                client.Connect("127.0.0.1", Port);
            }
        }

        private void but_Login_Click(object sender, EventArgs e)
        {
            if (mtb_StudNumb.Text.Length == 14)
            {
                NetworkStream stream = client.GetStream();
                byte[] message = Encoding.UTF8.GetBytes("login_"+mtb_StudNumb.Text);
                stream.Write(message, 0, message.Length);//Специальная комбинация "оператор_данные": например, login_20169876654237
                //DebuG & shit code
                byte[] data = new byte[256];
                stream.Read(data, 0, data.Length);
                string answ = Encoding.UTF8.GetString(data,0,data.Length);

                //ПРОВЕРИТЬ
                MessageBox.Show("Ans: "+answ);
                //NNN - Нету совпадений, [Имя]_[Отчество] - вход
                if(answ.CompareTo("login_NNN")==0)
                {
                    MessageBox.Show("Студент не найден, повторите ввод", "База данных",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    string[] buf = answ.Split('_');
                    MessageBox.Show("Добро пожаловать "+buf[0]+" "+buf[1],"База данных",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    MainScreen screen = new MainScreen();//Могут быть траблы)
                    screen.Show();
                    this.Hide();
                }
                //Офаем прием
                stream.Close();
            //
            }
            else
            {
                MessageBox.Show("Заполните пожалуйста поле!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Close();
        }
    }
}
