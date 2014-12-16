namespace Phalanx.UI
{
    partial class Form1
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
            ((System.ComponentModel.ISupportInitialize)(this.GRADE)).BeginInit();
            this.SuspendLayout();
            // 
            // GRADE
            // 
            this.GRADE.Location = new System.Drawing.Point(0, 35);
            this.GRADE.MyQueryFile = "consulta1";
            this.GRADE.Size = new System.Drawing.Size(638, 285);
            this.GRADE.MyCellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.GRADE_MyCellPainting);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 318);
            this.Location = new System.Drawing.Point(0, 0);
            this.MyAlterar = true;
            this.MyDigitar = "Form2";
            this.MyExcluir = true;
            this.MyInlcuir = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.MyPreDeletar += new System.EventHandler(this.Form1_MyPreDeletar);
            ((System.ComponentModel.ISupportInitialize)(this.GRADE)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion








    }
}