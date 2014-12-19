using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Phalanx.DataAccess;
using Phalanx.UI.Base;
using Phalanx.UI.Controls;
using Phalanx.Common;

namespace Phalanx.UI
{
    public partial class SCREEN : Form
    {
        public MdiClient mdi;
        form_base frmDesk=null;

        public SCREEN()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            MdiClient ctlMDI = (MdiClient)this.Controls[this.Controls.Count - 1];
            ctlMDI.BackColor = Color.White;
            mdi = ctlMDI;

            LoadMenu();
            LoadDesktop();
            
            this.Refresh();

        }

        public void LoadMenu()
        {

            FoxCursor tmpModulos = new FoxCursor("SELECT * FROM TS_MODULOS ORDER BY NR_ORDEM");
            FoxCursor tmpTelas;
            int qtModulos = tmpModulos.Reccount();
            ToolStripMenuItem menu;
            ToolStripMenuItem items;
            ToolStripMenuItem subitems;
            Condition cond;
            

            menu = new ToolStripMenuItem("Modulos");


            while (tmpModulos.ScanEOF())
            {
                items = new ToolStripMenuItem();
                items.Name = "menu_id"+tmpModulos["PK_ID"].ToString();
                items.Tag = "";
                items.Text = tmpModulos["DS_MODULO"].ToString();
                //items.Click += new EventHandler(MenuItemClickHandler);

                cond = new Condition();
                cond.Comando = "FK_MODULO = @MODULO";
                cond["MODULO"] = (int) tmpModulos["PK_ID"];
                tmpTelas = new FoxCursor("SELECT * FROM TS_TELAS {COND} ORDER BY FK_COLUNA,NR_ORDEM", cond);
                while (tmpTelas.ScanEOF())
                {
                    subitems = new ToolStripMenuItem();
                    subitems.Name = "form_" + tmpTelas["DS_FORM"].ToString();
                    subitems.Tag = tmpTelas["DS_FORM"].ToString();
                    subitems.Text = tmpTelas["DS_TELA"].ToString();
                    subitems.Click += new EventHandler(RunForm);

                    items.DropDownItems.Add(subitems);
                }

                tmpTelas.close();

                menu.DropDownItems.Add(items);
                menuStrip.Items.Add(menu);

            }


            tmpModulos.close();
        }



    

        public void LoadDesktop()
        {
            TabControl tab = Desktop;
            TabPage tPage;
            MyMenuIcon ico;
            Label line;
            FoxCursor tmpModulos = new FoxCursor("SELECT * FROM TS_MODULOS ORDER BY NR_ORDEM");
            FoxCursor tmpTelas;
            Condition cond;



            while (tmpModulos.ScanEOF())
            {
                tPage = new TabPage();
                tPage.Tag = tmpModulos["PK_ID"].ToString();
                tPage.Text = tmpModulos["DS_MODULO"].ToString();
                tPage.BackColor = Color.White;



                Microsoft.VisualBasic.PowerPacks.ShapeContainer PageCaption = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
                Microsoft.VisualBasic.PowerPacks.RectangleShape PageShape = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
                PageCaption.Top = 2;
                PageCaption.Left = 2;
                PageCaption.Height = 25;
                PageCaption.Width = 100;
                 PageCaption.Anchor = Anchor = (AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top);


                PageShape.Top = 0;
                PageShape.Left = 0;
                PageShape.Height = PageCaption.Height;
                PageShape.Width = PageCaption.Width;
                PageShape.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
                PageShape.Anchor = Anchor = (AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top);
                PageShape.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                PageShape.CornerRadius = 2;                
                PageShape.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Horizontal;
                PageShape.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                PageShape.FillColor = Color.LightGray;
                PageShape.FillGradientColor = Color.White;
               
                PageCaption.Shapes.Add(PageShape);
                tPage.Controls.Add(PageCaption);
                 
                //Label do nome do modulo
                Label nome_modulo = new Label();

                nome_modulo.BackColor = Color.LightGray;
                nome_modulo.Text = tPage.Text;
                nome_modulo.AutoSize = true;
                nome_modulo.Top = 3;
                nome_modulo.Left = 10; 
                nome_modulo.Font = new System.Drawing.Font(new FontFamily("Microsoft Sans Serif"), 14, FontStyle.Bold);
                nome_modulo.Parent = PageCaption;
                tPage.Controls.Add(nome_modulo);
                nome_modulo.BringToFront();

                cond = new Condition();
                cond.Comando = "FK_MODULO = @MODULO";
                cond["MODULO"] = (int)tmpModulos["PK_ID"];
                tmpTelas = new FoxCursor("SELECT * FROM TS_TELAS {COND} ORDER BY FK_COLUNA,NR_ORDEM", cond);


                int default_indice = 33;
                int indice = default_indice;
                int col_indiex=-1;
                while (tmpTelas.ScanEOF())
                {

                    if (col_indiex == -1 || col_indiex != (int)tmpTelas["FK_COLUNA"])
                    {
                        indice = default_indice;
                        col_indiex = (int)tmpTelas["FK_COLUNA"];
                        line = new Label();
                        line.AutoSize = false;
                        line.BackColor = Color.FromArgb(50, 0, 0, 0);
                        line.Width = 1;
                        line.Height = tPage.Height;
                        line.Top = 40;
                        line.Left = 10 + (((int)tmpTelas["FK_COLUNA"]) - 1) * 100;
                        line.Anchor = Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top);
                        tPage.Controls.Add(line);
                    }


                    ico = new MyMenuIcon();
                    ico.MyText = tmpTelas["DS_TELA"].ToString();
                    ico.Tag = tmpTelas["DS_FORM"].ToString();
                    ico.SetIcon(ico.Tag + ".png");
                    ico.Run += RunForm;
                    ico.Top = indice;
                    ico.Left = 10 + (((int)tmpTelas["FK_COLUNA"]) - 1) * 100;
                    indice += ico.Height;
                    tPage.Controls.Add(ico);
                }

                              

                Desktop.TabPages.Add(tPage);

         
            }
            

