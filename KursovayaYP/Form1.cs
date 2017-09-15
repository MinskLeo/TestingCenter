using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**/
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace KursovayaYP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        /*  try
                    {
                        SendMessageFromSocket(11000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    finally
                    {
                        Console.ReadLine();
                    }
                

                static void SendMessageFromSocket(int port)
                {
                    // Буфер для входящих данных
                    byte[] bytes = new byte[1024];

                    // Соединяемся с удаленным устройством

                    // Устанавливаем удаленную точку для сокета
                    IPHostEntry ipHost = Dns.GetHostEntry("localhost");
                    IPAddress ipAddr = ipHost.AddressList[0];
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

                    Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                    // Соединяем сокет с удаленной точкой
                    sender.Connect(ipEndPoint);

                    Console.Write("Введите номер студенческого билета : ");
                    string message = Console.ReadLine();

                    byte[] msg = Encoding.UTF8.GetBytes(message);

                    // Отправляем данные через сокет
                    int bytesSent = sender.Send(msg);

                    // Получаем ответ от сервера
                    int bytesRec = sender.Receive(bytes);

                    Console.WriteLine("\nОтвет от сервера: {0}\n\n", Encoding.UTF8.GetString(bytes, 0, bytesRec));

                    // Используем рекурсию для неоднократного вызова SendMessageFromSocket()
                    if (message.IndexOf(".End>") == -1)
                        SendMessageFromSocket(port);

                    // Освобождаем сокет
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();*/
             
                  

        private void but_Login_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainScreen screen = new MainScreen();
            screen.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
