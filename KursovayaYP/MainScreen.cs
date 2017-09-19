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
    public partial class MainScreen : Form
    {
        private static string FirstName, Surname, MiddleName;
        private static int Port, ID;

        private void but_NewTest_Click(object sender, EventArgs e)
        {
            TestsList testsList = new TestsList(Port, ID);
            this.Hide();
            testsList.Show();
        }

        public MainScreen(string surname, string name, string middleName, int id, int port)//Не получается открывать то окно (логин скрин), которое мейновое, если закрыть это
        {
            InitializeComponent();
            FirstName = name;
            Surname = surname;
            MiddleName = middleName;
            ID = id;
            Port = port;

            this.Text = Surname + " " + FirstName + " " + MiddleName;
        }

        private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
