namespace FFISA.Inventarios.RecuentoInventarios
{
    partial class DatosMaestrosRecuento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatosMaestrosRecuento));
            this.Footer = new System.Windows.Forms.Panel();
            this.MenuRecuentos = new System.Windows.Forms.PictureBox();
            this.PtbGuardar = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Panel();
            this.LblOrdenVenta = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.LblCantidadMetros = new System.Windows.Forms.Label();
            this.TxtCantidadMetrosCLNVAL = new System.Windows.Forms.TextBox();
            this.LblFolio = new System.Windows.Forms.Label();
            this.lblRecuento = new System.Windows.Forms.Label();
            this.LblTotalRecuento = new System.Windows.Forms.Label();
            this.LblCantidad = new System.Windows.Forms.Label();
            this.TxtCantidadCLNVAL = new System.Windows.Forms.TextBox();
            this.LblLote = new System.Windows.Forms.Label();
            this.TxtLoteCLNVAL = new System.Windows.Forms.TextBox();
            this.LblArticulo = new System.Windows.Forms.Label();
            this.LblAlmacen = new System.Windows.Forms.Label();
            this.TxtAlmacenCLNVAL = new System.Windows.Forms.TextBox();
            this.TxtArticuloCLNVAL = new System.Windows.Forms.TextBox();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Footer.Controls.Add(this.MenuRecuentos);
            this.Footer.Controls.Add(this.PtbGuardar);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 290);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(240, 30);
            // 
            // MenuRecuentos
            // 
            this.MenuRecuentos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.MenuRecuentos.Image = ((System.Drawing.Image)(resources.GetObject("MenuRecuentos.Image")));
            this.MenuRecuentos.Location = new System.Drawing.Point(4, 1);
            this.MenuRecuentos.Name = "MenuRecuentos";
            this.MenuRecuentos.Size = new System.Drawing.Size(29, 29);
            this.MenuRecuentos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MenuRecuentos.Click += new System.EventHandler(this.MenuRecuentos_Click);
            // 
            // PtbGuardar
            // 
            this.PtbGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.PtbGuardar.Image = ((System.Drawing.Image)(resources.GetObject("PtbGuardar.Image")));
            this.PtbGuardar.Location = new System.Drawing.Point(210, 3);
            this.PtbGuardar.Name = "PtbGuardar";
            this.PtbGuardar.Size = new System.Drawing.Size(25, 25);
            this.PtbGuardar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PtbGuardar.Click += new System.EventHandler(this.Guardar_Click);
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Header.Controls.Add(this.LblOrdenVenta);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(240, 30);
            // 
            // LblOrdenVenta
            // 
            this.LblOrdenVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.LblOrdenVenta.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblOrdenVenta.ForeColor = System.Drawing.Color.White;
            this.LblOrdenVenta.Location = new System.Drawing.Point(8, 8);
            this.LblOrdenVenta.Name = "LblOrdenVenta";
            this.LblOrdenVenta.Size = new System.Drawing.Size(195, 15);
            this.LblOrdenVenta.Text = "DATOS MAESTROS RECUENTO";
            this.LblOrdenVenta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.LblCantidadMetros);
            this.Body.Controls.Add(this.TxtCantidadMetrosCLNVAL);
            this.Body.Controls.Add(this.LblFolio);
            this.Body.Controls.Add(this.lblRecuento);
            this.Body.Controls.Add(this.LblTotalRecuento);
            this.Body.Controls.Add(this.LblCantidad);
            this.Body.Controls.Add(this.TxtCantidadCLNVAL);
            this.Body.Controls.Add(this.LblLote);
            this.Body.Controls.Add(this.TxtLoteCLNVAL);
            this.Body.Controls.Add(this.LblArticulo);
            this.Body.Controls.Add(this.LblAlmacen);
            this.Body.Controls.Add(this.TxtAlmacenCLNVAL);
            this.Body.Controls.Add(this.TxtArticuloCLNVAL);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // LblCantidadMetros
            // 
            this.LblCantidadMetros.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblCantidadMetros.ForeColor = System.Drawing.Color.Gray;
            this.LblCantidadMetros.Location = new System.Drawing.Point(5, 74);
            this.LblCantidadMetros.Name = "LblCantidadMetros";
            this.LblCantidadMetros.Size = new System.Drawing.Size(156, 15);
            this.LblCantidadMetros.Text = "Cantidad en metros";
            // 
            // TxtCantidadMetrosCLNVAL
            // 
            this.TxtCantidadMetrosCLNVAL.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtCantidadMetrosCLNVAL.Enabled = false;
            this.TxtCantidadMetrosCLNVAL.Location = new System.Drawing.Point(5, 92);
            this.TxtCantidadMetrosCLNVAL.Name = "TxtCantidadMetrosCLNVAL";
            this.TxtCantidadMetrosCLNVAL.Size = new System.Drawing.Size(231, 21);
            this.TxtCantidadMetrosCLNVAL.TabIndex = 95;
            // 
            // LblFolio
            // 
            this.LblFolio.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LblFolio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.LblFolio.Location = new System.Drawing.Point(124, 9);
            this.LblFolio.Name = "LblFolio";
            this.LblFolio.Size = new System.Drawing.Size(111, 17);
            this.LblFolio.Text = "Folio: XXXXXX";
            // 
            // lblRecuento
            // 
            this.lblRecuento.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblRecuento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.lblRecuento.Location = new System.Drawing.Point(4, 9);
            this.lblRecuento.Name = "lblRecuento";
            this.lblRecuento.Size = new System.Drawing.Size(114, 17);
            this.lblRecuento.Text = "Recuento: XXXXXX";
            // 
            // LblTotalRecuento
            // 
            this.LblTotalRecuento.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LblTotalRecuento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.LblTotalRecuento.Location = new System.Drawing.Point(4, 201);
            this.LblTotalRecuento.Name = "LblTotalRecuento";
            this.LblTotalRecuento.Size = new System.Drawing.Size(226, 53);
            this.LblTotalRecuento.Text = "No. rollos: 0";
            // 
            // LblCantidad
            // 
            this.LblCantidad.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblCantidad.ForeColor = System.Drawing.Color.Gray;
            this.LblCantidad.Location = new System.Drawing.Point(171, 33);
            this.LblCantidad.Name = "LblCantidad";
            this.LblCantidad.Size = new System.Drawing.Size(62, 14);
            this.LblCantidad.Tag = "";
            this.LblCantidad.Text = "Cantidad";
            // 
            // TxtCantidadCLNVAL
            // 
            this.TxtCantidadCLNVAL.BackColor = System.Drawing.Color.White;
            this.TxtCantidadCLNVAL.Location = new System.Drawing.Point(169, 50);
            this.TxtCantidadCLNVAL.Name = "TxtCantidadCLNVAL";
            this.TxtCantidadCLNVAL.Size = new System.Drawing.Size(64, 21);
            this.TxtCantidadCLNVAL.TabIndex = 90;
            this.TxtCantidadCLNVAL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCantidadCLNVAL_KeyPress);
            // 
            // LblLote
            // 
            this.LblLote.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblLote.ForeColor = System.Drawing.Color.Gray;
            this.LblLote.Location = new System.Drawing.Point(4, 33);
            this.LblLote.Name = "LblLote";
            this.LblLote.Size = new System.Drawing.Size(33, 14);
            this.LblLote.Text = "Lote";
            // 
            // TxtLoteCLNVAL
            // 
            this.TxtLoteCLNVAL.Location = new System.Drawing.Point(4, 50);
            this.TxtLoteCLNVAL.Name = "TxtLoteCLNVAL";
            this.TxtLoteCLNVAL.Size = new System.Drawing.Size(157, 21);
            this.TxtLoteCLNVAL.TabIndex = 89;
            this.TxtLoteCLNVAL.TextChanged += new System.EventHandler(this.TxtLoteCLNVAL_TextChanged);
            this.TxtLoteCLNVAL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtLote_KeyPress);
            // 
            // LblArticulo
            // 
            this.LblArticulo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblArticulo.ForeColor = System.Drawing.Color.Gray;
            this.LblArticulo.Location = new System.Drawing.Point(4, 115);
            this.LblArticulo.Name = "LblArticulo";
            this.LblArticulo.Size = new System.Drawing.Size(110, 15);
            this.LblArticulo.Text = "Artículo";
            // 
            // LblAlmacen
            // 
            this.LblAlmacen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblAlmacen.ForeColor = System.Drawing.Color.Gray;
            this.LblAlmacen.Location = new System.Drawing.Point(4, 156);
            this.LblAlmacen.Name = "LblAlmacen";
            this.LblAlmacen.Size = new System.Drawing.Size(114, 14);
            this.LblAlmacen.Text = "Almacén";
            // 
            // TxtAlmacenCLNVAL
            // 
            this.TxtAlmacenCLNVAL.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtAlmacenCLNVAL.Enabled = false;
            this.TxtAlmacenCLNVAL.Location = new System.Drawing.Point(4, 175);
            this.TxtAlmacenCLNVAL.Name = "TxtAlmacenCLNVAL";
            this.TxtAlmacenCLNVAL.Size = new System.Drawing.Size(231, 21);
            this.TxtAlmacenCLNVAL.TabIndex = 74;
            // 
            // TxtArticuloCLNVAL
            // 
            this.TxtArticuloCLNVAL.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtArticuloCLNVAL.Enabled = false;
            this.TxtArticuloCLNVAL.Location = new System.Drawing.Point(4, 133);
            this.TxtArticuloCLNVAL.Name = "TxtArticuloCLNVAL";
            this.TxtArticuloCLNVAL.Size = new System.Drawing.Size(231, 21);
            this.TxtArticuloCLNVAL.TabIndex = 75;
            // 
            // DatosMaestrosRecuento
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
            this.Name = "DatosMaestrosRecuento";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Footer.ResumeLayout(false);
            this.Header.ResumeLayout(false);
            this.Body.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Footer;
        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Label LblOrdenVenta;
        public System.Windows.Forms.PictureBox MenuRecuentos;
        private System.Windows.Forms.Panel Body;
        private System.Windows.Forms.Label LblArticulo;
        private System.Windows.Forms.Label LblAlmacen;
        private System.Windows.Forms.TextBox TxtAlmacenCLNVAL;
        private System.Windows.Forms.TextBox TxtArticuloCLNVAL;
        private System.Windows.Forms.Label LblCantidad;
        private System.Windows.Forms.TextBox TxtCantidadCLNVAL;
        private System.Windows.Forms.Label LblLote;
        private System.Windows.Forms.TextBox TxtLoteCLNVAL;
        public System.Windows.Forms.PictureBox PtbGuardar;
        public System.Windows.Forms.Label LblTotalRecuento;
        public System.Windows.Forms.Label lblRecuento;
        public System.Windows.Forms.Label LblFolio;
        private System.Windows.Forms.Label LblCantidadMetros;
        private System.Windows.Forms.TextBox TxtCantidadMetrosCLNVAL;
    }
}