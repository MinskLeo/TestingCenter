namespace KursovayaYP
{
    partial class TestResults
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
            this.gb_Results = new System.Windows.Forms.GroupBox();
            this.lb_Correct = new System.Windows.Forms.Label();
            this.lb_Incorrect = new System.Windows.Forms.Label();
            this.lb_Mark = new System.Windows.Forms.Label();
            this.but_Exit = new System.Windows.Forms.Button();
            this.gb_Results.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_Results
            // 
            this.gb_Results.Controls.Add(this.lb_Mark);
            this.gb_Results.Controls.Add(this.lb_Incorrect);
            this.gb_Results.Controls.Add(this.lb_Correct);
            this.gb_Results.Location = new System.Drawing.Point(63, 12);
            this.gb_Results.Name = "gb_Results";
            this.gb_Results.Size = new System.Drawing.Size(324, 281);
            this.gb_Results.TabIndex = 0;
            this.gb_Results.TabStop = false;
            // 
            // lb_Correct
            // 
            this.lb_Correct.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_Correct.Location = new System.Drawing.Point(6, 54);
            this.lb_Correct.Name = "lb_Correct";
            this.lb_Correct.Size = new System.Drawing.Size(312, 41);
            this.lb_Correct.TabIndex = 0;
            this.lb_Correct.Text = "Правильно:[n\\всех]";
            // 
            // lb_Incorrect
            // 
            this.lb_Incorrect.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_Incorrect.Location = new System.Drawing.Point(6, 107);
            this.lb_Incorrect.Name = "lb_Incorrect";
            this.lb_Incorrect.Size = new System.Drawing.Size(312, 41);
            this.lb_Incorrect.TabIndex = 1;
            this.lb_Incorrect.Text = "Неправильно:[n\\всех]";
            // 
            // lb_Mark
            // 
            this.lb_Mark.Font = new System.Drawing.Font("Consolas", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_Mark.Location = new System.Drawing.Point(52, 163);
            this.lb_Mark.Name = "lb_Mark";
            this.lb_Mark.Size = new System.Drawing.Size(212, 102);
            this.lb_Mark.TabIndex = 2;
            this.lb_Mark.Text = "100";
            this.lb_Mark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // but_Exit
            // 
            this.but_Exit.Location = new System.Drawing.Point(166, 318);
            this.but_Exit.Name = "but_Exit";
            this.but_Exit.Size = new System.Drawing.Size(101, 41);
            this.but_Exit.TabIndex = 1;
            this.but_Exit.Text = "Выход";
            this.but_Exit.UseVisualStyleBackColor = true;
            this.but_Exit.Click += new System.EventHandler(this.but_Exit_Click);
            // 
            // TestResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 371);
            this.Controls.Add(this.but_Exit);
            this.Controls.Add(this.gb_Results);
            this.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestResults";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Результаты теста";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestResults_FormClosing);
            this.gb_Results.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_Results;
        private System.Windows.Forms.Label lb_Mark;
        private System.Windows.Forms.Label lb_Incorrect;
        private System.Windows.Forms.Label lb_Correct;
        private System.Windows.Forms.Button but_Exit;
    }
}