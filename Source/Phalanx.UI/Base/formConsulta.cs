using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Phalanx.DataAccess;
using Phalanx.Common;
using Phalanx.UI.Base;
using System.Threading;
using Phalanx.UI.Controls;
namespace Phalanx.UI.Base
{

    public partial class formConsulta : form_base
    {

        #region "eventos"
        // definição do evendo
        public event EventHandler MyLoad;
        public event EventHandler MyPreDeletar;

        // chamadas de eventos
        public virtual void DoMyLoad(EventArgs e, Object o){ if (MyLoad != null)  MyLoad(o, e); }
        public virtual void DoMyPreDeletar(EventArgs e, Object o) { if (MyPreDeletar != null)  MyPreDeletar(o, e); }


        

        #endregion
        
        #region "propriedades"

        public string MyDigitar { get; set; }
        public bool MyAlterar { get; set; } 
        public bool MyInlcuir { get; set; }
        public bool MyExcluir { get; set; }
        public string MyDesativar { get; set; }
        public Things MyParameters { get; set;  }

        public bool CancelDelete { get; set; }
        public string CancelDeleteMessage { get; set; }
        
        #endregion

        private string ExportError="";

        #region "Contrutores"
        public formConsulta(): base()
        {
            InitializeComponent();
        }
        #endregion

        #region "Metodos"


        public override Object Execute(Things obj, ref  FoxCursor tmp, form_base Parent, Things parameters) 
        {
         
            this.Show();
            return MyRetorno;
        }

        public void MyExecutar()
        {
            this.GRADE.MyExecutar();
            statusStrip.Items["lblQtdRegistros"].Text =  this.GRADE.MyCursor.Reccount().ToString()+" Registros";

        }

        private void ContextMenuClick(Object sender, System.EventArgs e)
        {
            //form_base oform;
            object  oRet;
            FoxCursor tmp;
            tmp = GRADE.MyCursor;
            switch (sender.ToString().Trim())
            {
                case "Alterar":
                    
                   
                    oRet = Caller.ChamaForm(this.MyDigitar, ref tmp, this, MyParameters, TipoChamadaDigitar.Database);

                    if (oRet != null)
                        RefreshActionGrid((Things)oRet);  
                 
                    break;
                    

                case "Incluir":

                    oRet = Caller.ChamaForm(this.MyDigitar, ref tmp, this, MyParameters, TipoChamadaDigitar.Database, new Things());

                    if (oRet != null)
                         RefreshActionGrid((Things)oRet);


                    break;

                case "Excluir":

                    if (tmp.Reccount() <= 0)
                        return;

                    DoMyPreDeletar(e, GRADE.Scatter());
                    if (CancelDelete)
                    {
                        if (CancelDeleteMessage != String.Empty)
                            func.Mens(CancelDeleteMessage);

                        CancelDeleteMessage = "";
                        CancelDelete = false;

                        return;
                    }



                    if (func.SimOuNao("Deseja excluir o registro numero " + GRADE.MyCursor[RuntimeParameters.DefaultPrimaryKeyField].ToString()))
                    {
                        tmp.AtuSql('D', GRADE.QUERY.TABELA);
                        tmp.Remove();
                        GRADE.RefreshPosition();
                    }
                
                    break;

                case "Desativar":
                    MessageBox.Show("qwer");
                    break;

                case "Propriedades":

                    break;
            }
        }

        private void RefreshActionGrid(Things obj)
        {
            FoxCursor tmp = this.GRADE.MyCursor;

            Condition cond = new Condition();
            cond.Comando = RuntimeParameters.DefaultPrimaryKeyField + " = @" + RuntimeParameters.DefaultPrimaryKeyField;
            cond[RuntimeParameters.DefaultPrimaryKeyField] = obj[RuntimeParameters.DefaultPrimaryKeyField];
            FoxCursor updatedRec = db.PesquisaSQL(this.GRADE.MyQueryFile, cond);
            Things nRec = updatedRec.ScatterName();
            updatedRec.close();

               tmp.GoTop();
               try {

                   if (tmp.Locate(RuntimeParameters.DefaultPrimaryKeyField, (nRec)[RuntimeParameters.DefaultPrimaryKeyField]))
                        {
                            tmp.GatherName(nRec);
                        }
                        else
                        {
                            tmp.Insert(nRec);
                        }

                   }
	                catch (Exception)
	                {
		
	
	                }
               GRADE.RefreshPosition();
               
        }


       

