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
        private static string[] TEST;
        private static int Port=8888;
        //Поля самого теста
        private int QuestionsCount;//Количество вопросов
        public static Question[] questions;//Массив с вопросами
        public static Answer[] user_answers;//Ответы пользователя
        private static int CURRENT=1;//Текущий вопрос, чтобы получить место в массиве ОТНЯТЬ 1
        private static bool synchro = false;//т.к. при нажатии креста в углу возникает событие FormClosing, и т.к. оно же возникает при окончании теста, была созданна данная переменная, для верной синхронизации появления событий
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
                but_Previous.Text = "";
                but_Previous.Size = new Size(68,34);
                but_Previous.Location=new Point(340, 200);
                but_Previous.Image = new Bitmap("images\\prev.png");
                but_Next.Text = "";
                but_Next.Size = new Size(68, 34);
                but_Next.Image = new Bitmap("images\\next.png");
            }
        }

        private void Test_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//Замена курсора на Курсор ожидания
            this.Text = TestsList.TestName+" - "+MainScreen.Surname+" "+MainScreen.FirstName + " " + MainScreen.MiddleName;//Менем заголовок окна на Имя Фамилия Отчество пользователя
            string[] buf = TEST[0].Split(';',':');//Режем первую строчку 
            //2;1:0:0              вопросов ; часы : минуты : секунды
            hours = Convert.ToInt32(buf[1]);//Конвертируем строковое представление часов в целочисленное значение
            minutes = Convert.ToInt32(buf[2]);//Конвертируем строковое представление часов в целочисленное значение
            seconds = Convert.ToInt32(buf[3]);//Конвертируем строковое представление секунд в целочисленное значение

            QuestionsCount = Convert.ToInt32(buf[0]);//Кол во вопросов
            questions = new Question[QuestionsCount];//Определяем размер массивов с вопросами и ответами пользователя
            user_answers = new Answer[QuestionsCount];
            for(int i=0;i<QuestionsCount;i++)//Цикл на добавление в элементы управления контейнера flowLayout новых кнопок, отвечающих за переключение текущего вопроса
            {
                flow_Questions.Controls.Add(new Button() { Text=(i+1).ToString(),AutoSize=true,AutoSizeMode=AutoSizeMode.GrowAndShrink});
                flow_Questions.Controls[i].Click += new EventHandler(FlowClick);//Регистрируем нажатие на кнопку
            }

            //Тут создается заполняется массив вопросами и т.п.
            int k = 1;//Используется дл постепенного перемещения по строкам, чтобы корректно считать вопрос и принадлежащие ему количество вариантов ответов, сами ответы, правильные ответы
            for(int i=1;i<=QuestionsCount;i++)//Т.к. первая строка в массиве строк содержит информацию о тесте, отсчет с 1
            {
                questions[i - 1] = new Question();//Создаем объект типа Вопрос (Question)
                user_answers[i - 1] = new Answer();//Создаем объект типа Ответ (Answer)
                questions[i - 1].question = TEST[k];//Присваиваем элементу массива вопросов значение элемента массива с тестом целиком
                k++;//Переходим на строку с кол вом ответов
                questions[i - 1].ansCount = Convert.ToInt32(TEST[k]);//Считываем количество вариантов ответов
                k++;//Переходим на строку с самими ответами
                for(int j=0;j<questions[i-1].ansCount;j++)//Считываем сами ответы через нижеприведенный вложенный цикл
                {
                    questions[i - 1].answers.Add(TEST[k]);
                    k++;
                }
                questions[i - 1].rightAnswers = TEST[k];//Считываем строку с правильными ответами
                k++;//Переходим на строку с следующим вопросом
            }
            LoadQuestion(0);//Вызываем метод который осуществлет "загрузку" вопроса на форму, где 0 - элемент который требуется загрузить от
            lb_Current.Text = CURRENT.ToString() + "/" + QuestionsCount.ToString();//Выводим пользователю номер текущего вопроса к номеру общего количества вопросов 
            this.Cursor = Cursors.Default;//Возвращаем обычный курсор
            timer_TickTock.Enabled = true;//Включаем таймер, отвечающий за отсчет и контроль времени выделяемого на прохождение теста
        }

        private void LoadQuestion(int n)//Метод предназначенный для загрузки вопроса по параметру, передаваемому при вызове
        {
            lb_Question.Text = questions[n].question;//Элемент управления Label, который используется для вывода текстовых данных пользователю
            if(questions[n].ansCount>=1)//Если количество ответов больше 1
            {
                cb_1.Text = questions[n].answers[0];//Заполняем параметр, отвечающий за отображение текста у элемента типа CheckBox нашим 1м вариантом ответа
            }
            if(questions[n].ansCount>=2)//Если количество ответов больше либо равно 2
            {
                cb_2.Text = questions[n].answers[1];//Заполняем параметр, отвечающий за отображение текста у элемента типа CheckBox нашим 2м вариантом ответа
            }
            if(questions[n].ansCount>=3)//Если количество ответов больше либо равно 3
            {
                cb_3.Text = questions[n].answers[2];//Заполняем параметр, отвечающий за отображение текста у элемента типа CheckBox нашим 3м вариантом ответа
            }
            if(questions[n].ansCount>=4)//Если количество ответов больше либо равно 4
            {
                cb_4.Text = questions[n].answers[3];//Заполняем параметр, отвечающий за отображение текста у элемента типа CheckBox нашим 4м вариантом ответа
            }
            if(questions[n].ansCount>=5)//Если количество ответов больше либо равно 5
            {
                cb_5.Text = questions[n].answers[4];//Заполняем параметр, отвечающий за отображение текста у элемента типа CheckBox нашим 5м вариантом ответа
            }
            if(questions[n].ansCount==6)//Если количество ответов равно 6
            {
                cb_6.Text = questions[n].answers[5];//Заполняем параметр, отвечающий за отображение текста у элемента типа CheckBox нашим 6м вариантом ответа
            }
            switch(questions[n].ansCount)//Скрываем или отображаем необходимые элементы типа CheckBox, в зависимости от количества вариантов ответа
            {
                case 1:
                    cb_1.Visible = true;//False - Элемент пользователю не виден, True - элемент отображается визуально
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
            //user_answers - массив, содержащий выбранные ответы пользователя
            //В данном моменте происходит проверка, содержит ли пользовательские данные по выбранному вопросу для загрузки
            //(Отвечал ли пользователь на вопрос ранее)
            if (user_answers[n].answered)
            {
                string[] buf = user_answers[n].answer.Split(',');//Разрезаем строку содержащую ответ пользователя по ,
                for(int i=0;i<buf.Length;i++)//Проходимся по каждому элементу разрезанной строки
                {
                    switch(Convert.ToInt32(buf[i]))
                    {
                        case 1:
                            cb_1.Checked = true;//Если элемент содержит 1, изменем свойство Checked объекта cb_1 (Типа CheckBox) на Отвечено
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


        private void FlowClick(object sender, EventArgs e)//Обработка нажатия по кнопкам в нижней панели
        {
            foreach(var a in gb_Answers.Controls)//Снимаем "отвеченность" с каждого варианта ответа, чтобы загрузить свежие, корректные данные
            {
                (a as CheckBox).Checked = false;
            }
            int k = Convert.ToInt32((sender as Button).Text);//Получаем наш номер элемента, который мы должны загрузить на форму
            CURRENT = k;//Обновляем наше поле CURRENT, являющееся текущим вопросом на форме на новое значение k
            lb_Current.Text = CURRENT.ToString()+"/"+QuestionsCount.ToString();//Изменяем информационный элемент lb_Current, отображающий текущий отвечаемый вопрос
            LoadQuestion(k-1);//Вызываем метод на загрузку данных
        }

        private void but_Save_Click(object sender, EventArgs e)//Кнопка сохранения ответа
        {
            bool exit = false;//Переменная, предназначенная для проверки, если пользователь не выделил не один вариант ответа
            foreach(var a in gb_Answers.Controls)
            {
                if((a as CheckBox).Checked)
                {
                    exit = true;
                }
            }
            if (exit == false)
            {
                //Диалоговое окно, информирующее пользователя о том, что требуется выбрать хотя бы один вариант ответа
                MessageBox.Show("Выберите хотя бы один вариант ответа", "Тест",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;//Выход из метода
            }
            //CURRENT-1
            user_answers[CURRENT - 1].answered = true;//Изменяем значение переменной, являющееся показателем статуса пользовательского ответа
            int MoreThanOne = 0;//Переменная, считающая количество отмеченных "галочек"
            foreach(var a in gb_Answers.Controls)
            {
                if((a as CheckBox).Checked)
                {
                    MoreThanOne++;//Подсчитаем количество отмеченных элементов CheckBox
                }
            }
            int LastOne = 0;//Переменная, предназначенная для ограничения и недопущения выхода за границы массива
            //Пользовательский ответ записывается в одну строку, чтобы сравниваться напрямую со строкой, содержащей правильный ответ
            foreach(var a in gb_Answers.Controls)//Постепенный проход по коллекции элементов, которые хранит элемент-контейнер GroupBox
            {
                if((a as CheckBox).Checked)//Если элемент отмечен галочкой (отмечен пользователем)
                {
                    switch((a as CheckBox).Name)//Индентификатором в данном случае является название элемента (необходимо понять, какой именно элемент отмечен)
                    {
                        case "cb_1":
                            user_answers[CURRENT - 1].answer += "1";//Используем оператор присоединения новых значений к строке
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
                    LastOne++;//Инкрементируем переменную, ограничивающую выход за границы массива
                    if(LastOne!=MoreThanOne)//Добавляем запятые в строку
                    {
                        user_answers[CURRENT - 1].answer += ",";
                    }
                }
            }
            flow_Questions.Controls[CURRENT - 1].BackColor = Color.Green;//Цвет фона кнопки для отвеченного хотя бы раз вопроса сменится на зеленый

            but_Next_Click(sender, e);//Вызовем принудительно нажатие по кнопке Следующий вопрос
        }

        private void but_End_Click(object sender, EventArgs e)//Кнопка, отвечающая за окончание теста и вывод результатов пользователю
        {
            //Диалоговое окно, запрашивающее у пользовател подтверждение, на случай, если нажатие на Завершение теста случайно
            if (MessageBox.Show("Вы уверены?", "Завершение теста", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                TestResults results = new TestResults(ID,QuestionsCount,Port);//Созаем объект формы, отвечающей за отображение и подсчет результатов теста
                results.Disposed += new EventHandler(IfClosed);//Регистрируем событие закрытия окна результатов
                this.Hide();//Скрываем текущее окно (this)
                synchro = true;//Переменная, ограничивающая выполнение блока метода, срабатывающего при зкрытии окон
                results.Show();//Отображаем окно с результатами
            }
        }

        private void IfClosed(object sender,EventArgs e)//Метод, срабатывающий при распылении/закрытии созданного окна
        {
            this.Close();
        }

        private void but_Previous_Click(object sender, EventArgs e)//Метод, обрабатывающий нажатие по кнопке Возврат к предыдущему вопрос
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
            foreach (var a in gb_Answers.Controls)//Снимаем "отвеченность" с каждого варианта ответа, чтобы загрузить свежие, корректные данные
            {
                (a as CheckBox).Checked = false;
            }

            if (CURRENT - 1 < QuestionsCount - 1)
            {
                LoadQuestion(CURRENT);//Вызываем метод на загрузку данных предыдущего вопроса и обновляем данные на информационном элементе
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

        private void Test_FormClosing(object sender, FormClosingEventArgs e)//Метод срабатывающий при закрытии формы (в основном при закрытии при помощи креста в углу)
        {
            if (synchro == false)//Контролируем закрытие окна формы
            {
                if (MessageBox.Show("Вы уверены?", "Завершение теста", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                {
                    e.Cancel = true;//Отменяем закрытие, мы сами будем в коде управлять закрытием форм
                    return;
                }
                else
                {
                    TestResults results = new TestResults(ID, QuestionsCount, Port);//Соаздем объект формы выводящей результаты пользователю
                    results.Disposed += new EventHandler(IfClosed);//Регистрируемся на 
                    this.Hide();
                    e.Cancel = true;//Отменяем закрытие, мы сами будем в коде управлять закрытием форм
                    synchro = true;
                    results.Show();//Отображаем форму с результатами
                }
            }
        }

        private void timer_TickTock_Tick(object sender, EventArgs e)//Таймер, отслеживающий время выполнения теста
        {
            string time = hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();//Отображем время пользователю

            lb_Time.Text = time;//Меняем параметр Text у информационного элемента, отвечающего за отображение времени пользователю

            if (minutes == 10 && seconds == 0)//Предупреждение насчет оставшегося времени (менее 10 минут)
            {
                timer_TickTock.Stop();
                if (MessageBox.Show("У вас осталось менее 10 минут","Время",MessageBoxButtons.OK,MessageBoxIcon.Information) == DialogResult.OK)
                timer_TickTock.Start();
            }

            if (hours == 0 && minutes <= 10)//Меняем цвет текста информационного элемента отображающего временя на красный
            {
                lb_Time.ForeColor = Color.Red;
            }

              if (hours == 0 && minutes == 0 && seconds == 0)//Принудительно завершаем тест, при  окончании времени отведенного на тест
              {
                timer_TickTock.Enabled = false;//Останавливаем таймер
                MessageBox.Show("Время вышло", "Тест", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TestResults results = new TestResults(ID, QuestionsCount, Port);//Создаем форму отображающую результаты теста
                results.Disposed += new EventHandler(IfClosed);//Регистрируемся на событие распыления
                this.Hide();//Скрываем эту форму
                synchro = true;
                results.Show();//Вызываем отображение формы с результатами
              }

            if (minutes == 0)//Управляем переменными времени
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

    public class Question//Объект содержащий данные вопроса
    {
        public string question;//1 строка   Сам вопрос
        public int ansCount;//2 строка  Количество вариантов ответов
        public List<string> answers = new List<string>();//Список содержащий ответы
        public string rightAnswers;//Строка, содержащая правильный ответ на вопрос
        public Question()
        {

        }
    }
    public class Answer//Объект содержащий данные ползовательского ответа
    {
        public bool answered = false;//Состояние пользовательского ответа
        public string answer;//Значение(я) пользовательского ответа
        public Answer()
        {

        }
    }
}
