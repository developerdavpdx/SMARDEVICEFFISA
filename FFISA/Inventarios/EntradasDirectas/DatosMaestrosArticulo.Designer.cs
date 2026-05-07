namespace FFISA.Inventarios.EntradasDirectas
{
    partial class DatosMaestrosArticulo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatosMaestrosArticulo));
            this.Footer = new System.Windows.Forms.Panel();
            this.Regresar = new System.Windows.Forms.PictureBox();
            this.AñadirArticulo = new System.Windows.Forms.Button();
            this.Guardar = new System.Windows.Forms.Button();
            this.Header = new System.Windows.Forms.Panel();
            this.MenuVentas = new System.Windows.Forms.PictureBox();
            this.LblOrdenVenta = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.ModificarCuentaContable = new System.Windows.Forms.PictureBox();
            this.ValidarAlmacen = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCuentaContableVAL = new System.Windows.Forms.TextBox();
            this.LblComentarios = new System.Windows.Forms.Label();
            this.TxtComentariosCLN = new System.Windows.Forms.TextBox();
            this.LblCantidadMetros = new System.Windows.Forms.Label();
            this.TxtCantidadMetrosCLNVAL = new System.Windows.Forms.TextBox();
            this.LblCantidad = new System.Windows.Forms.Label();
            this.TxtCantidadCLNVAL = new System.Windows.Forms.TextBox();
            this.LblLote = new System.Windows.Forms.Label();
            this.TxtLoteCLN = new System.Windows.Forms.TextBox();
            this.LblArticulo = new System.Windows.Forms.Label();
            this.LblAlmacen = new System.Windows.Forms.Label();
            this.TxtAlmacenVAL = new System.Windows.Forms.TextBox();
            this.TxtArticulo = new System.Windows.Forms.TextBox();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Footer.Controls.Add(this.Regresar);
            this.Footer.Controls.Add(this.AñadirArticulo);
            this.Footer.Controls.Add(this.Guardar);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 290);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(240, 30);
            // 
            // Regresar
            // 
            this.Regresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Regresar.Image = ((System.Drawing.Image)(resources.GetObject("Regresar.Image")));
            this.Regresar.Location = new System.Drawing.Point(4, 3);
            this.Regresar.Name = "Regresar";
            this.Regresar.Size = new System.Drawing.Size(25, 25);
            this.Regresar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Regresar.Click += new System.EventHandler(this.Regresar_Click);
            // 
            // AñadirArticulo
            // 
            this.AñadirArticulo.BackColor = System.Drawing.Color.SteelBlue;
            this.AñadirArticulo.ForeColor = System.Drawing.Color.White;
            this.AñadirArticulo.Location = new System.Drawing.Point(138, 2);
            this.AñadirArticulo.Name = "AñadirArticulo";
            this.AñadirArticulo.Size = new System.Drawing.Size(98, 25);
            this.AñadirArticulo.TabIndex = 26;
            this.AñadirArticulo.Tag = "";
            this.AñadirArticulo.Text = "Añadir Artículo";
            this.AñadirArticulo.Click += new System.EventHandler(this.AñadirArticulo_Click);
            // 
            // Guardar
            // 
            this.Guardar.BackColor = System.Drawing.Color.SteelBlue;
            this.Guardar.ForeColor = System.Drawing.Color.White;
            this.Guardar.Location = new System.Drawing.Point(73, 2);
            this.Guardar.Name = "Guardar";
            this.Guardar.Size = new System.Drawing.Size(59, 25);
            this.Guardar.TabIndex = 27;
            this.Guardar.Tag = "";
            this.Guardar.Text = "Guardar";
            this.Guardar.Click += new System.EventHandler(this.Guardar_Click);
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Header.Controls.Add(this.MenuVentas);
            this.Header.Controls.Add(this.LblOrdenVenta);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(240, 30);
            // 
            // MenuVentas
            // 
            this.MenuVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.MenuVentas.Image = ((System.Drawing.Image)(resources.GetObject("MenuVentas.Image")));
            this.MenuVentas.Location = new System.Drawing.Point(206, 0);
            this.MenuVentas.Name = "MenuVentas";
            this.MenuVentas.Size = new System.Drawing.Size(29, 29);
            this.MenuVentas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MenuVentas.Click += new System.EventHandler(this.MenuEntradas_Click);
            // 
            // LblOrdenVenta
            // 
            this.LblOrdenVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.LblOrdenVenta.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblOrdenVenta.ForeColor = System.Drawing.Color.White;
            this.LblOrdenVenta.Location = new System.Drawing.Point(8, 8);
            this.LblOrdenVenta.Name = "LblOrdenVenta";
            this.LblOrdenVenta.Size = new System.Drawing.Size(195, 15);
            this.LblOrdenVenta.Text = "DATOS MAESTROS ARTÍCULO";
            this.LblOrdenVenta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.ModificarCuentaContable);
            this.Body.Controls.Add(this.ValidarAlmacen);
            this.Body.Controls.Add(this.label1);
            this.Body.Controls.Add(this.TxtCuentaContableVAL);
            this.Body.Controls.Add(this.LblComentarios);
            this.Body.Controls.Add(this.TxtComentariosCLN);
            this.Body.Controls.Add(this.LblCantidadMetros);
            this.Body.Controls.Add(this.TxtCantidadMetrosCLNVAL);
            this.Body.Controls.Add(this.LblCantidad);
            this.Body.Controls.Add(this.TxtCantidadCLNVAL);
            this.Body.Controls.Add(this.LblLote);
            this.Body.Controls.Add(this.TxtLoteCLN);
            this.Body.Controls.Add(this.LblArticulo);
            this.Body.Controls.Add(this.LblAlmacen);
            this.Body.Controls.Add(this.TxtAlmacenVAL);
            this.Body.Controls.Add(this.TxtArticulo);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // ModificarCuentaContable
            // 
            this.ModificarCuentaContable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.ModificarCuentaContable.Image = ((System.Drawing.Image)(resources.GetObject("ModificarCuentaContable.Image")));
            this.ModificarCuentaContable.Location = new System.Drawing.Point(210, 229);
            this.ModificarCuentaContable.Name = "ModificarCuentaContable";
            this.ModificarCuentaContable.Size = new System.Drawing.Size(25, 25);
            this.ModificarCuentaContable.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ModificarCuentaContable.Click += new System.EventHandler(this.ModificarCuentaContable_Click);
            // 
            // ValidarAlmacen
            // 
            this.ValidarAlmacen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.ValidarAlmacen.Image = ((System.Drawing.Image)(resources.GetObject("ValidarAlmacen.Image")));
            this.ValidarAlmacen.Location = new System.Drawing.Point(210, 61);
            this.ValidarAlmacen.Name = "ValidarAlmacen";
            this.ValidarAlmacen.Size = new System.Drawing.Size(25, 25);
            this.ValidarAlmacen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ValidarAlmacen.Click += new System.EventHandler(this.ValidarAlmacen_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(6, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 14);
            this.label1.Tag = "";
            this.label1.Text = "Cuenta Contable";
            // 
            // TxtCuentaContableVAL
            // 
            this.TxtCuentaContableVAL.BackColor = System.Drawing.Color.White;
            this.TxtCuentaContableVAL.Enabled = false;
            this.TxtCuentaContableVAL.Location = new System.Drawing.Point(6, 229);
            this.TxtCuentaContableVAL.Name = "TxtCuentaContableVAL";
            this.TxtCuentaContableVAL.Size = new System.Drawing.Size(194, 21);
            this.TxtCuentaContableVAL.TabIndex = 80;
            // 
            // LblComentarios
            // 
            this.LblComentarios.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblComentarios.ForeColor = System.Drawing.Color.Gray;
            this.LblComentarios.Location = new System.Drawing.Point(6, 173);
            this.LblComentarios.Name = "LblComentarios";
            this.LblComentarios.Size = new System.Drawing.Size(131, 14);
            this.LblComentarios.Tag = "";
            this.LblComentarios.Text = "Comentarios";
            // 
            // TxtComentariosCLN
            // 
            this.TxtComentariosCLN.BackColor = System.Drawing.Color.White;
            this.TxtComentariosCLN.Location = new System.Drawing.Point(6, 191);
            this.TxtComentariosCLN.Name = "TxtComentariosCLN";
            this.TxtComentariosCLN.Size = new System.Drawing.Size(227, 21);
            this.TxtComentariosCLN.TabIndex = 79;
            // 
            // LblCantidadMetros
            // 
            this.LblCantidadMetros.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblCantidadMetros.ForeColor = System.Drawing.Color.Gray;
            this.LblCantidadMetros.Location = new System.Drawing.Point(6, 132);
            this.LblCantidadMetros.Name = "LblCantidadMetros";
            this.LblCantidadMetros.Size = new System.Drawing.Size(131, 14);
            this.LblCantidadMetros.Tag = "";
            this.LblCantidadMetros.Text = "Cantidad en metros";
            // 
            // TxtCantidadMetrosCLNVAL
            // 
            this.TxtCantidadMetrosCLNVAL.BackColor = System.Drawing.Color.White;
            this.TxtCantidadMetrosCLNVAL.Location = new System.Drawing.Point(6, 149);
            this.TxtCantidadMetrosCLNVAL.Name = "TxtCantidadMetrosCLNVAL";
            this.TxtCantidadMetrosCLNVAL.Size = new System.Drawing.Size(227, 21);
            this.TxtCantidadMetrosCLNVAL.TabIndex = 78;
            this.TxtCantidadMetrosCLNVAL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCantidadMetrosCLNVAL_KeyPress);
            // 
            // LblCantidad
            // 
            this.LblCantidad.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblCantidad.ForeColor = System.Drawing.Color.Gray;
            this.LblCantidad.Location = new System.Drawing.Point(173, 90);
            this.LblCantidad.Name = "LblCantidad";
            this.LblCantidad.Size = new System.Drawing.Size(62, 14);
            this.LblCantidad.Tag = "";
            this.LblCantidad.Text = "Cantidad";
            // 
            // TxtCantidadCLNVAL
            // 
            this.TxtCantidadCLNVAL.BackColor = System.Drawing.Color.White;
            this.TxtCantidadCLNVAL.Location = new System.Drawing.Point(171, 107);
            this.TxtCantidadCLNVAL.Name = "TxtCantidadCLNVAL";
            this.TxtCantidadCLNVAL.Size = new System.Drawing.Size(64, 21);
            this.TxtCantidadCLNVAL.TabIndex = 77;
            this.TxtCantidadCLNVAL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCantidadCLNVAL_KeyPress);
            // 
            // LblLote
            // 
            this.LblLote.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblLote.ForeColor = System.Drawing.Color.Gray;
            this.LblLote.Location = new System.Drawing.Point(6, 90);
            this.LblLote.Name = "LblLote";
            this.LblLote.Size = new System.Drawing.Size(33, 14);
            this.LblLote.Text = "Lote";
            // 
            // TxtLoteCLN
            // 
            this.TxtLoteCLN.Location = new System.Drawing.Point(6, 107);
            this.TxtLoteCLN.Name = "TxtLoteCLN";
            this.TxtLoteCLN.Size = new System.Drawing.Size(157, 21);
            this.TxtLoteCLN.TabIndex = 76;
            this.TxtLoteCLN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtLote_KeyPress);
            // 
            // LblArticulo
            // 
            this.LblArticulo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblArticulo.ForeColor = System.Drawing.Color.Gray;
            this.LblArticulo.Location = new System.Drawing.Point(6, 6);
            this.LblArticulo.Name = "LblArticulo";
            this.LblArticulo.Size = new System.Drawing.Size(110, 15);
            this.LblArticulo.Text = "Artículo";
            // 
            // LblAlmacen
            // 
            this.LblAlmacen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblAlmacen.ForeColor = System.Drawing.Color.Gray;
            this.LblAlmacen.Location = new System.Drawing.Point(6, 47);
            this.LblAlmacen.Name = "LblAlmacen";
            this.LblAlmacen.Size = new System.Drawing.Size(114, 14);
            this.LblAlmacen.Text = "Almacén";
            // 
            // TxtAlmacenVAL
            // 
            this.TxtAlmacenVAL.BackColor = System.Drawing.Color.White;
            this.TxtAlmacenVAL.Location = new System.Drawing.Point(6, 66);
            this.TxtAlmacenVAL.Name = "TxtAlmacenVAL";
            this.TxtAlmacenVAL.Size = new System.Drawing.Size(195, 21);
            this.TxtAlmacenVAL.TabIndex = 74;
            this.TxtAlmacenVAL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtAlmacenVAL_KeyPress);
            // 
            // TxtArticulo
            // 
            this.TxtArticulo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtArticulo.Enabled = false;
            this.TxtArticulo.Location = new System.Drawing.Point(6, 24);
            this.TxtArticulo.Name = "TxtArticulo";
            this.TxtArticulo.Size = new System.Drawing.Size(227, 21);
            this.TxtArticulo.TabIndex = 75;
            // 
            // DatosMaestrosArticulo
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
            this.Name = "DatosMaestrosArticulo";
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
        private System.Windows.Forms.Button AñadirArticulo;
        private System.Windows.Forms.Button Guardar;
        public System.Windows.Forms.PictureBox MenuVentas;
        public System.Windows.Forms.PictureBox Regresar;
        private System.Windows.Forms.Panel Body;
        public System.Windows.Forms.PictureBox ModificarCuentaContable;
        public System.Windows.Forms.PictureBox ValidarAlmacen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtCuentaContableVAL;
        private System.Windows.Forms.Label LblComentarios;
        private System.Windows.Forms.TextBox TxtComentariosCLN;
        private System.Windows.Forms.Label LblCantidadMetros;
        private System.Windows.Forms.TextBox TxtCantidadMetrosCLNVAL;
        private System.Windows.Forms.Label LblCantidad;
        private System.Windows.Forms.TextBox TxtCantidadCLNVAL;
        private System.Windows.Forms.Label LblLote;
        private System.Windows.Forms.TextBox TxtLoteCLN;
        private System.Windows.Forms.Label LblArticulo;
        private System.Windows.Forms.Label LblAlmacen;
        private System.Windows.Forms.TextBox TxtAlmacenVAL;
        private System.Windows.Forms.TextBox TxtArticulo;
    }
}