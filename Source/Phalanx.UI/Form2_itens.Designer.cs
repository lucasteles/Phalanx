namespace Phalanx.UI
{
    partial class Form2_itens
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
            this.myTextBox1 = new Phalanx.UI.Controls.MyTextBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.myTextBox2 = new Phalanx.UI.Controls.MyTextBox(this.components);
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(175, 117);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(83, 117);
            this.btnOK.TabIndex = 1;
            // 
            // myTextBox1
            // 
            this.myTextBox1.controlSource = "DS_ITEM";
            this.myTextBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myTextBox1.Location = new System.Drawing.Point(83, 25);
            this.myTextBox1.MyNoAutoResize = false;
            this.myTextBox1.MyQtdDecimais = 0;
            this.myTextBox1.Name = "myTextBox1";
            this.myTextBox1.PromptChar = ' ';
            this.myTextBox1.ResetOnSpace = false;
            this.myTextBox1.Size = new System.Drawing.Size(199, 26);
            this.myTextBox1.TabIndex = 0;
            this.myTextBox1.Value = null;
            this.myTextBox1.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.myTextBox1_MaskInputRejected);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Item";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Dia";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // myTextBox2
            // 
            this.myTextBox2.controlSource = "DT_ITEM";
            this.myTextBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myTextBox2.Location = new System.Drawing.Point(83, 67);
            this.myTextBox2.MyNoAutoResize = false;
            this.myTextBox2.MyQtdDecimais = 0;
            this.myTextBox2.Name = "myTextBox2";
            this.myTextBox2.PromptChar = ' ';
            this.myTextBox2.ResetOnSpace = false;
            this.myTextBox2.Size = new System.Drawing.Size(199, 26);
            this.myTextBox2.TabIndex = 6;
            this.myTextBox2.Value = null;
            this.myTextBox2.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.myTextBox2_MaskInputRejected);
            // 
            // Form2_itens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 151);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.myTextBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.myTextBox1);
            this.MyTabela = "TB_ITENSTESTE";
            this.Name = "Form2_itens";
            this.Text = "Form2_itens";
            this.Controls.SetChildIndex(this.btnCancelar, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.myTextBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.myTextBox2, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MyTextBox myTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Controls.MyTextBox myTextBox2;
    }
}