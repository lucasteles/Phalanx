using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Phalanx.UI.Controls
{
   // [Designer(typeof(ControlDesigner))]
    


    //[Designer(typeof(DataGridView))]
    public partial class SuperGrade 
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // SuperGrade
            // 
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.BackgroundColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DefaultCellStyle = dataGridViewCellStyle1;
            this.MultiSelect = false;
            this.ReadOnly = true;
            this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.SuperGrade_CellFormatting);
            this.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.SuperGrade_CellMouseDown);
            this.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.SuperGrade_CellPainting);
            this.SelectionChanged += new System.EventHandler(this.SuperGrade_SelectionChanged);
            this.Sorted += new System.EventHandler(this.SuperGrade_Sorted);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SuperGrade_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SuperGrade_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }

}
