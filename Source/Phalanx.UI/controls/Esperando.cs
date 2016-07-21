using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Phalanx.UI.Controls
{
    public partial class Esperando : UserControl
    {
        public Esperando(string desc)
        {
            InitializeComponent();
            lblMessage.Text = desc;
        }

        public void Show(string desc)
        {
            lblMessage.Text = desc;
        }
    }
}
