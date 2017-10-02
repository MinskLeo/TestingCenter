using System;
using System.Net.Sockets;
using System.Net;
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
    public partial class TestResults : Form
    {
        private static string ID;
        private static int Port=8888;
        //-------------
        private static int All = 0;//Общее количество вопросов. Чтобы получить место в массиве ОТНИМАЕМ 1!!!
        private static int Correct=0;//Правильных
        private static int Incorrect = 0;//Неправильных
        private static int Mark=0;
        //
        private static bool synchro=false;

        public TestResults(string id, int all, int port)
        {
            InitializeComponent();
            ID = id;
            All = all;
            Port = port;
            //Подсчет результата
            for (int i = 0; i < All; i++)
            {
                //MessageBox.Show(Test.user_answers[i].answer);   //DEBUG
                if (Test.questions[i].rightAnswers.Equals(Test.user_answers[i].answer))
                {
                    Correct++;
                }
            }

            Incorrect = All - Correct;
            lb_Correct.Text = "Правильно: " +Correct+"/"+All;
            lb_Incorrect.Text = "Неправильно: " + Incorrect + "/" + All;
            Mark = ((100 * Correct) / All)/10;
            lb_Mark.Text = Mark.ToString();
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
                UploadingInformation();
            }
        }

        private void but_Exit_Click(object sender, EventArgs e)
        {
            but_Exit.Enabled = false;
            UploadingInformation();
            synchro = true;
            this.Close();
        }

        private void UploadingInformation()
        {
            try
            {
                TcpClient tcp = new TcpClient();
                tcp.Connect("127.0.0.1", Port);
                NetworkStream stream = tcp.GetStream();
                //testresult_ID_Subject_Mark_Time
                string[] buf = TestsList.TestName.Split('_');
                byte[] message = Encoding.UTF8.GetBytes("testresult_" + ID+"_"+buf[2]+"_"+Mark+"_"+Test.StartTime.ToShortTimeString());//fsafsafas
                stream.Write(message, 0, message.Length);
                stream.Close();
                tcp.Close();
            }
            catch(SocketException ex)
            {
                MessageBox.Show("Ошибка подключения. Сервер не отвечает.\n" + ex.Message + "\n" + ex.StackTrace, "Подключение", MessageBoxButtons.OK, MessageBoxIcon.Error);//DEBUG
                but_Exit.Enabled = true;
            }
            finally
            {
                but_Exit.Enabled = true;
            }
        }
    }
}
