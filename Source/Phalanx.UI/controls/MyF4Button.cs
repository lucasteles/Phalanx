using Phalanx.UI.Base;
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
    public partial class MyF4Button : PictureBox
    {
        public MyTextBox myTextBox { get; set; }
        public F4 myForm { get; set; }
        public string table { get; set; }

        private void _Set() {
            Image = Properties.Resources.F4;
            SizeMode = PictureBoxSizeMode.StretchImage;
            this.Size = new Size(27, 27);
            this.Cursor = Cursors.Hand;

            this.Click += myClick;

        }

        private void myClick(object sender, EventArgs e)
        {
            if (myForm == null)
                myForm = new F4(this.table);
            
            myForm.ShowDialog();
            myTextBox.Value = myForm.Value;

        }

        public MyF4Button()
        {
            InitializeComponent();
            _Set();
            
        }

        public MyF4Button(IContainer container)
        {

            InitializeComponent();

            _Set();
            
            container.Add(this);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
