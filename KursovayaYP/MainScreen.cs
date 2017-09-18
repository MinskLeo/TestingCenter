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
        public static string FirstName, Surname, MiddleName;
        public MainScreen(string surname, string name, string middleName)//Не получается открывать то окно (логин скрин), которое мейновое, если закрыть это
        {
            InitializeComponent();
            FirstName = name;
            Surname = surname;
            MiddleName = middleName;

            this.Text = Surname + " " + FirstName + " " + MiddleName;
        }

        private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
