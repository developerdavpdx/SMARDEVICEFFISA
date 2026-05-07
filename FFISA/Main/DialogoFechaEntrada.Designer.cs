using System.Windows.Forms;
using System.Drawing;
namespace FFISA.Main
{
    partial class DialogoFechaEntrada
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogoFechaEntrada));
            this.Footer = new System.Windows.Forms.Panel();
            this.Continuar = new System.Windows.Forms.PictureBox();
            this.Regresar = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Panel();
            this.LblTitleModule = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.PnlFechaContainer = new System.Windows.Forms.Panel();
            this.Preeliminar = new System.Windows.Forms.CheckBox();
            this.LblPreeliminar = new System.Windows.Forms.Label();
            this.lblDescriptionModule = new System.Windows.Forms.Label();
            this.TxtFechaEntrada = new System.Windows.Forms.DateTimePicker();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.Body.SuspendLayout();
            this.PnlFechaContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Footer.Controls.Add(this.Continuar);
            this.Footer.Controls.Add(this.Regresar);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 290);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(240, 30);
            // 
            // Continuar
            // 
            this.Continuar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Continuar.Image = ((System.Drawing.Image)(resources.GetObject("Continuar.Image")));
            this.Continuar.Location = new System.Drawing.Point(208, 3);
            this.Continuar.Name = "Continuar";
            this.Continuar.Size = new System.Drawing.Size(25, 25);
            this.Continuar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Continuar.Click += new System.EventHandler(this.Continuar_Click);
            // 
            // Regresar
            // 
            this.Regresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Regresar.Image = ((System.Drawing.Image)(resources.GetObject("Regresar.Image")));
            this.Regresar.Location = new System.Drawing.Point(8, 3);
            this.Regresar.Name = "Regresar";
            this.Regresar.Size = new System.Drawing.Size(25, 25);
            this.Regresar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Regresar.Click += new System.EventHandler(this.Regresar_Click);
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Header.Controls.Add(this.LblTitleModule);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(240, 30);
            // 
            // LblTitleModule
            // 
            this.LblTitleModule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.LblTitleModule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblTitleModule.ForeColor = System.Drawing.Color.White;
            this.LblTitleModule.Location = new System.Drawing.Point(8, 8);
            this.LblTitleModule.Name = "LblTitleModule";
            this.LblTitleModule.Size = new System.Drawing.Size(225, 15);
            this.LblTitleModule.Text = "FECHA ENTRADA";
            this.LblTitleModule.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.PnlFechaContainer);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // PnlFechaContainer
            // 
            this.PnlFechaContainer.BackColor = System.Drawing.Color.White;
            this.PnlFechaContainer.Controls.Add(this.Preeliminar);
            this.PnlFechaContainer.Controls.Add(this.LblPreeliminar);
            this.PnlFechaContainer.Controls.Add(this.lblDescriptionModule);
            this.PnlFechaContainer.Controls.Add(this.TxtFechaEntrada);
            this.PnlFechaContainer.Location = new System.Drawing.Point(3, 72);
            this.PnlFechaContainer.Name = "PnlFechaContainer";
            this.PnlFechaContainer.Size = new System.Drawing.Size(234, 117);
            // 
            // Preeliminar
            // 
            this.Preeliminar.Location = new System.Drawing.Point(186, 14);
            this.Preeliminar.Name = "Preeliminar";
            this.Preeliminar.Size = new System.Drawing.Size(22, 20);
            this.Preeliminar.TabIndex = 34;
            this.Preeliminar.Visible = false;
            // 
            // LblPreeliminar
            // 
            this.LblPreeliminar.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LblPreeliminar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(118)))), ((int)(((byte)(189)))));
            this.LblPreeliminar.Location = new System.Drawing.Point(13, 17);
            this.LblPreeliminar.Name = "LblPreeliminar";
            this.LblPreeliminar.Size = new System.Drawing.Size(173, 14);
            this.LblPreeliminar.Tag = "";
            this.LblPreeliminar.Text = "MARCAR COMO PREELIMINAR";
            this.LblPreeliminar.Visible = false;
            // 
            // lblDescriptionModule
            // 
            this.lblDescriptionModule.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblDescriptionModule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(118)))), ((int)(((byte)(189)))));
            this.lblDescriptionModule.Location = new System.Drawing.Point(13, 55);
            this.lblDescriptionModule.Name = "lblDescriptionModule";
            this.lblDescriptionModule.Size = new System.Drawing.Size(212, 17);
            this.lblDescriptionModule.Text = "SELECCIONE LA FECHA DE ENTRADA";
            // 
            // TxtFechaEntrada
            // 
            this.TxtFechaEntrada.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFechaEntrada.Location = new System.Drawing.Point(69, 79);
            this.TxtFechaEntrada.Name = "TxtFechaEntrada";
            this.TxtFechaEntrada.Size = new System.Drawing.Size(85, 22);
            this.TxtFechaEntrada.TabIndex = 24;
            this.TxtFechaEntrada.Value = new System.DateTime(2025, 2, 20, 0, 0, 0, 0);
            // 
            // DialogoFechaEntrada
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
            this.Name = "DialogoFechaEntrada";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Footer.ResumeLayout(false);
            this.Header.ResumeLayout(false);
            this.Body.ResumeLayout(false);
            this.PnlFechaContainer.ResumeLayout(false);
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
        public System.Windows.Forms.PictureBox Continuar;
        public System.Windows.Forms.PictureBox Regresar;
        private System.Windows.Forms.Panel Header;
        public Label LblTitleModule;
        private Panel Body;
        private Panel PnlFechaContainer;
        public CheckBox Preeliminar;
        public Label LblPreeliminar;
        public Label lblDescriptionModule;
        public DateTimePicker TxtFechaEntrada;
    }
}