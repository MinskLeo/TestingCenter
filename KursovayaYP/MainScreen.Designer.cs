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
            this.data_DataGrid = new System.Windows.Forms.DataGridView();
            this.but_Update = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTime_From = new System.Windows.Forms.DateTimePicker();
            this.dateTime_To = new System.Windows.Forms.DateTimePicker();
            this.gb_Buttons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_DataGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            // data_DataGrid
            // 
            this.data_DataGrid.AllowUserToResizeColumns = false;
            this.data_DataGrid.AllowUserToResizeRows = false;
            this.data_DataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.data_DataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.data_DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_DataGrid.Location = new System.Drawing.Point(12, 12);
            this.data_DataGrid.MultiSelect = false;
            this.data_DataGrid.Name = "data_DataGrid";
            this.data_DataGrid.ReadOnly = true;
            this.data_DataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.data_DataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.data_DataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.data_DataGrid.Size = new System.Drawing.Size(562, 562);
            this.data_DataGrid.TabIndex = 4;
            // 
            // but_Update
            // 
            this.but_Update.Location = new System.Drawing.Point(16, 24);
            this.but_Update.Name = "but_Update";
            this.but_Update.Size = new System.Drawing.Size(106, 28);
            this.but_Update.TabIndex = 5;
            this.but_Update.Text = "Обновить";
            this.but_Update.UseVisualStyleBackColor = true;
            this.but_Update.Click += new System.EventHandler(this.but_Update_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTime_To);
            this.groupBox1.Controls.Add(this.dateTime_From);
            this.groupBox1.Controls.Add(this.but_Update);
            this.groupBox1.Location = new System.Drawing.Point(580, 167);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(133, 163);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // dateTime_From
            // 
            this.dateTime_From.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTime_From.Location = new System.Drawing.Point(16, 78);
            this.dateTime_From.Name = "dateTime_From";
            this.dateTime_From.Size = new System.Drawing.Size(106, 25);
            this.dateTime_From.TabIndex = 6;
            // 
            // dateTime_To
            // 
            this.dateTime_To.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTime_To.Location = new System.Drawing.Point(16, 119);
            this.dateTime_To.Name = "dateTime_To";
            this.dateTime_To.Size = new System.Drawing.Size(106, 25);
            this.dateTime_To.TabIndex = 7;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(725, 586);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.data_DataGrid);
            this.Controls.Add(this.gb_Buttons);
            this.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[Отчество][Имя][Фамилия]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainScreen_FormClosing);
            this.Load += new System.EventHandler(this.MainScreen_Load);
            this.gb_Buttons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.data_DataGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button but_NewTest;
        private System.Windows.Forms.Button but_Calc;
        private System.Windows.Forms.GroupBox gb_Buttons;
        private System.Windows.Forms.DataGridView data_DataGrid;
        private System.Windows.Forms.Button but_Update;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTime_To;
        private System.Windows.Forms.DateTimePicker dateTime_From;
    }
}