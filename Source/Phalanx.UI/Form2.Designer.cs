namespace Phalanx.UI
{
    partial class Form2
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
            this.myTextBox2 = new Phalanx.UI.Controls.MyTextBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cntItens1 = new Phalanx.UI.Controls.CntItens();
            this.label3 = new System.Windows.Forms.Label();
            this.myComboBox1 = new Phalanx.UI.Controls.MyComboBox(this.components);
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(225, 353);
            this.btnCancelar.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(128, 353);
            this.btnOK.TabIndex = 2;
            // 
            // lblID
            // 
            this.lblID.Location = new System.Drawing.Point(10, 373);
            // 
            // myTextBox1
            // 
            this.myTextBox1.controlSource = "DS_EXEMPLO";
            this.myTextBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myTextBox1.Location = new System.Drawing.Point(128, 22);
            this.myTextBox1.MyNoAutoResize = true;
            this.myTextBox1.MyQtdDecimais = 0;
            this.myTextBox1.Name = "myTextBox1";
            this.myTextBox1.PromptChar = ' ';
            this.myTextBox1.ResetOnSpace = false;
            this.myTextBox1.Size = new System.Drawing.Size(306, 26);
            this.myTextBox1.TabIndex = 0;
            this.myTextBox1.Value = null;
            // 
            // myTextBox2
            // 
            this.myTextBox2.controlSource = "VL_EXEMPLO";
            this.myTextBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myTextBox2.Location = new System.Drawing.Point(128, 68);
            this.myTextBox2.MyNoAutoResize = false;
            this.myTextBox2.MyQtdDecimais = 0;
            this.myTextBox2.Name = "myTextBox2";
            this.myTextBox2.PromptChar = ' ';
            this.myTextBox2.ResetOnSpace = false;
            this.myTextBox2.Size = new System.Drawing.Size(181, 26);
            this.myTextBox2.TabIndex = 1;
            this.myTextBox2.Value = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(70, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Nome";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(70, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Valor";
            // 
            // cntItens1
            // 
            this.cntItens1.Location = new System.Drawing.Point(17, 166);
            this.cntItens1.MyAlterar = true;
            this.cntItens1.MyCursor = null;
            this.cntItens1.MyExcluir = true;
            this.cntItens1.MyInlcuir = true;
            this.cntItens1.MyParameters = null;
            this.cntItens1.MyQueryFile = "consulta1_itens";
            this.cntItens1.Name = "cntItens1";
            this.cntItens1.Size = new System.Drawing.Size(417, 167);
            this.cntItens1.TabIndex = 8;
            this.cntItens1.MyCallForm += new System.EventHandler(this.cntItens1_MyCallForm);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(70, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "Tipo";
            // 
            // myComboBox1
            // 
            this.myComboBox1.controlSource = "TG_TIPO";
            this.myComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.myComboBox1.FormattingEnabled = true;
            this.myComboBox1.Location = new System.Drawing.Point(128, 114);
            this.myComboBox1.MyColumnOrder = -1;
            this.myComboBox1.MyCondicaoString = null;
            this.myComboBox1.MyCursor = null;
            this.myComboBox1.myDataSource = "TIPO_TESTE";
            this.myComboBox1.myKeyAndValue = true;
            this.myComboBox1.myTipoDataSource = Phalanx.UI.Controls.MyComboBox.TipoDataFIll.ComboTable;
            this.myComboBox1.Name = "myComboBox1";
            this.myComboBox1.Size = new System.Drawing.Size(181, 21);
            this.myComboBox1.TabIndex = 10;
            this.myComboBox1.Value = null;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 386);
            this.Controls.Add(this.myComboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cntItens1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.myTextBox2);
            this.Controls.Add(this.myTextBox1);
            this.MyTabela = "TESTE";
            this.Name = "Form2";
            this.Text = "Form2";
            this.MyInit += new System.EventHandler(this.Form2_MyInit);
            this.MyPosOK += new System.EventHandler(this.Form2_MyPosOK);
            this.Controls.SetChildIndex(this.lblID, 0);
            this.Controls.SetChildIndex(this.myTextBox1, 0);
            this.Controls.SetChildIndex(this.btnCancelar, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.myTextBox2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cntItens1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.myComboBox1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MyTextBox myTextBox1;
        private Controls.MyTextBox myTextBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Controls.CntItens cntItens1;
        private System.Windows.Forms.Label label3;
        private Controls.MyComboBox myComboBox1;
    }
}