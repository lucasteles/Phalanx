namespace Phalanx.UI.Controls
{
    partial class CntItens
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GRADE = new Phalanx.UI.Controls.SuperGrade(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.GRADE)).BeginInit();
            this.SuspendLayout();
            // 
            // GRADE
            // 
            this.GRADE.AllowUserToAddRows = false;
            this.GRADE.AllowUserToDeleteRows = false;
            this.GRADE.BackgroundColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GRADE.DefaultCellStyle = dataGridViewCellStyle1;
            this.GRADE.Location = new System.Drawing.Point(0, 0);
            this.GRADE.MultiSelect = false;
            this.GRADE.MyCondicaoString = null;
            this.GRADE.MyCursor = null;
            this.GRADE.MyQueryFile = null;
            this.GRADE.Name = "GRADE";
            this.GRADE.ReadOnly = true;
            this.GRADE.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GRADE.Size = new System.Drawing.Size(417, 299);
            this.GRADE.TabIndex = 0;
            this.GRADE.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GRADE_CellDoubleClick);
            // 
            // CntItens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GRADE);
            this.Name = "CntItens";
            this.Size = new System.Drawing.Size(417, 301);
            this.Load += new System.EventHandler(this.CntItens_Load);
            this.Resize += new System.EventHandler(this.CntItens_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.GRADE)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SuperGrade GRADE;
    }
}
