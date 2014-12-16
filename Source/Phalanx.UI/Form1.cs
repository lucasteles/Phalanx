using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Phalanx.UI.Base;
using Phalanx.DataAccess;

namespace Phalanx.UI
{
    public partial class Form1 : formConsulta
    {
        public Form1()
        {
            InitializeComponent();
            this.MyExecutar();            
          
                  

        }

      
        private void Form1_MyPreDeletar(object sender, EventArgs e)
        {
            Things obj = (Things)sender;
            Condition cond = new Condition("FK_TESTE=@ID");
            cond["ID"] = obj["PK_ID"];
            FoxCursor tmp = new FoxCursor("SELECT * FROM TB_ITENSTESTE",cond);
            
            if (tmp.Reccount() > 0)
            {
                this.CancelDelete = true;
                this.CancelDeleteMessage = "Registro possui itens, nao pode ser deletado";

            }

        }

        private void GRADE_MyCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DataGridView grade = sender as DataGridView;
            if (grade.Columns[e.ColumnIndex].Name == "VL_EXEMPLO")
                if ((Decimal)e.Value < 0)
                    e.CellStyle.ForeColor = Color.Red;

        }

      
    }
}
