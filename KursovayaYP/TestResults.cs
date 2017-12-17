using System;
using System.Net.Sockets;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KursovayaYP
{
    public partial class TestResults : Form
    {
        private static string ID;
        private static int Port=8888;
        //-------------
        private static int All = 0;//Общее количество вопросов. Чтобы получить место в массиве ОТНИМАЕМ 1
        private static int Correct=0;//Правильных
        private static int Incorrect = 0;//Неправильных
        private static int Mark=0;//Оценка за тест
        //
        private static bool synchro=false;

        public TestResults(string id, int all, int port)
        {
            InitializeComponent();
            ID = id;
            All = all;
            Port = port;
            //Подсчет результатов через сравнение пользовательского ответа с верным
            for (int i = 0; i < All; i++)
            {
                if (Test.questions[i].rightAnswers.Equals(Test.user_answers[i].answer))
                {
                    Correct++;//Инкрементация переменной, считающей правильные ответы
                }
            }

            Incorrect = All - Correct;//Подсчет неправильно отвеченных вопросов


            lb_Correct.Text = "Правильно: " +Correct+"/"+All;//Записываем значения переменных полученных выше в информационные элементы
            lb_Incorrect.Text = "Неправильно: " + Incorrect + "/" + All;
            Mark = ((100 * Correct) / All)/10;//Рассчет оценки
            lb_Mark.Text = Mark.ToString();//Вывод оценки пользователю
            //Далее следуют три блока, отвечающих за более красивое отображение результата в зависимости от полученной оценки
            if(Mark>4)
            {
                lb_Mark.ForeColor = Color.Green;
            }
            else if(Mark==4)
            {
                lb_Mark.ForeColor = Color.Yellow;
            }
            else
            {
                lb_Mark.ForeColor = Color.Red;
            }
        }

        private void TestResults_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (synchro == false)
            {
                UploadingInformation();//Метод, вызываемый при закрытии формы, отправляющий результаты на сервер
            }
        }

        private void but_Exit_Click(object sender, EventArgs e)
        {
            but_Exit.Enabled = false;//Не разрешаем польователю нажать на кнопку несколько раз, чтобы исключить множественную отправку результатов на сервер
            UploadingInformation();//Вызываем метод на отправку результатов на сервер
            synchro = true;
            this.Close();
        }

        private void UploadingInformation()//Метод, вызываемый при закрытии формы, отправляющий результаты на сервер
        {
            try
            {
                TcpClient tcp = new TcpClient();
                tcp.Connect("127.0.0.1", Port);
                NetworkStream stream = tcp.GetStream();
                //testresult_ID_Subject_Mark_Time
                string[] buf = TestsList.TestName.Split('_');
                byte[] message = Encoding.UTF8.GetBytes("testresult_" + ID+"_"+buf[2]+"_"+Mark+"_"+Test.StartTime.ToShortTimeString());
                stream.Write(message, 0, message.Length);
                //Закрытие потоков
                stream.Close();
                tcp.Close();
            }
            catch(SocketException ex)
            {
                MessageBox.Show("Ошибка подключения. Сервер не отвечает.", "Подключение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                but_Exit.Enabled = true;
            }
            finally
            {
                but_Exit.Enabled = true;
            }
        }
    }
}
