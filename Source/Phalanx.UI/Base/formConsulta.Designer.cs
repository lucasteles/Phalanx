namespace Phalanx.UI.Base
{
    partial class formConsulta
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formConsulta));
            this.GRADE = new Phalanx.UI.Controls.SuperGrade(this.components);
            this.btnCancelar = new System.Windows.Forms.Button();
            this.toolMenu = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnFiltrar = new System.Windows.Forms.ToolStripButton();
            this.btnByPK = new System.Windows.Forms.ToolStripButton();
            this.btnRemoverFiltro = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.splitExport = new System.Windows.Forms.ToolStripSplitButton();
            this.xLSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tXTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnImprimir = new System.Windows.Forms.ToolStripSplitButton();
            this.padraoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnInsert = new System.Windows.Forms.ToolStripButton();
            this.btnAlterar = new System.Windows.Forms.ToolStripButton();
            this.btnExcluir = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblQtdRegistros = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.GRADE)).BeginInit();
            this.toolMenu.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // GRADE
            // 
            this.GRADE.AllowUserToAddRows = false;
            this.GRADE.AllowUserToDeleteRows = false;
            this.GRADE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GRADE.BackgroundColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GRADE.DefaultCellStyle = dataGridViewCellStyle1;
            this.GRADE.Location = new System.Drawing.Point(1, 37);
            this.GRADE.MultiSelect = false;
            this.GRADE.MyCondicaoString = null;
            this.GRADE.MyCursor = null;
            this.GRADE.MyQueryFile = null;
            this.GRADE.Name = "GRADE";
            this.GRADE.ReadOnly = true;
            this.GRADE.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GRADE.Size = new System.Drawing.Size(772, 383);
            this.GRADE.TabIndex = 0;
            this.GRADE.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GRADE_CellDoubleClick);
            
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(761, -11);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(10, 10);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolMenu
            // 
            this.toolMenu.AutoSize = false;
            this.toolMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnFiltrar,
            this.btnByPK,
            this.btnRemoverFiltro,
            this.toolStripLabel3,
            this.toolStripSeparator1,
            this.toolStripLabel4,
            this.splitExport,
            this.btnImprimir,
            this.toolStripLabel2,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.btnInsert,
            this.btnAlterar,
            this.btnExcluir});
            this.toolMenu.Location = new System.Drawing.Point(0, 0);
            this.toolMenu.Name = "toolMenu";
            this.toolMenu.Size = new System.Drawing.Size(773, 34);
            this.toolMenu.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.AutoSize = false;
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(30, 30);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFiltrar.Image = global::Phalanx.UI.Properties.Resources.filter;
            this.btnFiltrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(24, 31);
            this.btnFiltrar.Text = "Filtrar";
            // 
            // btnByPK
            // 
            this.btnByPK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnByPK.Image = global::Phalanx.UI.Properties.Resources.lupa;
            this.btnByPK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnByPK.Name = "btnByPK";
            this.btnByPK.Size = new System.Drawing.Size(24, 31);
            this.btnByPK.Text = "Filtrar por ID";
            // 
            // btnRemoverFiltro
            // 
            this.btnRemoverFiltro.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemoverFiltro.Image = global::Phalanx.UI.Properties.Resources.nolupa;
            this.btnRemoverFiltro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoverFiltro.Name = "btnRemoverFiltro";
            this.btnRemoverFiltro.Size = new System.Drawing.Size(24, 31);
            this.btnRemoverFiltro.Text = "Remover Filtro";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(13, 31);
            this.toolStripLabel3.Text = "  ";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(13, 31);
            this.toolStripLabel4.Text = "  ";
            // 
            // splitExport
            // 
            this.splitExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.splitExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xLSToolStripMenuItem,
            this.pDFToolStripMenuItem,
            this.tXTToolStripMenuItem});
            this.splitExport.Image = global::Phalanx.UI.Properties.Resources.Export;
            this.splitExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.splitExport.Name = "splitExport";
            this.splitExport.Size = new System.Drawing.Size(36, 31);
            this.splitExport.Text = "toolStripSplitButton1";
            this.splitExport.ButtonClick += new System.EventHandler(this.splitExport_ButtonClick);
            // 
            // xLSToolStripMenuItem
            // 
            this.xLSToolStripMenuItem.Image = global::Phalanx.UI.Properties.Resources.XLS;
            this.xLSToolStripMenuItem.Name = "xLSToolStripMenuItem";
            this.xLSToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.xLSToolStripMenuItem.Text = "XLS";
            this.xLSToolStripMenuItem.Click += new System.EventHandler(this.xLSToolStripMenuItem_Click);
            // 
            // pDFToolStripMenuItem
            // 
            this.pDFToolStripMenuItem.Image = global::Phalanx.UI.Properties.Resources.pdf;
            this.pDFToolStripMenuItem.Name = "pDFToolStripMenuItem";
            this.pDFToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.pDFToolStripMenuItem.Text = "PDF";
            this.pDFToolStripMenuItem.Click += new System.EventHandler(this.pDFToolStripMenuItem_Click);
            // 
            // tXTToolStripMenuItem
            // 
            this.tXTToolStripMenuItem.Image = global::Phalanx.UI.Properties.Resources.txt;
            this.tXTToolStripMenuItem.Name = "tXTToolStripMenuItem";
            this.tXTToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.tXTToolStripMenuItem.Text = "TXT";
            this.tXTToolStripMenuItem.Click += new System.EventHandler(this.tXTToolStripMenuItem_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.padraoToolStripMenuItem});
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(36, 31);
            this.btnImprimir.Text = "toolStripSplitButton1";
            // 
            // padraoToolStripMenuItem
            // 
            this.padraoToolStripMenuItem.Name = "padraoToolStripMenuItem";
            this.padraoToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.padraoToolStripMenuItem.Text = "Padrao";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(13, 31);
            this.toolStripLabel2.Text = "  ";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(13, 31);
            this.toolStripLabel1.Text = "  ";
            // 
            // btnInsert
            // 
            this.btnInsert.Image = global::Phalanx.UI.Properties.Resources.insert_icon;
            this.btnInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(64, 31);
            this.btnInsert.Text = "Incluir";
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnAlterar
            // 
            this.btnAlterar.Image = global::Phalanx.UI.Properties.Resources.update_icon;
            this.btnAlterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(66, 31);
            this.btnAlterar.Text = "Alterar";
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Image = global::Phalanx.UI.Properties.Resources.delete_icon;
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(65, 31);
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblQtdRegistros});
            this.statusStrip.Location = new System.Drawing.Point(0, 416);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(773, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(680, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // lblQtdRegistros
            // 
            this.lblQtdRegistros.Name = "lblQtdRegistros";
            this.lblQtdRegistros.Size = new System.Drawing.Size(78, 17);
            this.lblQtdRegistros.Text = "Qtd.Registros";
            // 
            // formConsulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(773, 438);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolMenu);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.GRADE);
            this.Location = new System.Drawing.Point(300, 300);
            this.Name = "formConsulta";
            this.Text = "formConsulta";
            this.MyPosEsperando += new System.EventHandler(this.formConsulta_MyPosEsperando);
            this.Load += new System.EventHandler(this.formConsulta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GRADE)).EndInit();
            this.toolMenu.ResumeLayout(false);
            this.toolMenu.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Controls.SuperGrade GRADE;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripButton btnFiltrar;
        private System.Windows.Forms.ToolStripButton btnByPK;
        private System.Windows.Forms.ToolStripButton btnRemoverFiltro;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton splitExport;
        private System.Windows.Forms.ToolStripMenuItem xLSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pDFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tXTToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton btnImprimir;
        private System.Windows.Forms.ToolStripMenuItem padraoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnInsert;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnAlterar;
        private System.Windows.Forms.ToolStripButton btnExcluir;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        public System.Windows.Forms.ToolStrip toolMenu;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblQtdRegistros;



    }
}