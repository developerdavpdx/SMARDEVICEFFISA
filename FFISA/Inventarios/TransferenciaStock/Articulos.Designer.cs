namespace FFISA.Inventarios.TransferenciaStock
{
    partial class Articulos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Articulos));
            this.Footer = new System.Windows.Forms.Panel();
            this.MenuVentas = new System.Windows.Forms.PictureBox();
            this.Continuar = new System.Windows.Forms.PictureBox();
            this.Regresar = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Panel();
            this.Lbltitle = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.LblTotalPaginas = new System.Windows.Forms.Label();
            this.Anterior = new System.Windows.Forms.Button();
            this.Siguiente = new System.Windows.Forms.Button();
            this.TxtArticulo = new System.Windows.Forms.TextBox();
            this.lblTotalOV = new System.Windows.Forms.Label();
            this.lbltotalOVtitle = new System.Windows.Forms.Label();
            this.lblrangosfecha = new System.Windows.Forms.Label();
            this.LvArticulos = new System.Windows.Forms.ListView();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Footer.Controls.Add(this.MenuVentas);
            this.Footer.Controls.Add(this.Continuar);
            this.Footer.Controls.Add(this.Regresar);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 290);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(240, 30);
            // 
            // MenuVentas
            // 
            this.MenuVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.MenuVentas.Image = ((System.Drawing.Image)(resources.GetObject("MenuVentas.Image")));
            this.MenuVentas.Location = new System.Drawing.Point(106, 1);
            this.MenuVentas.Name = "MenuVentas";
            this.MenuVentas.Size = new System.Drawing.Size(29, 29);
            this.MenuVentas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MenuVentas.Click += new System.EventHandler(this.MenuEntradas_Click);
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
            this.Header.Controls.Add(this.Lbltitle);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(240, 30);
            // 
            // Lbltitle
            // 
            this.Lbltitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Lbltitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.Lbltitle.ForeColor = System.Drawing.Color.White;
            this.Lbltitle.Location = new System.Drawing.Point(7, 8);
            this.Lbltitle.Name = "Lbltitle";
            this.Lbltitle.Size = new System.Drawing.Size(228, 15);
            this.Lbltitle.Text = "LISTADO DE ARTÍCULOS";
            this.Lbltitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.LblTotalPaginas);
            this.Body.Controls.Add(this.Anterior);
            this.Body.Controls.Add(this.Siguiente);
            this.Body.Controls.Add(this.TxtArticulo);
            this.Body.Controls.Add(this.lblTotalOV);
            this.Body.Controls.Add(this.lbltotalOVtitle);
            this.Body.Controls.Add(this.lblrangosfecha);
            this.Body.Controls.Add(this.LvArticulos);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // LblTotalPaginas
            // 
            this.LblTotalPaginas.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.LblTotalPaginas.ForeColor = System.Drawing.Color.Gray;
            this.LblTotalPaginas.Location = new System.Drawing.Point(131, 189);
            this.LblTotalPaginas.Name = "LblTotalPaginas";
            this.LblTotalPaginas.Size = new System.Drawing.Size(104, 13);
            this.LblTotalPaginas.Text = "Página 0 de 0";
            this.LblTotalPaginas.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Anterior
            // 
            this.Anterior.BackColor = System.Drawing.Color.LightGray;
            this.Anterior.Enabled = false;
            this.Anterior.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.Anterior.ForeColor = System.Drawing.Color.White;
            this.Anterior.Location = new System.Drawing.Point(7, 189);
            this.Anterior.Name = "Anterior";
            this.Anterior.Size = new System.Drawing.Size(56, 21);
            this.Anterior.TabIndex = 38;
            this.Anterior.Tag = "";
            this.Anterior.Text = "Anterior";
            this.Anterior.Click += new System.EventHandler(this.Anterior_Click);
            // 
            // Siguiente
            // 
            this.Siguiente.BackColor = System.Drawing.Color.LightGray;
            this.Siguiente.Enabled = false;
            this.Siguiente.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.Siguiente.ForeColor = System.Drawing.Color.White;
            this.Siguiente.Location = new System.Drawing.Point(69, 189);
            this.Siguiente.Name = "Siguiente";
            this.Siguiente.Size = new System.Drawing.Size(56, 21);
            this.Siguiente.TabIndex = 37;
            this.Siguiente.Tag = "";
            this.Siguiente.Text = "Siguiente";
            this.Siguiente.Click += new System.EventHandler(this.Siguiente_Click);
            // 
            // TxtArticulo
            // 
            this.TxtArticulo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtArticulo.Location = new System.Drawing.Point(6, 29);
            this.TxtArticulo.Name = "TxtArticulo";
            this.TxtArticulo.Size = new System.Drawing.Size(104, 21);
            this.TxtArticulo.TabIndex = 36;
            this.TxtArticulo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtArticulo_KeyUp);
            // 
            // lblTotalOV
            // 
            this.lblTotalOV.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalOV.Location = new System.Drawing.Point(179, 236);
            this.lblTotalOV.Name = "lblTotalOV";
            this.lblTotalOV.Size = new System.Drawing.Size(54, 20);
            this.lblTotalOV.Text = "0";
            // 
            // lbltotalOVtitle
            // 
            this.lbltotalOVtitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbltotalOVtitle.ForeColor = System.Drawing.Color.Gray;
            this.lbltotalOVtitle.Location = new System.Drawing.Point(6, 236);
            this.lbltotalOVtitle.Name = "lbltotalOVtitle";
            this.lbltotalOVtitle.Size = new System.Drawing.Size(186, 20);
            this.lbltotalOVtitle.Text = "Total artículos encontrados:";
            // 
            // lblrangosfecha
            // 
            this.lblrangosfecha.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblrangosfecha.ForeColor = System.Drawing.Color.Gray;
            this.lblrangosfecha.Location = new System.Drawing.Point(7, 4);
            this.lblrangosfecha.Name = "lblrangosfecha";
            this.lblrangosfecha.Size = new System.Drawing.Size(57, 22);
            this.lblrangosfecha.Text = "Artículo";
            // 
            // LvArticulos
            // 
            this.LvArticulos.FullRowSelect = true;
            this.LvArticulos.Location = new System.Drawing.Point(6, 56);
            this.LvArticulos.Name = "LvArticulos";
            this.LvArticulos.Size = new System.Drawing.Size(229, 130);
            this.LvArticulos.TabIndex = 35;
            this.LvArticulos.View = System.Windows.Forms.View.Details;
            // 
            // Articulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.Body);
            this.Controls.Add(this.Footer);
            this.Controls.Add(this.Header);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Articulos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Footer.ResumeLayout(false);
            this.Header.ResumeLayout(false);
            this.Body.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Footer;
        public System.Windows.Forms.PictureBox Regresar;
        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Label Lbltitle;
        public System.Windows.Forms.PictureBox Continuar;
        public System.Windows.Forms.PictureBox MenuVentas;
        private System.Windows.Forms.Panel Body;
        private System.Windows.Forms.Label LblTotalPaginas;
        public System.Windows.Forms.Button Anterior;
        public System.Windows.Forms.Button Siguiente;
        private System.Windows.Forms.TextBox TxtArticulo;
        private System.Windows.Forms.Label lblTotalOV;
        private System.Windows.Forms.Label lbltotalOVtitle;
        private System.Windows.Forms.Label lblrangosfecha;
        private System.Windows.Forms.ListView LvArticulos;
    }
}