using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Phalanx.UI.Controls
{
    public partial class F4Button : PictureBox
    {
        public TextBox myTextBox { get; set; }
        public Form myForm { get; set; }

        public F4Button()
        {
            InitializeComponent();
            Image = Properties.Resources.F4;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
