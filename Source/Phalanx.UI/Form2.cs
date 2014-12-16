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
    public partial class Form2 : formDigitar
    {
        public Form2()
        {
            InitializeComponent();           
        }

        private void Form2_MyInit(object sender, EventArgs e)
        {
            cntItens1.MyCondicao.Comando = "FK_TESTE=@TESTEID";
            cntItens1.MyCondicao.Add("TESTEID", this.EE["PK_ID"]);
            cntItens1.MyAmbientar();

            
        }

        private void cntItens1_MyCallForm(object sender, EventArgs e)
        {
            FoxCursor tmp = cntItens1.MyCursor;
            switch (sender.ToString().Trim())
            {
                case "Alterar":
                    cntItens1.oRet = Caller.ChamaForm("Form2_itens", ref tmp, this, new Things(), TipoChamadaDigitar.Datatable);
                    break;
                case "Incluir":
                    cntItens1.oRet = Caller.ChamaForm("Form2_itens", ref tmp, this, new Things(), TipoChamadaDigitar.Datatable, new Things());
                    break;
            }
        }

        private void Form2_MyPosOK(object sender, EventArgs e)
        {
            FoxCursor tmp = cntItens1.MyCursor;
            tmp.ReplaceAllChanged("FK_TESTE", this.EE["PK_ID"]);
            cntItens1.MyGravarLinhas();
        }

        private void lblID_Click(object sender, EventArgs e)
        {

        }


   
    }
}
