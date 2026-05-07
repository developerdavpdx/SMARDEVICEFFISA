namespace FFISA.Ventas
{
    partial class ArticulosOV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArticulosOV));
            this.Footer = new System.Windows.Forms.Panel();
            this.Regresar = new System.Windows.Forms.PictureBox();
            this.MenuEntregas = new System.Windows.Forms.PictureBox();
            this.Continuar = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.lblOV = new System.Windows.Forms.Label();
            this.lblTotalOV = new System.Windows.Forms.Label();
            this.lbltotalOCtitle = new System.Windows.Forms.Label();
            this.LvArticulos = new System.Windows.Forms.ListView();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Footer.Controls.Add(this.Regresar);
            this.Footer.Controls.Add(this.MenuEntregas);
            this.Footer.Controls.Add(this.Continuar);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 290);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(240, 30);
            // 
            // Regresar
            // 
            this.Regresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Regresar.Image = ((System.Drawing.Image)(resources.GetObject("Regresar.Image")));
            this.Regresar.Location = new System.Drawing.Point(9, 3);
            this.Regresar.Name = "Regresar";
            this.Regresar.Size = new System.Drawing.Size(25, 25);
            this.Regresar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Regresar.Click += new System.EventHandler(this.Regresar_Click);
            // 
            // MenuEntregas
            // 
            this.MenuEntregas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.MenuEntregas.Image = ((System.Drawing.Image)(resources.GetObject("MenuEntregas.Image")));
            this.MenuEntregas.Location = new System.Drawing.Point(106, 1);
            this.MenuEntregas.Name = "MenuEntregas";
            this.MenuEntregas.Size = new System.Drawing.Size(29, 29);
            this.MenuEntregas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MenuEntregas.Click += new System.EventHandler(this.MenuEntregas_Click);
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
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Header.Controls.Add(this.label6);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(240, 30);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(9, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(224, 15);
            this.label6.Text = "LISTADO DE ARTÍCULOS";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.lblOV);
            this.Body.Controls.Add(this.lblTotalOV);
            this.Body.Controls.Add(this.lbltotalOCtitle);
            this.Body.Controls.Add(this.LvArticulos);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // lblOV
            // 
            this.lblOV.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblOV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(118)))), ((int)(((byte)(189)))));
            this.lblOV.Location = new System.Drawing.Point(9, 12);
            this.lblOV.Name = "lblOV";
            this.lblOV.Size = new System.Drawing.Size(224, 20);
            this.lblOV.Text = "OV: XXXX";
            // 
            // lblTotalOV
            // 
            this.lblTotalOV.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalOV.Location = new System.Drawing.Point(200, 229);
            this.lblTotalOV.Name = "lblTotalOV";
            this.lblTotalOV.Size = new System.Drawing.Size(34, 20);
            this.lblTotalOV.Text = "0";
            // 
            // lbltotalOCtitle
            // 
            this.lbltotalOCtitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbltotalOCtitle.ForeColor = System.Drawing.Color.Gray;
            this.lbltotalOCtitle.Location = new System.Drawing.Point(7, 229);
            this.lbltotalOCtitle.Name = "lbltotalOCtitle";
            this.lbltotalOCtitle.Size = new System.Drawing.Size(197, 20);
            this.lbltotalOCtitle.Text = "Total de artículos encontrados:";
            // 
            // LvArticulos
            // 
            this.LvArticulos.FullRowSelect = true;
            this.LvArticulos.Location = new System.Drawing.Point(8, 48);
            this.LvArticulos.Name = "LvArticulos";
            this.LvArticulos.Size = new System.Drawing.Size(225, 178);
            this.LvArticulos.TabIndex = 14;
            this.LvArticulos.View = System.Windows.Forms.View.Details;
            this.LvArticulos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LvArticulos_KeyPress);
            // 
            // ArticulosOV
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
            this.Name = "ArticulosOV";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Footer.ResumeLayout(false);
            this.Header.ResumeLayout(false);
            this.Body.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Footer;
        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.PictureBox Continuar;
        public System.Windows.Forms.PictureBox MenuEntregas;
        public System.Windows.Forms.PictureBox Regresar;
        private System.Windows.Forms.Panel Body;
        public System.Windows.Forms.Label lblOV;
        private System.Windows.Forms.Label lblTotalOV;
        private System.Windows.Forms.Label lbltotalOCtitle;
        private System.Windows.Forms.ListView LvArticulos;
    }
}