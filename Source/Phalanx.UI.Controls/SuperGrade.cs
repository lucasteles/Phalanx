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

    [Designer(typeof(System.Windows.Forms.Design.ControlDesigner))]
    public partial class SuperGrade : DataGridView
    {

        public event DataGridViewCellPaintingEventHandler MyCellPainting;
        public virtual void _MyCellPainting(Object sender, DataGridViewCellPaintingEventArgs e) { if (MyCellPainting != null)  MyCellPainting(sender, e); }


        #region "properties"

        
        public FoxCursor MyCursor { get; set; }
        public string MyQueryFile { get; set; }

        public SqlQuery QUERY = new SqlQuery();
        public Condition condicaoSQL = new Condition();
        public ContextMenuStrip menu = new ContextMenuStrip();
        private Things cellFormats;
        
        public string MyCondicaoString 
        {
            get { return condicaoSQL.Comando; }
            set { condicaoSQL.Comando = value;}
        }

        protected List<valorXcampo> _MyCondicaoCampos = new List<valorXcampo>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<valorXcampo> MyCondicaoCampos 
        {
            get 
            { 
                return _MyCondicaoCampos; 
            } 
           set 
             {
                 condicaoSQL = new Condition();
                 condicaoSQL.Comando = MyCondicaoString;
                 foreach (valorXcampo item in value)
                 {
                     condicaoSQL.Add(item.campo, item.valor);
                 }


               _MyCondicaoCampos = value; 
             } 
        }
        #endregion

        public SuperGrade()
        {
            InitializeComponent();
        }

        public SuperGrade(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void MyExecutar()
        {
            if (String.IsNullOrEmpty(MyQueryFile))
                return;

            //guarda query em propriedades para uso no consultar
            QUERY = new SqlQuery(MyQueryFile);

            MyCursor = db.PesquisaSQL(MyQueryFile, condicaoSQL);

            MyConfigurar();
        }


        public void MyConfigurar()
        {
            DataTable table = MyCursor.getDataTable();
            table.Columns.Add("DummyColumn");

            this.DataSource = MyCursor.getDataTable();
            this.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray; 

            GridHeaderLayoutGroup header = func.readXMLGridHeader(this.MyQueryFile);
            
            //EnableHeadersVisualStyles = false;

            cellFormats = new Things();
            bool setInvisible;
            foreach (DataGridViewColumn col in this.Columns)
            {
                setInvisible = true;
                foreach (GridHeaderLayout item in header.headers)
                {
                    if (item.nome == col.Name)
                    {
                        this.Columns[item.nome].HeaderText = item.descricao;
                        this.Columns[item.nome].Width = item.tamanho;
                        this.Columns[item.nome].DisplayIndex = header.headers.IndexOf(item);
                        if (!String.IsNullOrEmpty(item.mascara))
                            cellFormats[item.nome] = item.mascara;

                        setInvisible = false;

                    }

                }

                if (col.Name == "DummyColumn")
                {
                    this.Columns["DummyColumn"].DisplayIndex = this.Columns.Count - 1;
                    this.Columns["DummyColumn"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    this.Columns["DummyColumn"].HeaderText = "";
                    this.Columns["DummyColumn"].ReadOnly = true;
                    this.Columns["DummyColumn"].SortMode = DataGridViewColumnSortMode.Programmatic;
                    this.Columns["DummyColumn"].HeaderCell.Style.BackColor = Color.LightGray;

                    setInvisible = false;
                }

                if (setInvisible)
                    col.Visible = false;

            }

            
            
            // this.ContextMenuStrip = menu;
            /*
            var dumCol = new DataGridViewTextBoxColumn();
            dumCol.HeaderText = "";
            dumCol.Name = "DummyColumn";
            this.Columns.Add(dumCol);
            this.Columns["DummyColumn"].DisplayIndex = this.Columns.Count-1;
            this.Columns["DummyColumn"].Width = this.Width;
            this.Columns["DummyColumn"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            */
            this.RowHeadersWidth = 24;

        }

        public Things Scatter()
        {
            return MyCursor.ScatterName();
        }

        private void SuperGrade_SelectionChanged(object sender, EventArgs e)
        {
            if (this.CurrentCell != null)
                MyCursor.GoTo(this.CurrentCell.RowIndex);
        }

        private void SuperGrade_MouseClick(object sender, MouseEventArgs e)
        {
           // this.ContextMenuStrip = null; 
          }

        private void SuperGrade_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.ContextMenuStrip = null; 
            if (e.Button == MouseButtons.Right)
            {
                //MessageBox.Show(e.RowIndex.ToString());
                if (this.Rows.Count <= 0)
                    this.ContextMenuStrip = menu;

                if (e.RowIndex >= 0 && e.ColumnIndex >=0)
                {
                   
                    this.CurrentCell = this.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    
                    this.Rows[e.RowIndex].Selected = true;
                    this.Focus();

                    this.ContextMenuStrip = menu;
                    if (this.CurrentRow != null)
                        MyCursor.GoTo(this.CurrentRow.Index);
                }
            }
        }

        private void SuperGrade_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Rows.Count <= 0)
                this.ContextMenuStrip = menu;
            else
                this.ContextMenuStrip = null; 
        }

        public void RefreshPosition()
        {
   
            DataGridViewRowCollection rows = Rows;
            try
            {
                if (Rows.Count > 0)
                {
                    int nRecno = this.MyCursor.Recno();
                    this.CurrentCell = Rows[nRecno].Cells[0];
                    //Rows[this.MyCursor.Recno()].Selected = true;   
                    MyCursor.GoTo(nRecno);

                }
            }
            catch (Exception)
            {
                
                throw;
            }
            

        }

        private void SuperGrade_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (cellFormats.ContainsKey( this.Columns[e.ColumnIndex].Name ))
            {
                 if (e.Value.GetType() == typeof(Decimal) || e.Value.GetType() == typeof(double) || e.Value.GetType() == typeof(float))
                {
                    Decimal d = Decimal.Parse(e.Value.ToString());
                    e.Value = d.ToString(cellFormats[this.Columns[e.ColumnIndex].Name].ToString());

                }

            }
            
        }

        private void SuperGrade_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == this.Columns.Count - 1 && e.RowIndex >= 0 && this.Columns[e.ColumnIndex].Name=="DummyColumn")  
            {
                DataGridView dgv = sender as DataGridView;
                e.CellStyle.BackColor = Color.White;
            };


            if (e.ColumnIndex>=0 && e.RowIndex >=0)
             _MyCellPainting(this, e);
        }

        private void SuperGrade_Sorted(object sender, EventArgs e)
        {
            MyCursor.SetOrder(
            this.SortedColumn.DataPropertyName);
        }

    }
    
}
