namespace Phalanx.UI.Base
{
    partial class F4
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.superGrade1 = new Phalanx.UI.Controls.SuperGrade(this.components);
            this.myComboBox1 = new Phalanx.UI.Controls.MyComboBox(this.components);
            this.txtData = new Phalanx.UI.Controls.MyTextBox(this.components);
            this.btnBuscar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.superGrade1)).BeginInit();
            this.SuspendLayout();
            // 
            // superGrade1
            // 
            this.superGrade1.AllowUserToAddRows = false;
            this.superGrade1.AllowUserToDeleteRows = false;
            this.superGrade1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.superGrade1.BackgroundColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.superGrade1.DefaultCellStyle = dataGridViewCellStyle3;
            this.superGrade1.Location = new System.Drawing.Point(1, 1);
            this.superGrade1.MultiSelect = false;
            this.superGrade1.MyCondicaoString = null;
            this.superGrade1.MyCursor = null;
            this.superGrade1.MyQueryFile = null;
            this.superGrade1.Name = "superGrade1";
            this.superGrade1.ReadOnly = true;
            this.superGrade1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.superGrade1.Size = new System.Drawing.Size(444, 370);
            this.superGrade1.TabIndex = 0;
            // 
            // myComboBox1
            // 
            this.myComboBox1.controlSource = null;
            this.myComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.myComboBox1.FormattingEnabled = true;
            this.myComboBox1.Location = new System.Drawing.Point(12, 377);
            this.myComboBox1.MyColumnOrder = -1;
            this.myComboBox1.MyCondicaoString = null;
            this.myComboBox1.MyCursor = null;
            this.myComboBox1.myDataSource = null;
            this.myComboBox1.myKeyAndValue = false;
            this.myComboBox1.myTipoDataSource = Phalanx.UI.Controls.MyComboBox.TipoDataFIll.None;
            this.myComboBox1.Name = "myComboBox1";
            this.myComboBox1.Size = new System.Drawing.Size(121, 21);
            this.myComboBox1.TabIndex = 1;
            this.myComboBox1.Value = null;
            // 
            // txtData
            // 
            this.txtData.controlSource = null;
            this.txtData.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtData.Location = new System.Drawing.Point(139, 377);
            this.txtData.MyNoAutoResize = false;
            this.txtData.MyQtdDecimais = 0;
            this.txtData.Name = "txtData";
            this.txtData.PromptChar = ' ';
            this.txtData.ResetOnSpace = false;
            this.txtData.Size = new System.Drawing.Size(210, 26);
            this.txtData.TabIndex = 2;
            this.txtData.Value = null;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(355, 375);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // F4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(442, 437);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.myComboBox1);
            this.Controls.Add(this.superGrade1);
            this.Name = "F4";
            this.Text = "Busca";
            this.Load += new System.EventHandler(this.F4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.superGrade1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.SuperGrade superGrade1;
        private Controls.MyComboBox myComboBox1;
        private Controls.MyTextBox txtData;
        private System.Windows.Forms.Button btnBuscar;
    }
}