            tab.Alignment = TabAlignment.Left;
            tab.DrawMode = TabDrawMode.OwnerDrawFixed;
            tab.DrawItem += tabControl_DrawItem;
            tab.SizeMode = TabSizeMode.Fixed;
            tab.ItemSize = new Size(25,100) ;
           
            this.Controls.Add(tab);


            //carrega form de desktop

            form_base frmDesktop = new form_base();
            frmDesktop.TopMost = false;
            frmDesktop.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frmDesktop.MdiParent = this;
            frmDesktop.Text = "Desktop";
            frmDesktop.Controls.Add(Desktop);
            //frmDesktop.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

            Desktop.Top = 0;
            this.Controls.Remove(Desktop);
            frmDesktop.Top = 0;
            frmDesktop.Left = 0;

            frmDesktop.Activated += DesktopActiveteCancel;

            frmDesktop.Show();
            
            frmDesktop.Width = this.Width;
            frmDesktop.Height = this.Height;

            frmDesk = frmDesktop;
        }

        private void DesktopActiveteCancel(Object Sender, EventArgs e)
        {
            frmDesk.SendToBack();
        }

        private void RunForm(object sender, EventArgs e)
        {
             String form="";

            if (sender.GetType() == typeof(MyMenuIcon))
            {
              MyMenuIcon clickedItem = (MyMenuIcon)sender;
              form = clickedItem.Tag.ToString();
            }

            if (sender.GetType() == typeof(ToolStripMenuItem))
            {
                ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
                form = clickedItem.Tag.ToString();
            }

            
            
            FoxCursor tmp = null;
            Things Parameters = new Things();

            Object oRet;
            form_base oform ; 
            oform =  (form_base)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance("Phalanx.UI." + form);
            if (oform == null)
            {
                func.Mens("Tela não cadastrada.");
            }
            else
            {
                oform.MdiParent = this;
                oRet = (oform).Execute((new Things()), ref tmp, (new form_base()), Parameters);
            }
        }

      

        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            
            /*
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = Desktop.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = Desktop.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Black);
                g.FillRectangle(Brushes.White, e.Bounds);
            }
            else
            {
                _textBrush = new SolidBrush(Color.White);//System.Drawing.SolidBrush(e.ForeColor);
                g.FillRectangle(Brushes.DarkGray, e.Bounds);
                //e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Arial", (float)10.0, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));


            */
             Graphics g;
             String sText;

             //int iX, iY;
             SizeF sizeText;

             TabControl ctlTab= Desktop;

             g = e.Graphics;
             Rectangle _tabBounds = Desktop.GetTabRect(e.Index);
            sText = ctlTab.TabPages[e.Index].Text;
            ctlTab.TabPages[e.Index].BackColor = Color.White;
            sizeText = g.MeasureString(sText, ctlTab.Font);

           // iX = e.Bounds.Left + 6;
            //iY = (int) (e.Bounds.Top + (e.Bounds.Height - sizeText.Height) / 2);

             StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;

            g.DrawString(sText, ctlTab.Font, Brushes.Black, _tabBounds, _stringFlags);
            
            
        }

        private void SCREEN_Resize(object sender, EventArgs e)
        {
            if (frmDesk != null)
            {
                frmDesk.Width = this.Width-30;
                frmDesk.Height = this.Height-90;
            }
            

        }

      
       
    }
}
;