using System.Windows.Forms;
using System.Drawing;
namespace FFISA.Main
{
    partial class CustomMessage
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomMessage));
            this.Footer = new System.Windows.Forms.Panel();
            this.Continuar = new System.Windows.Forms.Button();
            this.Header = new System.Windows.Forms.Panel();
            this.RegresarENB = new System.Windows.Forms.PictureBox();
            this.LblTitleModule = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.TxtDescriptionModule = new System.Windows.Forms.TextBox();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Footer.Controls.Add(this.Continuar);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 290);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(240, 30);
            // 
            // Continuar
            // 
            this.Continuar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Continuar.ForeColor = System.Drawing.Color.Black;
            this.Continuar.Location = new System.Drawing.Point(204, 3);
            this.Continuar.Name = "Continuar";
            this.Continuar.Size = new System.Drawing.Size(33, 25);
            this.Continuar.TabIndex = 27;
            this.Continuar.Tag = "";
            this.Continuar.Text = "OK";
            this.Continuar.Click += new System.EventHandler(this.Continuar_Click);
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Header.Controls.Add(this.RegresarENB);
            this.Header.Controls.Add(this.LblTitleModule);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(240, 30);
            // 
            // RegresarENB
            // 
            this.RegresarENB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.RegresarENB.Image = ((System.Drawing.Image)(resources.GetObject("RegresarENB.Image")));
            this.RegresarENB.Location = new System.Drawing.Point(8, 3);
            this.RegresarENB.Name = "RegresarENB";
            this.RegresarENB.Size = new System.Drawing.Size(25, 25);
            this.RegresarENB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // LblTitleModule
            // 
            this.LblTitleModule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.LblTitleModule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblTitleModule.ForeColor = System.Drawing.Color.White;
            this.LblTitleModule.Location = new System.Drawing.Point(39, 8);
            this.LblTitleModule.Name = "LblTitleModule";
            this.LblTitleModule.Size = new System.Drawing.Size(194, 15);
            this.LblTitleModule.Text = "AVISO";
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.TxtDescriptionModule);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            this.Body.Paint += new System.Windows.Forms.PaintEventHandler(this.DibujarBorde);
            // 
            // TxtDescriptionModule
            // 
            this.TxtDescriptionModule.BackColor = System.Drawing.Color.White;
            this.TxtDescriptionModule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtDescriptionModule.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.TxtDescriptionModule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(118)))), ((int)(((byte)(189)))));
            this.TxtDescriptionModule.Location = new System.Drawing.Point(7, 9);
            this.TxtDescriptionModule.Multiline = true;
            this.TxtDescriptionModule.Name = "TxtDescriptionModule";
            this.TxtDescriptionModule.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtDescriptionModule.Size = new System.Drawing.Size(226, 242);
            this.TxtDescriptionModule.TabIndex = 1;
            // 
            // CustomMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.Body);
            this.Controls.Add(this.Footer);
            this.Controls.Add(this.Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "CustomMessage";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Footer.ResumeLayout(false);
            this.Header.ResumeLayout(false);
            this.Body.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void DibujarBorde(object sender, PaintEventArgs e)
        {
            Control c = (Control)sender;

            // ✅ (Opcional) Volver a pintar por si hay diferencias en el ClipRectangle
            using (SolidBrush fondo = new SolidBrush(c.BackColor))
            {
                e.Graphics.FillRectangle(fondo, e.ClipRectangle);
            }

            // 🟦 Dibuja el borde negro
            using (Pen pen = new Pen(Color.Black))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, c.Width - 1, c.Height - 1);
            }
        }


        #endregion

        private System.Windows.Forms.Panel Footer;
        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Panel Body;
        public Label LblTitleModule;
        private Button Continuar;
        public PictureBox RegresarENB;
        public TextBox TxtDescriptionModule;
    }
}