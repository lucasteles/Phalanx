using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Globalization;
using Phalanx.DataAccess;
using Phalanx.Common;

namespace Phalanx.UI.Controls
{
    public partial class MyComboBox : ComboBox
    {
        private object _Value;
        public Object Value 
        { 
            get {

                return this.SelectedValue ; 
            
            } 
            set {

                _Value = value;
                SelectedValue = _Value;
                
                
            } 
        }

        public enum TipoDataFIll
        {
            None,
            Query,
            QueryFile,
            Cursor,
            ComboTable
        }

        public bool MyIsF4 { get; set; }
        public Condition condicaoSQL = new Condition();
        public TipoDataFIll myTipoDataSource { get; set; }
        public String controlSource { get; set; }
        public string myDataSource { get; set; }
        public int MyColumnOrder { get; set; }
        public bool myKeyAndValue  { get; set; } 
        private Dictionary<object, object> combo = new Dictionary<object, object>();
        private FoxCursor _MyCursor; 
        public FoxCursor MyCursor { 
           get { return _MyCursor;}
            set { _MyCursor = value;  }
        }

        public string MyCondicaoString
        {
            get { return condicaoSQL.Comando; }
            set { condicaoSQL.Comando = value; }
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

        public MyComboBox()
        {
            InitializeComponent();
            myKeyAndValue = false;
            MyColumnOrder = -1;
            myTipoDataSource = TipoDataFIll.None;
        }

        public MyComboBox(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            
            

        }

        public void Start()
        {

            switch(myTipoDataSource)
            {

                case TipoDataFIll.None:
                    return;

                case TipoDataFIll.ComboTable:

                    string cSQL = "SELECT CD_ITEM,DS_ITEM FROM TS_COMBOITENS WHERE TG_INATIVO=0 AND DS_TABELA='" + myDataSource+"'";
                    _MyCursor = new FoxCursor(cSQL);

                    break;      

                case TipoDataFIll.Cursor:
                      return;
                    

                case TipoDataFIll.Query:
                    _MyCursor = new FoxCursor(myDataSource);
                    break;
                case TipoDataFIll.QueryFile:
                    _MyCursor = db.PesquisaSQL(myDataSource, condicaoSQL);
                    break;

            }



            if (MyColumnOrder!= -1)
                _MyCursor.SetOrder(MyColumnOrder);

            combo.Add(DBNull.Value, " ");

            _MyCursor.GoTop();
            while(_MyCursor.ScanEOF())
            {
                if (myKeyAndValue)
                    combo.Add(MyCursor[0], MyCursor[0].ToString().Trim().PadRight(3,' ') + " - " + MyCursor[1].ToString().Trim().PadRight(50,' '));
                else
                    combo.Add(MyCursor[0], MyCursor[1]);
            }


            DataSource = new BindingSource(combo, null);
            DisplayMember = "value";
            ValueMember = "key";

            if (!_Value.IsEmpty())
            {
                SelectedValue = _Value;
                var ee = combo.Where(e => e.Key.ToString() == _Value.ToString()).ToDictionary(e => e.Key, e => e.Value);
                if (!ee.IsEmpty())
                    SelectedItem = ee.First(); ;
            }
           // PrivateFontCollection pfc = new PrivateFontCollection();
            //pfc.AddFontFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6) + "\\FIXEDSYS.TTF");
            //this.Font = new Font(pfc.Families[0], 12, FontStyle.Bold);
            this.Font = new Font(this.Font.FontFamily, 12, FontStyle.Bold); 
            

        }   


    }
}
