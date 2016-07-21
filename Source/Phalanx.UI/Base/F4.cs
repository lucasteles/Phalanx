using Phalanx.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Phalanx.UI.Base
{
    public partial class F4 : Phalanx.UI.Base.form_base
    {
        public object Value { get; set; }
        public string Table { get; set; }

        public F4(string table)
        {
            InitializeComponent();

            Table = table;

        }
               

        private void F4_Load(object sender, EventArgs e)
        {
            var cond = new Condition("DS_TABELA=@ID");
            cond["ID"] = Table;
            var tmp = new FoxCursor("SELECT DS_CONSULTA,DS_CAMPO,DS_TABELA FROM TS_F4 {COND} order by DS_CAMPO", cond);

            if (tmp.Reccount() == 0)
            {
                MessageBox.Show("F4 não configurado");
                Value = null;
                Close();
            }

            myComboBox1.UseBlankLine = false;
            myComboBox1.myTipoDataSource = UI.Controls.MyComboBox.TipoDataFIll.Cursor;
            myComboBox1.MyCursor = tmp;
            myComboBox1.Start();

            buscar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void buscar()
        {
            var query = myComboBox1.Value.ToString();
            query = query.Replace("{VALUE}", txtData.Text);
            superGrade1.MyCursor = new FoxCursor(query);
            superGrade1.MyConfigurar();
            superGrade1.Refresh();
        }

      
    }
}
