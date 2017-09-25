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

        public Test(string id,string[] test)
        {
            InitializeComponent();
            ID = id;
            TEST = test;
        }

        private void Test_Load(object sender, EventArgs e)
        {
            for(int i=0;i<TEST.Length;i++)//DEBUG
            {
                textBox1.Text += TEST[i];
            }
        }
    }
}
