using System;
using System.IO;
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
    public partial class Test : Form
    {
        private static string ID;//Наш ИД, по нему потом будет отправлять результаты
        private static string[] TEST;//нужно что то сделать с этим массивом, он останется ненужный висеть в памяти
        private static int Port=8888;
        //Поля самого теста
        private int QuestionsCount;//Количество вопросов
        public static Question[] questions;//Массив с вопросами
        public static Answer[] user_answers;//Ответы пользователя
        private static int CURRENT=1;//Текущий вопрос, чтобы получить место в массиве ОТНЯТЬ 1!!!!
        public static DateTime StartTime = DateTime.Now;//Время начала
        private static bool synchro = false;
        private int hours = 0;
        private int minutes = 0;
        private int seconds = 0;

        public Test(string id,string[] test, int port)
        {
            InitializeComponent();
            ID = id;
            TEST = test;
            Port = port;
            if(File.Exists("images\\next.png") && File.Exists("images\\prev.png"))//Делаем иконки на кнопках, если их в папке нету - не будет проблем
            {
                //340; 200
                but_Previous.Text = "";
                but_Previous.Size = new Size(68,34);
                but_Previous.Location=new Point(340, 200);//Чуть чуть сдвигаем кнопочку
                but_Previous.Image = new Bitmap("images\\prev.png");
                but_Next.Text = "";
                but_Next.Size = new Size(68, 34);
                but_Next.Image = new Bitmap("images\\next.png");
            }
        }

        private void Test_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Text = TestsList.TestName+" - "+MainScreen.Surname+" "+MainScreen.FirstName + " " + MainScreen.MiddleName;//Чет не хочет больше символов выводить... Видать ограничение
            string[] buf = TEST[0].Split(';',':');
            //2;1:0:0              вопросов;часы:минуты:секунды
            hours = Convert.ToInt32(buf[1]);
            minutes = Convert.ToInt32(buf[2]);
            seconds = Convert.ToInt32(buf[3]);

            QuestionsCount = Convert.ToInt32(buf[0]);//Кол во вопросов
            questions = new Question[QuestionsCount];//Определяем размер массивов
            user_answers = new Answer[QuestionsCount];
            for(int i=0;i<QuestionsCount;i++)
            {
                flow_Questions.Controls.Add(new Button() { Text=(i+1).ToString(),AutoSize=true,AutoSizeMode=AutoSizeMode.GrowAndShrink});//DEBUG
                flow_Questions.Controls[i].Click += new EventHandler(FlowClick);
            }

            //Тут мы мутим наш массив с вопросами-----------P.S.Тут будет грязно)
            int k = 1;//Наша прыгалка по строчкам
            for(int i=1;i<=QuestionsCount;i++)
            {
                questions[i - 1] = new Question();
                user_answers[i - 1] = new Answer();
                questions[i - 1].question = TEST[k];
                k++;//Переходим на строку с кол вом ответов
                questions[i - 1].ansCount = Convert.ToInt32(TEST[k]);
                k++;//Переходим на строку с самими ответами
                for(int j=0;j<questions[i-1].ansCount;j++)
                {
                    questions[i - 1].answers.Add(TEST[k]);
                    k++;
                }
                questions[i - 1].rightAnswers = TEST[k];
                k++;//Переходим на строку с следующим вопросом
            }
            LoadQuestion(0);
            lb_Current.Text = CURRENT.ToString() + "/" + QuestionsCount.ToString();
            this.Cursor = Cursors.Default;
            timer_TickTock.Enabled = true;
        }

        private void LoadQuestion(int n)
        {
            lb_Question.Text = questions[n].question;
            if(questions[n].ansCount>=1)
            {
                cb_1.Text = questions[n].answers[0];
            }
            if(questions[n].ansCount>=2)
            {
                cb_2.Text = questions[n].answers[1];
            }
            if(questions[n].ansCount>=3)
            {
                cb_3.Text = questions[n].answers[2];
            }
            if(questions[n].ansCount>=4)
            {
                cb_4.Text = questions[n].answers[3];
            }
            if(questions[n].ansCount>=5)
            {
                cb_5.Text = questions[n].answers[4];
            }
            if(questions[n].ansCount==6)
            {
                cb_6.Text = questions[n].answers[5];
            }
            switch(questions[n].ansCount)
            {
                case 1:
                    cb_1.Visible = true;
                    cb_2.Visible = false;
                    cb_3.Visible = false;
                    cb_4.Visible = false;
                    cb_5.Visible = false;
                    cb_6.Visible = false;
                    break;
                case 2:
                    cb_1.Visible = true;
                    cb_2.Visible = true;
                    cb_3.Visible = false;
                    cb_4.Visible = false;
                    cb_5.Visible = false;
                    cb_6.Visible = false;
                    break;
                case 3:
                    cb_1.Visible = true;
                    cb_2.Visible = true;
                    cb_3.Visible = true;
                    cb_4.Visible = false;
                    cb_5.Visible = false;
                    cb_6.Visible = false;
                    break;
                case 4:
                    cb_1.Visible = true;
                    cb_2.Visible = true;
                    cb_3.Visible = true;
                    cb_4.Visible = true;
                    cb_5.Visible = false;
                    cb_6.Visible = false;
                    break;
                case 5:
                    cb_1.Visible = true;
                    cb_2.Visible = true;
                    cb_3.Visible = true;
                    cb_4.Visible = true;
                    cb_5.Visible = true;
                    cb_6.Visible = false;
                    break;
                case 6:
                    cb_1.Visible = true;
                    cb_2.Visible = true;
                    cb_3.Visible = true;
                    cb_4.Visible = true;
                    cb_5.Visible = true;
                    cb_6.Visible = true;
                    break;
            }
            if(user_answers[n].answered)
            {
                string[] buf = user_answers[n].answer.Split(',');
                for(int i=0;i<buf.Length;i++)
                {
                    switch(Convert.ToInt32(buf[i]))
                    {
                        case 1:
                            cb_1.Checked = true;
                            break;
                        case 2:
                            cb_2.Checked = true;
                            break;
                        case 3:
                            cb_3.Checked = true;
                            break;
                        case 4:
                            cb_4.Checked = true;
                            break;
                        case 5:
                            cb_5.Checked = true;
                            break;
                        case 6:
                            cb_6.Checked = true;
                            break;
                    }
                }
            }
        }


        private void FlowClick(object sender, EventArgs e)//Клик по кнопкам в нижней панели с вопросами
        {
            foreach(var a in gb_Answers.Controls)//Чистим чекбоксы для следующего вопроса
            {
                (a as CheckBox).Checked = false;
            }
            int k = Convert.ToInt32((sender as Button).Text);//Получаем наш номер элемента, который мы должны загрузить на форму
            CURRENT = k;//Обновляем наше CURRENT
            lb_Current.Text = CURRENT.ToString()+"/"+QuestionsCount.ToString();
            LoadQuestion(k-1);//Вызываем метод на загрузку данных
        }

        private void but_Save_Click(object sender, EventArgs e)//Емае, заработало с первого раза
        {
            bool exit = false;
            foreach(var a in gb_Answers.Controls)
            {
                if((a as CheckBox).Checked)
                {
                    exit = true;
                }
            }
            if (exit == false)
            {
                MessageBox.Show("Выберите хотя бы один вариант ответа", "Тест",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            //CURRENT-1
            user_answers[CURRENT - 1].answered = true;
            int MoreThanOne = 0;
            foreach(var a in gb_Answers.Controls)
            {
                if((a as CheckBox).Checked)
                {
                    MoreThanOne++;//Подсчитаем количество чекнутых боксов
                }
            }
            int LastOne = 0;
            foreach(var a in gb_Answers.Controls)
            {
                if((a as CheckBox).Checked)
                {
                    switch((a as CheckBox).Name)
                    {
                        case "cb_1":
                            user_answers[CURRENT - 1].answer += "1";
                            break;
                        case "cb_2":
                            user_answers[CURRENT - 1].answer += "2";
                            break;
                        case "cb_3":
                            user_answers[CURRENT - 1].answer += "3";
                            break;
                        case "cb_4":
                            user_answers[CURRENT - 1].answer += "4";
                            break;
                        case "cb_5":
                            user_answers[CURRENT - 1].answer += "5";
                            break;
                        case "cb_6":
                            user_answers[CURRENT - 1].answer += "6";
                            break;
                    }
                    LastOne++;
                    if(LastOne!=MoreThanOne)
                    {
                        user_answers[CURRENT - 1].answer += ",";
                    }
                }
            }
            flow_Questions.Controls[CURRENT - 1].BackColor = Color.Green;

            but_Next_Click(sender, e);
        }

        private void but_End_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены?", "Завершение теста", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                TestResults results = new TestResults(ID,QuestionsCount,Port);
                results.Disposed += new EventHandler(IfClosed);
                this.Hide();
                synchro = true;
                results.Show();
            }
        }

        private void IfClosed(object sender,EventArgs e)
        {
            this.Close();
        }

        private void but_Previous_Click(object sender, EventArgs e)//Чет с кнопками косяк какой то
        {
            foreach (var a in gb_Answers.Controls)//Чистим чекбоксы для следующего вопроса
            {
                (a as CheckBox).Checked = false;
            }

            if (CURRENT - 1 == 0)
            {
                CURRENT = QuestionsCount;
                LoadQuestion(CURRENT - 1);//Вызываем метод на загрузку данных 
                lb_Current.Text = CURRENT.ToString() + "/" + QuestionsCount.ToString();
            }
            else
            {
                CURRENT--;
                LoadQuestion(CURRENT - 1);//Вызываем метод на загрузку данных 
                lb_Current.Text = CURRENT.ToString() + "/" + QuestionsCount.ToString();
            }

        }

        private void but_Next_Click(object sender, EventArgs e)//Чет с кнопками косяк какой то
        {
            foreach (var a in gb_Answers.Controls)//Чистим чекбоксы для следующего вопроса
            {
                (a as CheckBox).Checked = false;
            }

            if (CURRENT - 1 < QuestionsCount - 1)
            {
                LoadQuestion(CURRENT);//Вызываем метод на загрузку данных 
                CURRENT++;
                lb_Current.Text = CURRENT.ToString() + "/" + QuestionsCount.ToString();
            }
            else
            {
                CURRENT = 1;
                LoadQuestion(CURRENT - 1);//Вызываем метод на загрузку данных 
                lb_Current.Text = CURRENT.ToString() + "/" + QuestionsCount.ToString();
            }
        }

        private void Test_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (synchro == false)
            {
                if (MessageBox.Show("Вы уверены?", "Завершение теста", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                {
                    e.Cancel = true;//отменяем закрытие, мы сами будем в коде управлять закрытием форм
                    return;
                }
                else
                {
                    TestResults results = new TestResults(ID, QuestionsCount, Port);
                    results.Disposed += new EventHandler(IfClosed);
                    this.Hide();
                    e.Cancel = true;//отменяем закрытие, мы сами будем в коде управлять закрытием форм
                    synchro = true;
                    results.Show();
                }
            }
        }

        private void timer_TickTock_Tick(object sender, EventArgs e)
        {
            string time = hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();

            lb_Time.Text = time;

            if (minutes == 10 && seconds == 0)
            {
                timer_TickTock.Stop();
                if (MessageBox.Show("У вас осталось менее 10 минут","Время",MessageBoxButtons.OK,MessageBoxIcon.Information) == DialogResult.OK)
                timer_TickTock.Start();
            }

            if (hours == 0 && minutes <= 10)
            {
                lb_Time.ForeColor = Color.Red;
            }

              if (hours == 0 && minutes == 0 && seconds == 0)
              {
                timer_TickTock.Enabled = false;
                MessageBox.Show("Время вышло", "Тест", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TestResults results = new TestResults(ID, QuestionsCount, Port);
                results.Disposed += new EventHandler(IfClosed);
                this.Hide();
                synchro = true;
                results.Show();
              }

            if (minutes == 0)
            {
                if (hours == 0)
                {
                    hours = 0;
                }
                else
                {
                    hours--;
                    minutes = 59;

                }
            }

            if (seconds == 0)
            {
                if (minutes == 0)
                {
                    minutes = 0;
                }
                else
                {
                    minutes--;
                    seconds = 59;
                }

            }

            seconds--;
        }

    }

    public class Question
    {
        public string question;//1 строка
        public int ansCount;//2 строка
        public List<string> answers = new List<string>();
        public string rightAnswers;
        public Question()
        {

        }
    }
    public class Answer
    {
        public bool answered = false;
        public string answer;
        public Answer()
        {

        }
    }
}
