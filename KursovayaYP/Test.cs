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
        private static string ID;
        private static string[] TEST;
        //Поля самого теста
        private int QuestionsCount;
        private static Question[] questions;

        public Test(string id,string[] test)
        {
            InitializeComponent();
            ID = id;
            TEST = test;
        }

        private void Test_Load(object sender, EventArgs e)
        {
            QuestionsCount = Convert.ToInt32(TEST[0]);//Кол во вопросов
            questions = new Question[QuestionsCount];//Определяем размер массива
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
        }

        private void FlowClick(object sender, EventArgs e)//Клик по кнопкам в нижней панели с вопросами
        {
            //MessageBox.Show("Text: "+(sender as Button).Text); //DEBUG
            int k = Convert.ToInt32((sender as Button).Text);
            LoadQuestion(k-1);
            //lb_Question.Text = questions[k-1].question;
            //gb_Answers.Controls.Clear();
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