        #endregion

        #region "Eventos do form"
        private void formConsulta_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

           // this.CenterToScreen();
            toolMenu.Items["btnInsert"].Visible = false;
            toolMenu.Items["btnAlterar"].Visible = false;
            toolMenu.Items["btnExcluir"].Visible = false;


            if (!String.IsNullOrEmpty(this.MyDesativar))
                GRADE.AddContextMenu("Desativar", ContextMenuClick);

            if (this.MyExcluir)
            {
                GRADE.AddContextMenu("Excluir", ContextMenuClick);

                toolMenu.Items["btnExcluir"].Visible = true;
            }

            if (!String.IsNullOrEmpty(this.MyDigitar))
            {
                if (MyInlcuir)
                {
                    
                    GRADE.AddContextMenu("Alterar", ContextMenuClick );

                    toolMenu.Items["btnAlterar"].Visible = true;
                }

                if (MyAlterar)
                {
                    
                    GRADE.AddContextMenu("Incluir", ContextMenuClick);

                    toolMenu.Items["btnInsert"].Visible = true;
                }
            }

           


            //starta combos
            foreach (Control item in this.Controls)
            {
                if (item.GetType()==typeof(MyComboBox))
                    ((MyComboBox)item).Start();
            }

            DoMyLoad(e,sender);
        }

   

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            GRADE.MyCursor.close();
            this.Close();
        }

        private void GRADE_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0 && e.ColumnIndex>=0)
                ContextMenuClick(GRADE.menu.Items[0], EventArgs.Empty);
        }
      
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();            
        }

        public void Refresh(Things atual = null)
        {
            
            if (atual == null)
                atual = GRADE.Scatter();

            this.GRADE.MyExecutar();

            FoxCursor tmp = this.GRADE.MyCursor;
            tmp.GoTop();
            tmp.Locate(RuntimeParameters.DefaultPrimaryKeyField, (atual)[RuntimeParameters.DefaultPrimaryKeyField]);
           
            GRADE.RefreshPosition();
            
        }

   
        private void btnInsert_Click(object sender, EventArgs e)
        {
            ContextMenuClick("Incluir", EventArgs.Empty);
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            ContextMenuClick("Alterar", EventArgs.Empty);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            ContextMenuClick("Excluir", EventArgs.Empty);
        }

        #region "XLS Export"

        private void splitExport_ButtonClick(object sender, EventArgs e)
        {
            xLSToolStripMenuItem_Click(sender, e);
        }

        private void xLSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = func.PutFile("Excel|*.xls");

            if (path.IsEmpty())
                return;

            DoEsperando( ExportXLSThread,"Gerando arquivo XLS", path);
           
        }

        private void ExportXLSThread(object path)
        {
            if(!GRADE.MyCursor.exportToXLS(path.ToString(), GRADE.MyQueryFile))
              ExportError = "Erro ao gerar arquivo XLS";
        }

   
        #endregion  

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = func.PutFile("PDF|*.pdf");

            if (path.IsEmpty())
                return;

            DoEsperando(ExportPDFThread, "Gerando arquivo PDF",path);
        }
        private void ExportPDFThread(object path)
        {

            if(!GRADE.MyCursor.exportToPDF(path.ToString(), GRADE.MyQueryFile))
                ExportError = "Erro ao gerar arquivo PDF";
        }

        private void tXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = func.PutFile("CSV|*.csv");

            if (path.IsEmpty())
                return;


            DoEsperando(ExportCSVThread, "Gerando arquivo CSV", path);
        }
        private void ExportCSVThread(object path)
        {

            if(!GRADE.MyCursor.exportToCSV(path.ToString(), GRADE.MyQueryFile))
                ExportError = "Erro ao gerar arquivo CSV";

        }

        private void formConsulta_MyPosEsperando(object sender, EventArgs e)
        {
            if (ExportError != "")
                func.Mens(ExportError);

            ExportError = "";
        }



    }

}
