namespace KursovayaYP
{
    partial class TestsList
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
            this.list_Tests = new System.Windows.Forms.ListBox();
            this.but_Choose = new System.Windows.Forms.Button();
            this.but_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // list_Tests
            // 
            this.list_Tests.FormattingEnabled = true;
            this.list_Tests.ItemHeight = 27;
            this.list_Tests.Location = new System.Drawing.Point(12, 12);
            this.list_Tests.Name = "list_Tests";
            this.list_Tests.Size = new System.Drawing.Size(208, 355);
            this.list_Tests.TabIndex = 0;
            // 
            // but_Choose
            // 
            this.but_Choose.Location = new System.Drawing.Point(119, 390);
            this.but_Choose.Name = "but_Choose";
            this.but_Choose.Size = new System.Drawing.Size(75, 28);
            this.but_Choose.TabIndex = 1;
            this.but_Choose.Text = "Выбрать";
            this.but_Choose.UseVisualStyleBackColor = true;
            this.but_Choose.Click += new System.EventHandler(this.but_Choose_Click);
            // 
            // but_Cancel
            // 
            this.but_Cancel.Location = new System.Drawing.Point(38, 390);
            this.but_Cancel.Name = "but_Cancel";
            this.but_Cancel.Size = new System.Drawing.Size(75, 28);
            this.but_Cancel.TabIndex = 2;
            this.but_Cancel.Text = "Отмена";
            this.but_Cancel.UseVisualStyleBackColor = true;
            this.but_Cancel.Click += new System.EventHandler(this.but_Cancel_Click);
            // 
            // TestsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 430);
            this.Controls.Add(this.but_Cancel);
            this.Controls.Add(this.but_Choose);
            this.Controls.Add(this.list_Tests);
            this.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "TestsList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestsList";
            this.Load += new System.EventHandler(this.TestsList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox list_Tests;
        private System.Windows.Forms.Button but_Choose;
        private System.Windows.Forms.Button but_Cancel;
    }
}