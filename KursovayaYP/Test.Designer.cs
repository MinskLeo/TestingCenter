namespace KursovayaYP
{
    partial class Test
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
            this.lb_Question = new System.Windows.Forms.Label();
            this.gb_Answers = new System.Windows.Forms.GroupBox();
            this.flow_Questions = new System.Windows.Forms.FlowLayoutPanel();
            this.cb_1 = new System.Windows.Forms.CheckBox();
            this.cb_2 = new System.Windows.Forms.CheckBox();
            this.cb_3 = new System.Windows.Forms.CheckBox();
            this.cb_4 = new System.Windows.Forms.CheckBox();
            this.cb_5 = new System.Windows.Forms.CheckBox();
            this.cb_6 = new System.Windows.Forms.CheckBox();
            this.gb_Answers.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_Question
            // 
            this.lb_Question.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_Question.Location = new System.Drawing.Point(12, 9);
            this.lb_Question.Name = "lb_Question";
            this.lb_Question.Size = new System.Drawing.Size(1141, 145);
            this.lb_Question.TabIndex = 0;
            this.lb_Question.Text = "[текст вопроса]";
            // 
            // gb_Answers
            // 
            this.gb_Answers.Controls.Add(this.cb_6);
            this.gb_Answers.Controls.Add(this.cb_5);
            this.gb_Answers.Controls.Add(this.cb_4);
            this.gb_Answers.Controls.Add(this.cb_3);
            this.gb_Answers.Controls.Add(this.cb_2);
            this.gb_Answers.Controls.Add(this.cb_1);
            this.gb_Answers.Location = new System.Drawing.Point(12, 157);
            this.gb_Answers.Name = "gb_Answers";
            this.gb_Answers.Size = new System.Drawing.Size(1141, 276);
            this.gb_Answers.TabIndex = 1;
            this.gb_Answers.TabStop = false;
            this.gb_Answers.Text = "Варианты ответов:";
            // 
            // flow_Questions
            // 
            this.flow_Questions.AutoScroll = true;
            this.flow_Questions.Location = new System.Drawing.Point(12, 439);
            this.flow_Questions.Name = "flow_Questions";
            this.flow_Questions.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.flow_Questions.Size = new System.Drawing.Size(1141, 54);
            this.flow_Questions.TabIndex = 2;
            this.flow_Questions.WrapContents = false;
            // 
            // cb_1
            // 
            this.cb_1.AutoSize = true;
            this.cb_1.Location = new System.Drawing.Point(30, 30);
            this.cb_1.Name = "cb_1";
            this.cb_1.Size = new System.Drawing.Size(91, 22);
            this.cb_1.TabIndex = 0;
            this.cb_1.Text = "answer_1";
            this.cb_1.UseVisualStyleBackColor = true;
            this.cb_1.Visible = false;
            // 
            // cb_2
            // 
            this.cb_2.AutoSize = true;
            this.cb_2.Location = new System.Drawing.Point(30, 70);
            this.cb_2.Name = "cb_2";
            this.cb_2.Size = new System.Drawing.Size(91, 22);
            this.cb_2.TabIndex = 1;
            this.cb_2.Text = "answer_2";
            this.cb_2.UseVisualStyleBackColor = true;
            this.cb_2.Visible = false;
            // 
            // cb_3
            // 
            this.cb_3.AutoSize = true;
            this.cb_3.Location = new System.Drawing.Point(30, 110);
            this.cb_3.Name = "cb_3";
            this.cb_3.Size = new System.Drawing.Size(91, 22);
            this.cb_3.TabIndex = 2;
            this.cb_3.Text = "answer_3";
            this.cb_3.UseVisualStyleBackColor = true;
            this.cb_3.Visible = false;
            // 
            // cb_4
            // 
            this.cb_4.AutoSize = true;
            this.cb_4.Location = new System.Drawing.Point(30, 150);
            this.cb_4.Name = "cb_4";
            this.cb_4.Size = new System.Drawing.Size(91, 22);
            this.cb_4.TabIndex = 3;
            this.cb_4.Text = "answer_4";
            this.cb_4.UseVisualStyleBackColor = true;
            this.cb_4.Visible = false;
            // 
            // cb_5
            // 
            this.cb_5.AutoSize = true;
            this.cb_5.Location = new System.Drawing.Point(30, 190);
            this.cb_5.Name = "cb_5";
            this.cb_5.Size = new System.Drawing.Size(91, 22);
            this.cb_5.TabIndex = 4;
            this.cb_5.Text = "answer_5";
            this.cb_5.UseVisualStyleBackColor = true;
            this.cb_5.Visible = false;
            // 
            // cb_6
            // 
            this.cb_6.AutoSize = true;
            this.cb_6.Location = new System.Drawing.Point(30, 230);
            this.cb_6.Name = "cb_6";
            this.cb_6.Size = new System.Drawing.Size(91, 22);
            this.cb_6.TabIndex = 5;
            this.cb_6.Text = "answer_6";
            this.cb_6.UseVisualStyleBackColor = true;
            this.cb_6.Visible = false;
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 582);
            this.Controls.Add(this.flow_Questions);
            this.Controls.Add(this.gb_Answers);
            this.Controls.Add(this.lb_Question);
            this.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Test";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[Название предмета]-[ФИО сдающего]";
            this.Load += new System.EventHandler(this.Test_Load);
            this.gb_Answers.ResumeLayout(false);
            this.gb_Answers.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb_Question;
        private System.Windows.Forms.GroupBox gb_Answers;
        private System.Windows.Forms.FlowLayoutPanel flow_Questions;
        private System.Windows.Forms.CheckBox cb_6;
        private System.Windows.Forms.CheckBox cb_5;
        private System.Windows.Forms.CheckBox cb_4;
        private System.Windows.Forms.CheckBox cb_3;
        private System.Windows.Forms.CheckBox cb_2;
        private System.Windows.Forms.CheckBox cb_1;
    }
}