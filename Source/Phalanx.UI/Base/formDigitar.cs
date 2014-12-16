using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Phalanx.DataAccess;
using Phalanx.UI.Controls;
using Phalanx.Common;

namespace Phalanx.UI.Base
{
    public partial class formDigitar : form_base
    {
        #region "eventos"
        // definição do evendo
        public event EventHandler MyInit;
        public event EventHandler MyValidarOK;
        public event EventHandler MyPosOK;

        // chamadas de eventos
        public virtual void DoMyInit(EventArgs e, Object o) { if (MyInit != null)  MyInit(o, e); }
        public virtual void DoMyValidarOK(EventArgs e, Object o) { if (MyValidarOK != null)  MyValidarOK(o, e); }
        public virtual void DoMyPosOK(EventArgs e, Object o) { if (MyPosOK != null)  MyPosOK(o, e); }


        #endregion

        public bool CancelaOK = false;
        public String MyTabela { get; set; }
        public Things EE = new Things();
        public Things EE_OLD = new Things();
        public FoxCursor tmpgrade;
        public Things tmpdig;
        public Things MyParameters = new Things();
        public form_base MyParent;
        public TipoChamadaDigitar tipoChamada;

        public formDigitar()
        {
            InitializeComponent();
        }

 

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.MyRetorno = null;
            this.Close();
        }

        public override Object Execute(Things obj, ref  FoxCursor tmp, form_base pParent, Things parameters, TipoChamadaDigitar tipo)
        {
            
            MyParameters = parameters;
            tmpgrade = tmp;
            MyParent = pParent;
            tipoChamada = tipo;
            this.MyRetorno = null;

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;

            
            
            //seta posição na tela
            this.StartPosition = FormStartPosition.Manual;
            var myScreen = Screen.FromControl(pParent);
            this.Left = myScreen.WorkingArea.Left + (myScreen.WorkingArea.Width/2) - (this.Width/2);
            this.Top = myScreen.WorkingArea.Top + (myScreen.WorkingArea.Height / 2) - (this.Height / 2);
            //this.CenterToScreen(); // problemas em dual monitor
            

            //seta como nada retorno
            MyRetorno = null;

            //posiciona botoes
            btnOK.Left = (this.Width / 2) - (btnOK.Width + 12);
            btnOK.Top = this.Height - btnOK.Height - 50;

            btnCancelar.Left = (this.Width / 2) + 12;
            btnCancelar.Top = this.Height - btnCancelar.Height - 50;

            if (!obj.ContainsKey(RuntimeParameters.DefaultPrimaryKeyField) ||  typeof(Object) == obj[RuntimeParameters.DefaultPrimaryKeyField].GetType())
                obj = db.ScatterBlank(MyTabela);

            int id = (int)obj[RuntimeParameters.DefaultPrimaryKeyField];
            FoxCursor tmpEE = new FoxCursor("select * from " + this.MyTabela + " where " + RuntimeParameters.DefaultPrimaryKeyField + "=" + id.ToString());
            tmpEE.GoTop();


            if (tmpEE.Reccount() == 0 || tipoChamada == TipoChamadaDigitar.Datatable)
                EE = obj;
            else
                EE = tmpEE.ScatterName();

           
            EE_OLD = EE.Copy();

            //busca estrutura
            MyTableSchema tabela = db.GetSchema(MyTabela);
            int tamanho;

            //ajusta textbox
            foreach (Control item in this.Controls)
            {
                if (typeof(MyTextBox) == item.GetType())
                {
                    MyTextBox newItem = (MyTextBox)item;

                    if (newItem.controlSource.IsEmpty())
                        continue;

                    tamanho = tabela[newItem.controlSource].size;
                    newItem.MaxLength = tamanho;

                    if (!newItem.MyNoAutoResize)
                    {
                        if (typeof(DateTime) == EE[newItem.controlSource].GetType() || (EE[newItem.controlSource] == DBNull.Value && (newItem.controlSource.StartsWith("DT_") || newItem.controlSource.StartsWith("DH_"))))
                            newItem.Width = 20 * 5 - 12;
                        else
                            newItem.Width = tamanho * 5;

                    }
                    newItem.Value = EE[newItem.controlSource];
                    newItem.Validation();
                }

                if (typeof(MyComboBox) == item.GetType())
                {
                    MyComboBox newItem = (MyComboBox)item;

                    if (newItem.controlSource.IsEmpty())
                        continue;

                    if (!EE[newItem.controlSource].IsEmpty()) { 
                        newItem.Value = EE[newItem.controlSource];
                       
                    }
                }


            }

            //starta combos
            foreach (Control item in this.Controls)
            {
                if (typeof(MyComboBox)==item.GetType())
                    ((MyComboBox)item).Start();
            }


            DoMyInit(EventArgs.Empty, this);

            this.lblID.Left = 10;
            this.lblID.Top = this.btnOK.Top+20;
            this.lblID.Text = "ID: " + EE[RuntimeParameters.DefaultPrimaryKeyField].ToString();

            this.ShowDialog();
            return MyRetorno;
        }

     
        private void btnOK_Click(object sender, EventArgs e)
        {
            DoMyValidarOK(e, this);
            if (CancelaOK)
                return;


            updateEE();            

            if (tipoChamada == TipoChamadaDigitar.Database)
            {

                char Action = ' ';
                if (EE[RuntimeParameters.DefaultPrimaryKeyField].GetType() == typeof(int))
                {
                    int nID = 0;
                    int.TryParse(EE[RuntimeParameters.DefaultPrimaryKeyField].ToString(), out nID);

                    if (nID == 0)
                        Action = 'A';

                    if (nID != 0)
                        Action = 'M';
                }
                if (EE[RuntimeParameters.DefaultPrimaryKeyField].GetType() == typeof(string))
                {
                    string cID = "";
                    cID = EE[RuntimeParameters.DefaultPrimaryKeyField].ToString();

                    if (cID.Trim() == "")
                        Action = 'A';

                    if (cID.Trim() != "")
                        Action = 'M';
                }

                                
                db.AtuSql(Action, MyTabela, ref EE);



            }
            this.MyRetorno = EE;

             DoMyPosOK(e, this);

             this.Close();

        }

        private void formDigitar_Resize(object sender, EventArgs e)
        {
            this.lblID.Left = 10;
            this.lblID.Top = this.btnOK.Top + 20;
        }

        private void updateEE()
        {
            foreach (Control item in this.Controls)
            {
                if (typeof(MyTextBox) == item.GetType())
                {
                    MyTextBox txt = (MyTextBox)item;

                    if (txt.controlSource == "")
                        continue;

                    EE[txt.controlSource] = txt.Value;
                }
                if (typeof(MyComboBox) == item.GetType())
                {
                    MyComboBox cmb = (MyComboBox)item;
                    if ( cmb.controlSource.IsEmpty() )
                        continue;

                    EE[cmb.controlSource] = cmb.Value;
                }

            }

        }

        private void formDigitar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.MyRetorno == null)
            {
                updateEE();
                if (!EE.Compare(EE_OLD))
                {
                    if (!func.SimOuNao("Sair sem salvar?"))
                        e.Cancel=true;
                }

            }
        }

     
     


    }
}
