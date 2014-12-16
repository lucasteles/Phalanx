using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Phalanx.DataAccess;
using Phalanx.Common;

namespace Phalanx.UI.Controls
{

    public partial class MyMenuIcon : UserControl
    {

        
        // definição do evendo
        public event EventHandler Run;

        // chamadas de eventos
        public virtual void DoRun(EventArgs e, Object o) {  if (Run != null)  Run(o, e); }

                
        public  string MyText
        {
            get
            {
                return this.lblTitulo.Text;
            }
            set
            {
                this.lblTitulo.Text = value;
                this.lblTitulo.Left = (int)( this.Width / 2 - this.lblTitulo.Width /2); 
            }

        }

        public MyMenuIcon()
        {
            InitializeComponent();
            

        }

        public void SetIcon(String icon)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            pictureBox1.BackColor = Color.Transparent;
            lblTitulo.BackColor = Color.Transparent;
            try
            {
                pictureBox1.Image = Image.FromFile(RuntimeParameters.GetDefaultIconPath() + icon);
            }
            catch (Exception)
            {

                pictureBox1.Image = Image.FromFile(RuntimeParameters.GetDefaultIconPath() + "nfound.png");
            }
                
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DoRun(e, this);
        }

        private void MyMenuIcon_Click(object sender, EventArgs e)
        {
            DoRun(e, this);
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {
            DoRun(e, this);
        }

        private void showfocus()
        {
            this.pictureBox1.BackColor = shape.BackColor;
            this.lblTitulo.BackColor = shape.BackColor;
            shape.Visible = true;
            

        }
        private void hidefocus()
        {
            this.pictureBox1.BackColor = Color.White;
            this.lblTitulo.BackColor = Color.White;
            shape.Visible = false; 
        }

        private void MyMenuIcon_MouseEnter(object sender, EventArgs e)
        {
            showfocus();
        }

        private void MyMenuIcon_MouseLeave(object sender, EventArgs e)
        {
            hidefocus();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            showfocus();
        }

        private void lblTitulo_MouseEnter(object sender, EventArgs e)
        {
            showfocus();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            hidefocus();
        }

        private void lblTitulo_MouseLeave(object sender, EventArgs e)
        {
            hidefocus();
        }




    }
}
