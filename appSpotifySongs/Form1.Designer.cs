namespace appSpotifySongs
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvSongs = new DataGridView();
            btnCSV = new Button();
            btnXML = new Button();
            btnJSON = new Button();
            button1 = new Button();
            btnSaveSQL = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvSongs).BeginInit();
            SuspendLayout();
            // 
            // dgvSongs
            // 
            dgvSongs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSongs.Location = new Point(30, 69);
            dgvSongs.Name = "dgvSongs";
            dgvSongs.RowHeadersWidth = 51;
            dgvSongs.Size = new Size(890, 413);
            dgvSongs.TabIndex = 0;
            dgvSongs.CellValueChanged += dgvSongs_CellValueChanged;
            // 
            // btnCSV
            // 
            btnCSV.Location = new Point(1321, 70);
            btnCSV.Name = "btnCSV";
            btnCSV.Size = new Size(151, 44);
            btnCSV.TabIndex = 1;
            btnCSV.Text = "Export with CSV";
            btnCSV.UseVisualStyleBackColor = true;
            btnCSV.Click += btnCSV_Click;
            // 
            // btnXML
            // 
            btnXML.Location = new Point(1321, 120);
            btnXML.Name = "btnXML";
            btnXML.Size = new Size(151, 44);
            btnXML.TabIndex = 2;
            btnXML.Text = "Export with XML";
            btnXML.UseVisualStyleBackColor = true;
            btnXML.Click += btnXML_Click;
            // 
            // btnJSON
            // 
            btnJSON.Location = new Point(1321, 170);
            btnJSON.Name = "btnJSON";
            btnJSON.Size = new Size(151, 44);
            btnJSON.TabIndex = 3;
            btnJSON.Text = "Export with JSON";
            btnJSON.UseVisualStyleBackColor = true;
            btnJSON.Click += btnJSON_Click;
            // 
            // button1
            // 
            button1.Location = new Point(1321, 220);
            button1.Name = "button1";
            button1.Size = new Size(151, 44);
            button1.TabIndex = 4;
            button1.Text = "Export with PDF";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnSaveSQL
            // 
            btnSaveSQL.Location = new Point(926, 72);
            btnSaveSQL.Name = "btnSaveSQL";
            btnSaveSQL.Size = new Size(151, 44);
            btnSaveSQL.TabIndex = 5;
            btnSaveSQL.Text = "Save";
            btnSaveSQL.UseVisualStyleBackColor = true;
            btnSaveSQL.Click += btnSaveSQL_Click;
            // 
            // button2
            // 
            button2.Location = new Point(926, 122);
            button2.Name = "button2";
            button2.Size = new Size(151, 44);
            button2.TabIndex = 6;
            button2.Text = "Reset";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(1484, 521);
            Controls.Add(button2);
            Controls.Add(btnSaveSQL);
            Controls.Add(button1);
            Controls.Add(btnJSON);
            Controls.Add(btnXML);
            Controls.Add(btnCSV);
            Controls.Add(dgvSongs);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSongs).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvSongs;
        private Button btnCSV;
        private Button btnXML;
        private Button btnJSON;
        private Button button1;
        private Button btnSaveSQL;
        private Button button2;
    }
}
