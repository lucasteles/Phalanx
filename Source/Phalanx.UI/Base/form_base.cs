using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Phalanx.Common;
using Phalanx.DataAccess;
using Phalanx.UI.Controls;

namespace Phalanx.UI.Base
{
    public partial class form_base : Form
    {


        public Esperando myEsperandoContainer;
        public Object MyRetorno { get; set; }
        public string esperandoMessage { get; set; }


        public event EventHandler MyPosEsperando;
        public virtual void DoMyPosEsperando(EventArgs e, Object o) { if (MyPosEsperando != null)  MyPosEsperando(o, e); }
    
        protected Panel __BLACKPRINT;

        private object ThreadParam;

        public form_base()
        {
            InitializeComponent();

        }


        public virtual Object Execute(Things obj, ref FoxCursor tmp, form_base Parent, Things parameters)
        {

            this.Show();

            return MyRetorno;
        }

          public virtual Object Execute(Things obj, ref FoxCursor tmp, form_base Parent, Things parameters, TipoChamadaDigitar tipo)
            {

                this.Show();

                return MyRetorno;
            }


          public void DoEsperando(Action func, string Message)
          {
              esperandoMessage = Message;

              var myThreadStart = new ParameterizedThreadStart(ThreadCaller);
              var myThread = new Thread(myThreadStart);
              myThread.Start(func);

          }

          public void DoEsperando(Action<object> func, string Message, object Parameter)
          {
              esperandoMessage = Message;
              ThreadParam = Parameter;
              var myThreadStart = new ParameterizedThreadStart(ParameterizedThreadCaller);
              var myThread = new Thread(myThreadStart);
              myThread.Start(func);

          }

          private void ThreadCaller(object func)
          {

              this.InvokeEx(() => DoEsperando(esperandoMessage));

              ((Action)func)();

             
              this.InvokeEx(FimEsperando);

              
          }

          private void ParameterizedThreadCaller(object func)
          {

              this.InvokeEx(() => DoEsperando(esperandoMessage));

              ((Action<object>)func)(ThreadParam);

             
              this.InvokeEx(FimEsperando);
              ThreadParam = null;

              
          }


          public void DoEsperando(string desc)
          {
              
              myEsperandoContainer = new Esperando(desc);
              myEsperandoContainer.Top = this.Height/2 -  myEsperandoContainer.Height / 2 - 50;
              myEsperandoContainer.Left = this.Width / 2 - myEsperandoContainer.Width / 2;

             
              Panel blackP = new Panel();
              __BLACKPRINT = blackP;
              blackP.Top = 0;
              blackP.Left = 0;
              blackP.Height = this.Height;
              blackP.Width = this.Width;
              blackP.BackColor = Color.FromArgb(25, Color.Black);

         

              Bitmap bmp = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
              Graphics g = Graphics.FromImage(bmp);
              g.CopyFromScreen(PointToScreen(ClientRectangle.Location).X, PointToScreen(ClientRectangle.Location).Y, 0, 0, ClientRectangle.Size);
              
              SetBrightness(-100, ref bmp);

              blackP.BackgroundImage = bmp;

              this.Controls.Add(blackP);
              blackP.BringToFront();
              this.Controls.Add(myEsperandoContainer);
              myEsperandoContainer.BringToFront();


             
              
          }
          public void FimEsperando()
          {
              this.Controls.Remove(myEsperandoContainer);
              myEsperandoContainer.Dispose();
              __BLACKPRINT.Dispose();
              DoMyPosEsperando(EventArgs.Empty, this);
          }

          private void SetBrightness(int brightness,ref Bitmap _currentBitmap)
          {
              Bitmap temp = (Bitmap)_currentBitmap;
              Bitmap bmap = (Bitmap)temp.Clone();
              if (brightness < -255) brightness = -255;
              if (brightness > 255) brightness = 255;
              Color c;
              for (int i = 0; i < bmap.Width; i++)
              {
                  for (int j = 0; j < bmap.Height; j++)
                  {
                      c = bmap.GetPixel(i, j);
                      int cR = c.R + brightness;
                      int cG = c.G + brightness;
                      int cB = c.B + brightness;

                      if (cR < 0) cR = 1;
                      if (cR > 255) cR = 255;

                      if (cG < 0) cG = 1;
                      if (cG > 255) cG = 255;

                      if (cB < 0) cB = 1;
                      if (cB > 255) cB = 255;

                      bmap.SetPixel(i, j,
          Color.FromArgb((byte)cR, (byte)cG, (byte)cB));
                  }
              }
              _currentBitmap = (Bitmap)bmap.Clone();
          }
    }
}
