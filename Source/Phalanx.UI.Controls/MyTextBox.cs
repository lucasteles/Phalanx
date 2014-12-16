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
using Phalanx.Common;

namespace Phalanx.UI.Controls
{
    public partial class MyTextBox : MaskedTextBox
    {

        public String controlSource { get; set; }
        public bool MyNoAutoResize { get; set; }
        public int MyQtdDecimais { get; set; }
        ToolTip msg = new ToolTip();
        
        protected Object _Value = null;
        public Object Value
        {
            get
            {
                if (_Value==null)
                    return null;

                if (Text.Equals(""))
                    return null;

                if (_Value.GetType() == typeof(int) || _Value.GetType() == typeof(Int64))
                {
                    _Value = int.Parse(Text);
                }

                if (_Value.GetType() == typeof(Decimal) || _Value.GetType() == typeof(double) || _Value.GetType() == typeof(float))
                {
                    if (Text.Substring(0,1) == "." )
                     Text = Text.Substring(1, Text.Length - 1);

                    _Value = Decimal.Parse(Text);
                }
                if (isDate(_Value) )
                {
                    DateTime aux =  new DateTime();
                    if (DateTime.TryParse(Text, out aux))
                            _Value = aux;


                }
                if (_Value.GetType() == typeof(string))
                {
                    _Value = Text;
                }

                
                return _Value;
            }

            set
            {

                if (value != null)
                {

                    if (isDate(value))
                        this.Mask = "00/00/0000";

                    if (!Text.Equals(value.ToString()))
                    
                        Text = value.ToString();
                }
                _Value = value;
                AutoFormat();
            }

        }


        public MyTextBox()
        {
            InitializeComponent();
        }

        public MyTextBox(IContainer container)
        {

            PrivateFontCollection pfc = new PrivateFontCollection();
            string fontFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6) + "\\FIXEDSYS.TTF";

            if (System.IO.File.Exists(fontFile)) { 
                pfc.AddFontFile(fontFile);

            this.Font = new Font(pfc.Families[0], 10, FontStyle.Bold);
            }
            container.Add(this);
         
            InitializeComponent();
        }

        private void MyTextBox_TextChanged(object sender, EventArgs e)
        {
            AutoFormat();
        }



        private void AutoFormat()
        {
            this.PromptChar = ' ';
            msg.IsBalloon = true;
            if (Value != null)
            {
                
                if (_Value.GetType() == typeof(int) || _Value.GetType() == typeof(Int64))
                {
                    this.Mask = "000000000";
                    
                    Text = Text.Trim();
                    //ValidatingType = typeof(System.Int32);
                    if (!Text.Equals(""))
                    {
                        Text = Text.Trim();

                    }
                    else
                    {
                        Int64 num;
                        Int64.TryParse(Text, out num);

                        if (num == 0) 
                             Text = "";
                    }
                   
                }

                if (_Value.GetType() == typeof(Decimal) || _Value.GetType() == typeof(double) || _Value.GetType() == typeof(float))
                {
                   // Decimal num = 0;
                   // Decimal.TryParse(Text, out num);
                   // Text = num.ToString("#,0.0#;(#,0.0#)");

                   // decimal myValue;
                   /* if (decimal.TryParse(Text, out myValue))
                    {
                      //  Text = myValue.ToString("#,0.0#;(#,0.0#)").Trim();
                      
                    } */

                   // Mask = "0,000,000.00";
                    //"#,0.00#;(#,0.00#)"

                }

                if (isDate(_Value))
                {
                    
                    ValidatingType = typeof(System.DateTime);

                }

               
            }
        }

        private bool isDate(object value)
        {
            return value.GetType() == typeof(DateTime) || (value == DBNull.Value && (this.controlSource.StartsWith("DT_") || this.controlSource.StartsWith("DH_")));

        }


        private void MyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
            msg.Hide(this);

            if (_Value.GetType() == typeof(Decimal) || _Value.GetType() == typeof(double) || _Value.GetType() == typeof(float))
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '-' && ((int)e.KeyChar) != 8)
                {

                    e.Handled = true;

                }

                if ((e.KeyChar == ',' && Text.IndexOf(',') > 0) || (e.KeyChar == '-' && Text.IndexOf('-') >= 0) || (e.KeyChar == '-' && this.SelectionStart!=0 && Text.IndexOf('-') < 0) )
                    e.Handled = true;

            }
        }

        private void MyTextBox_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {

           
            if (!e.IsValidInput) 
            {
                               
                if (isDate(_Value) )
                {

                    if (this.Text.Replace("/","").IsEmpty())
                        return;

                    msg.ToolTipTitle = "Data Invalida";
                    msg.UseFading = true;
                    
                    msg.Show("A data deve estar no formato dd/mm/yyyy.", this.Parent, this.Left+this.Width, this.Top+this.Height, 2000);
                    e.Cancel = true;
                }
            }
         
        }

        private void MyTextBox_Click(object sender, EventArgs e)
        {
            if (_Value.GetType() == typeof(int) || _Value.GetType() == typeof(Int64))
            {
                if (this.Text.Trim().Equals(string.Empty))
                 this.SelectionStart = 0;
                else
                    this.SelectionStart = Text.Trim().Length;
               
            }
        }

        private void MyTextBox_Validating(object sender, CancelEventArgs e)
        {
            Validation();
        }

        public void Validation()
        {
            if (_Value.GetType() == typeof(int) || _Value.GetType() == typeof(Int64))
            {
                Int64 num;
                Int64.TryParse(Text, out num);

                if (num == 0)
                    Text = "";

            }

            if (_Value.GetType() == typeof(Decimal) || _Value.GetType() == typeof(double) || _Value.GetType() == typeof(float))
            {
                Decimal num;
                Decimal.TryParse(Text, out num);

                if (MyQtdDecimais == 0)
                    MyQtdDecimais = 2;

                if (num == 0)
                    Text = "";
                else
                    Text = num.ToString("#,0." + (new String('0', MyQtdDecimais )) + "#;-#,0." + (new String('0', MyQtdDecimais )) + "#").Trim();

                if (Text.Contains(","))
                {
                    int qtdEfetiveDecimals =Text.Substring( Text.IndexOf(",")+1 ).Length;
                    if (qtdEfetiveDecimals > MyQtdDecimais)
                        Text=Text.Substring(0, Text.Length-(qtdEfetiveDecimals - MyQtdDecimais));

                }

            }       
         }

    
    }
}
