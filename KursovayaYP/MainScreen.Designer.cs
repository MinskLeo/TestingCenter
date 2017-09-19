namespace KursovayaYP
{
    partial class MainScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.but_NewTest = new System.Windows.Forms.Button();
            this.but_Calc = new System.Windows.Forms.Button();
            this.gb_Buttons = new System.Windows.Forms.GroupBox();
            this.gb_Buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // but_NewTest
            // 
            this.but_NewTest.Location = new System.Drawing.Point(16, 24);
            this.but_NewTest.Name = "but_NewTest";
            this.but_NewTest.Size = new System.Drawing.Size(106, 30);
            this.but_NewTest.TabIndex = 1;
            this.but_NewTest.Text = "Новый тест";
            this.but_NewTest.UseVisualStyleBackColor = true;
            this.but_NewTest.Click += new System.EventHandler(this.but_NewTest_Click);
            // 
            // but_Calc
            // 
            this.but_Calc.Location = new System.Drawing.Point(16, 75);
            this.but_Calc.Name = "but_Calc";
            this.but_Calc.Size = new System.Drawing.Size(106, 28);
            this.but_Calc.TabIndex = 2;
            this.but_Calc.Text = "Калькулятор";
            this.but_Calc.UseVisualStyleBackColor = true;
            // 
            // gb_Buttons
            // 
            this.gb_Buttons.Controls.Add(this.but_NewTest);
            this.gb_Buttons.Controls.Add(this.but_Calc);
            this.gb_Buttons.Location = new System.Drawing.Point(580, 12);
            this.gb_Buttons.Name = "gb_Buttons";
            this.gb_Buttons.Size = new System.Drawing.Size(133, 137);
            this.gb_Buttons.TabIndex = 3;
            this.gb_Buttons.TabStop = false;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(725, 586);
            this.Controls.Add(this.gb_Buttons);
            this.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[Отчество][Имя][Фамилия]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainScreen_FormClosing);
            this.gb_Buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button but_NewTest;
        private System.Windows.Forms.Button but_Calc;
        private System.Windows.Forms.GroupBox gb_Buttons;
    }
}