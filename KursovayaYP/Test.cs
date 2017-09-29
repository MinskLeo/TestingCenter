using System;
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
        //Поля самого теста
        private int QuestionsCount;//Количество вопросов
        private static Question[] questions;//Массив с вопросами
        private static Answer[] user_answers;//Ответы пользователя
        private static int CURRENT;//Текущий вопрос, чтобы получить место в массиве ОТНЯТЬ 1!!!!

        public Test(string id,string[] test)
        {
            InitializeComponent();
            ID = id;
            TEST = test;
        }

        private void Test_Load(object sender, EventArgs e)
        {
            QuestionsCount = Convert.ToInt32(TEST[0]);//Кол во вопросов
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
        }
    }

    class Question
    {
        public string question;//1 строка
        public int ansCount;//2 строка
        public List<string> answers = new List<string>();
        public string rightAnswers;
        public Question()
        {

        }
    }
    class Answer
    {
        public bool answered = false;
        public string answer;
        public Answer()
        {

        }
    }
}
