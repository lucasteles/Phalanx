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
    public partial class CntItens : UserControl
    {

        #region "eventos"
        // definição do evendo
        public event EventHandler MyCallForm;

        // chamadas de eventos
        public virtual void DoMyCallForm(EventArgs e, Object o) { if (MyCallForm != null)  MyCallForm(o, e); }

        #endregion

        #region "propriedades"

        public string MyQueryFile { get; set; }
        public bool MyAlterar { get; set; }
        public bool MyInlcuir { get; set; }
        public bool MyExcluir { get; set; }
        public Things MyParameters { get; set; }
        public Condition MyCondicao = new Condition();
        public object oRet = null;
        public FoxCursor MyCursor
        { get{return GRADE.MyCursor; }
            set { GRADE.MyCursor = value; }
        }
        public ContextMenuStrip menu
        {
            get { return GRADE.menu; }
            set { GRADE.menu = value; }
        }

        #endregion
        
        public CntItens()
        {
            InitializeComponent();
        }

        private void CntItens_Resize(object sender, EventArgs e)
        {
            GRADE.Size = this.Size;
        }

        private void CntItens_Load(object sender, EventArgs e)
        {
            GRADE.Size = this.Size;
        }


        public void MyAmbientar()
        {
            GRADE.MyQueryFile = this.MyQueryFile;
            GRADE.condicaoSQL = MyCondicao;
            GRADE.MyExecutar();

            Image img = null;
          
                if (MyInlcuir)
                    GRADE.menu.Items.Add("Alterar", img, new System.EventHandler(ContextMenuClick));

                if (MyAlterar)
                    GRADE.menu.Items.Add("Incluir", img, new System.EventHandler(ContextMenuClick));
            

            if (this.MyExcluir)
                GRADE.menu.Items.Add("Remover", img, new System.EventHandler(ContextMenuClick));

       

        }
        private void ContextMenuClick(Object sender, System.EventArgs e)
        {
            char action = ' ';
            FoxCursor tmp;
            switch (sender.ToString().Trim())
            {
                case "Alterar":
                    action = 'M';
                    CallForm(sender);
                    break;


                case "Incluir":
                    action = 'A';
                    CallForm(sender);
                    break;


                case "Remover":
                    tmp = GRADE.MyCursor;
                    if (tmp.Reccount() <= 0)
                        return;

                    tmp.Remove();
                    GRADE.RefreshPosition();


                    break;

                case "Desativar":
                    MessageBox.Show("qwer");
                    break;

                case "Propriedades":

                    break;
            }

            if (oRet!=null)
                RefreshActionGrid((Things)oRet, action);

            oRet = null;

        }

        private void CallForm(object sender)
        {
            DoMyCallForm(EventArgs.Empty, sender );
        }

        private void RefreshActionGrid(Things obj, char action)
        {
            FoxCursor tmp = this.GRADE.MyCursor;

            if (action == 'A' || (action == 'M' && tmp.Reccount()==0) )
                tmp.Insert(obj);
            else if (action == 'M')
                tmp.GatherName(obj);

            /*
            tmp.GoTop();
            try
            {

                if (tmp.Locate(RuntimeParameters.DefaultPrimaryKeyField, (obj)[RuntimeParameters.DefaultPrimaryKeyField]))
                {
                 //   tmp.GatherName(obj);
                }
                else
                {
                    tmp.Insert(obj);
                }

            }
            catch (Exception)
            {


            } */
            GRADE.RefreshPosition();

        }

        public void MyGravarLinhas()
        {

            GRADE.MyCursor.ScanAtuSql(GRADE.QUERY.TABELA, "", true);
            
        }

        private void GRADE_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex>=0 && e.RowIndex>=0)
                ContextMenuClick(this.GRADE.menu.Items[0], EventArgs.Empty);
        }

      

    }
}
