namespace Phalanx.UI.Controls
{
    partial class MyTextBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MyTextBox
            // 
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResetOnSpace = false;
            this.TypeValidationCompleted += new System.Windows.Forms.TypeValidationEventHandler(this.MyTextBox_TypeValidationCompleted);
            this.Click += new System.EventHandler(this.MyTextBox_Click);
            this.TextChanged += new System.EventHandler(this.MyTextBox_TextChanged);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextBox_KeyPress);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.MyTextBox_Validating);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
