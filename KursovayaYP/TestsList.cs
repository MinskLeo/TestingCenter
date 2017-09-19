using System;
using System.Net;
using System.Net.Sockets;
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
    public partial class TestsList : Form
    {
        private static TcpClient tcp=new TcpClient();
        private static int Port = 8888, ID;

        public TestsList(int port, int id)
        {
            InitializeComponent();
            //Тут 2 варианта развития событий:
            //1.Грузимся в блоке с конструктором - трабл с экзепшоном может быть, если не будет коннекта
            //2.Грузимся после загрузки данной формы - не совсем логично, пока попробуем первый вариант
            Port = port;
            ID = id;
            tcp.Connect("127.0.0.1",Port);
            NetworkStream stream = tcp.GetStream();
            string request = "testlist_"+ID;
            string answer;
            stream.Write(Encoding.UTF8.GetBytes(request),0,request.Length);
            byte[] dat=new byte[256];

            stream.Read(dat, 0, dat.Length);//Принимаем количество строчек (кол во тестов возможных)
            answer = Encoding.UTF8.GetString(dat);//Конвертируем в UTF8 строку
            for(int i=0;i<Convert.ToInt32(answer);i++)//Будем считывать с потока данные, и запихивать в список
            {
                stream.Read(dat, 0, dat.Length);
                answer = Encoding.UTF8.GetString(dat);
                list_Tests.Items.Add(answer);
            }
            stream.Close();//Закрываем поток
            //p.s.Это поидее должно работать, когда допишем сервер, но я не уверен--------------------------------------------
        }
    }
}
