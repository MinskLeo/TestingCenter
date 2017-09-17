namespace KursovayaYP
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_StudNumber = new System.Windows.Forms.Label();
            this.mtb_StudNumb = new System.Windows.Forms.MaskedTextBox();
            this.but_Login = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_StudNumber
            // 
            this.lb_StudNumber.AutoSize = true;
            this.lb_StudNumber.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_StudNumber.Location = new System.Drawing.Point(12, 26);
            this.lb_StudNumber.Name = "lb_StudNumber";
            this.lb_StudNumber.Size = new System.Drawing.Size(467, 28);
            this.lb_StudNumber.TabIndex = 0;
            this.lb_StudNumber.Text = "Введите номер студенческого билета:";
            this.lb_StudNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mtb_StudNumb
            // 
            this.mtb_StudNumb.Location = new System.Drawing.Point(137, 78);
            this.mtb_StudNumb.Mask = "00000000000000";
            this.mtb_StudNumb.Name = "mtb_StudNumb";
            this.mtb_StudNumb.Size = new System.Drawing.Size(217, 25);
            this.mtb_StudNumb.TabIndex = 1;
            this.mtb_StudNumb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // but_Login
            // 
            this.but_Login.Location = new System.Drawing.Point(200, 127);
            this.but_Login.Name = "but_Login";
            this.but_Login.Size = new System.Drawing.Size(90, 28);
            this.but_Login.TabIndex = 2;
            this.but_Login.Text = "Войти";
            this.but_Login.UseVisualStyleBackColor = true;
            this.but_Login.Click += new System.EventHandler(this.but_Login_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 167);
            this.Controls.Add(this.but_Login);
            this.Controls.Add(this.mtb_StudNumb);
            this.Controls.Add(this.lb_StudNumber);
            this.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_StudNumber;
        private System.Windows.Forms.MaskedTextBox mtb_StudNumb;
        private System.Windows.Forms.Button but_Login;
    }
}

