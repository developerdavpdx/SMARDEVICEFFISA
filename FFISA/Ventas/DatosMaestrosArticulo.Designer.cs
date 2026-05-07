namespace FFISA.Ventas
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
            this.FinalizarEscaneo = new System.Windows.Forms.Button();
            this.MenuVentas = new System.Windows.Forms.PictureBox();
            this.Regresar = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Panel();
            this.LblOrdenVenta = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.TxtArticulo = new System.Windows.Forms.TextBox();
            this.TxtAlmacen = new System.Windows.Forms.TextBox();
            this.LblAlmacen = new System.Windows.Forms.Label();
            this.LblArticulo = new System.Windows.Forms.Label();
            this.TxtUMS = new System.Windows.Forms.TextBox();
            this.LblUMV = new System.Windows.Forms.Label();
            this.TxtUMI = new System.Windows.Forms.TextBox();
            this.LblUMI = new System.Windows.Forms.Label();
            this.TxtCantidadReferenciaCLN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ChkCantidadReferenciaCLN = new System.Windows.Forms.CheckBox();
            this.TxtLoteCLN = new System.Windows.Forms.TextBox();
            this.LblLote = new System.Windows.Forms.Label();
            this.TxtCantidadCLN = new System.Windows.Forms.TextBox();
            this.LblCantidad = new System.Windows.Forms.Label();
            this.LblNoRollos = new System.Windows.Forms.Label();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Footer.Controls.Add(this.FinalizarEscaneo);
            this.Footer.Controls.Add(this.MenuVentas);
            this.Footer.Controls.Add(this.Regresar);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 290);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(240, 30);
            // 
            // FinalizarEscaneo
            // 
            this.FinalizarEscaneo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.FinalizarEscaneo.ForeColor = System.Drawing.Color.White;
            this.FinalizarEscaneo.Location = new System.Drawing.Point(123, 3);
            this.FinalizarEscaneo.Name = "FinalizarEscaneo";
            this.FinalizarEscaneo.Size = new System.Drawing.Size(113, 25);
            this.FinalizarEscaneo.TabIndex = 26;
            this.FinalizarEscaneo.Tag = "";
            this.FinalizarEscaneo.Text = "Finalizar Escaneo";
            this.FinalizarEscaneo.Click += new System.EventHandler(this.FinalizarEscaneo_Click);
            // 
            // MenuVentas
            // 
            this.MenuVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.MenuVentas.Image = ((System.Drawing.Image)(resources.GetObject("MenuVentas.Image")));
            this.MenuVentas.Location = new System.Drawing.Point(8, 1);
            this.MenuVentas.Name = "MenuVentas";
            this.MenuVentas.Size = new System.Drawing.Size(29, 29);
            this.MenuVentas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MenuVentas.Click += new System.EventHandler(this.MenuEntregas_Click);
            // 
            // Regresar
            // 
            this.Regresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Regresar.Image = ((System.Drawing.Image)(resources.GetObject("Regresar.Image")));
            this.Regresar.Location = new System.Drawing.Point(45, 3);
            this.Regresar.Name = "Regresar";
            this.Regresar.Size = new System.Drawing.Size(25, 25);
            this.Regresar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Regresar.Click += new System.EventHandler(this.Regresar_Click);
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
            this.LblOrdenVenta.Size = new System.Drawing.Size(225, 15);
            this.LblOrdenVenta.Text = "DATOS MAESTROS ARTÍCULO";
            this.LblOrdenVenta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.LblNoRollos);
            this.Body.Controls.Add(this.LblCantidad);
            this.Body.Controls.Add(this.TxtCantidadCLN);
            this.Body.Controls.Add(this.LblLote);
            this.Body.Controls.Add(this.TxtLoteCLN);
            this.Body.Controls.Add(this.ChkCantidadReferenciaCLN);
            this.Body.Controls.Add(this.label1);
            this.Body.Controls.Add(this.TxtCantidadReferenciaCLN);
            this.Body.Controls.Add(this.LblUMI);
            this.Body.Controls.Add(this.TxtUMI);
            this.Body.Controls.Add(this.LblUMV);
            this.Body.Controls.Add(this.TxtUMS);
            this.Body.Controls.Add(this.LblArticulo);
            this.Body.Controls.Add(this.LblAlmacen);
            this.Body.Controls.Add(this.TxtAlmacen);
            this.Body.Controls.Add(this.TxtArticulo);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // TxtArticulo
            // 
            this.TxtArticulo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtArticulo.Enabled = false;
            this.TxtArticulo.Location = new System.Drawing.Point(8, 24);
            this.TxtArticulo.Name = "TxtArticulo";
            this.TxtArticulo.Size = new System.Drawing.Size(227, 21);
            this.TxtArticulo.TabIndex = 53;
            // 
            // TxtAlmacen
            // 
            this.TxtAlmacen.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtAlmacen.Enabled = false;
            this.TxtAlmacen.Location = new System.Drawing.Point(8, 65);
            this.TxtAlmacen.Name = "TxtAlmacen";
            this.TxtAlmacen.ReadOnly = true;
            this.TxtAlmacen.Size = new System.Drawing.Size(227, 21);
            this.TxtAlmacen.TabIndex = 52;
            // 
            // LblAlmacen
            // 
            this.LblAlmacen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblAlmacen.ForeColor = System.Drawing.Color.Gray;
            this.LblAlmacen.Location = new System.Drawing.Point(6, 48);
            this.LblAlmacen.Name = "LblAlmacen";
            this.LblAlmacen.Size = new System.Drawing.Size(114, 14);
            this.LblAlmacen.Text = "Almacén";
            // 
            // LblArticulo
            // 
            this.LblArticulo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblArticulo.ForeColor = System.Drawing.Color.Gray;
            this.LblArticulo.Location = new System.Drawing.Point(8, 6);
            this.LblArticulo.Name = "LblArticulo";
            this.LblArticulo.Size = new System.Drawing.Size(110, 15);
            this.LblArticulo.Text = "Artículo";
            // 
            // TxtUMS
            // 
            this.TxtUMS.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtUMS.Enabled = false;
            this.TxtUMS.Location = new System.Drawing.Point(8, 109);
            this.TxtUMS.Name = "TxtUMS";
            this.TxtUMS.Size = new System.Drawing.Size(110, 21);
            this.TxtUMS.TabIndex = 54;
            // 
            // LblUMV
            // 
            this.LblUMV.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblUMV.ForeColor = System.Drawing.Color.Gray;
            this.LblUMV.Location = new System.Drawing.Point(6, 91);
            this.LblUMV.Name = "LblUMV";
            this.LblUMV.Size = new System.Drawing.Size(35, 15);
            this.LblUMV.Text = "UMS";
            // 
            // TxtUMI
            // 
            this.TxtUMI.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtUMI.Enabled = false;
            this.TxtUMI.Location = new System.Drawing.Point(125, 109);
            this.TxtUMI.Name = "TxtUMI";
            this.TxtUMI.Size = new System.Drawing.Size(110, 21);
            this.TxtUMI.TabIndex = 55;
            // 
            // LblUMI
            // 
            this.LblUMI.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblUMI.ForeColor = System.Drawing.Color.Gray;
            this.LblUMI.Location = new System.Drawing.Point(123, 91);
            this.LblUMI.Name = "LblUMI";
            this.LblUMI.Size = new System.Drawing.Size(35, 15);
            this.LblUMI.Text = "UMI";
            // 
            // TxtCantidadReferenciaCLN
            // 
            this.TxtCantidadReferenciaCLN.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtCantidadReferenciaCLN.Enabled = false;
            this.TxtCantidadReferenciaCLN.Location = new System.Drawing.Point(34, 160);
            this.TxtCantidadReferenciaCLN.Name = "TxtCantidadReferenciaCLN";
            this.TxtCantidadReferenciaCLN.Size = new System.Drawing.Size(201, 21);
            this.TxtCantidadReferenciaCLN.TabIndex = 56;
            this.TxtCantidadReferenciaCLN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCantidadReferenciaCLN_KeyPress);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(8, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 14);
            this.label1.Tag = "";
            this.label1.Text = "Cantidad Referencia";
            // 
            // ChkCantidadReferenciaCLN
            // 
            this.ChkCantidadReferenciaCLN.Location = new System.Drawing.Point(6, 161);
            this.ChkCantidadReferenciaCLN.Name = "ChkCantidadReferenciaCLN";
            this.ChkCantidadReferenciaCLN.Size = new System.Drawing.Size(22, 20);
            this.ChkCantidadReferenciaCLN.TabIndex = 57;
            this.ChkCantidadReferenciaCLN.Click += new System.EventHandler(this.ChkCantidadReferencia_Click);
            // 
            // TxtLoteCLN
            // 
            this.TxtLoteCLN.Location = new System.Drawing.Point(7, 208);
            this.TxtLoteCLN.MaxLength = 20;
            this.TxtLoteCLN.Name = "TxtLoteCLN";
            this.TxtLoteCLN.Size = new System.Drawing.Size(157, 21);
            this.TxtLoteCLN.TabIndex = 58;
            this.TxtLoteCLN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtLote_KeyPress);
            // 
            // LblLote
            // 
            this.LblLote.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblLote.ForeColor = System.Drawing.Color.Gray;
            this.LblLote.Location = new System.Drawing.Point(7, 191);
            this.LblLote.Name = "LblLote";
            this.LblLote.Size = new System.Drawing.Size(33, 14);
            this.LblLote.Text = "Lote";
            // 
            // TxtCantidadCLN
            // 
            this.TxtCantidadCLN.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtCantidadCLN.Enabled = false;
            this.TxtCantidadCLN.Location = new System.Drawing.Point(170, 208);
            this.TxtCantidadCLN.Name = "TxtCantidadCLN";
            this.TxtCantidadCLN.Size = new System.Drawing.Size(64, 21);
            this.TxtCantidadCLN.TabIndex = 59;
            // 
            // LblCantidad
            // 
            this.LblCantidad.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblCantidad.ForeColor = System.Drawing.Color.Gray;
            this.LblCantidad.Location = new System.Drawing.Point(172, 191);
            this.LblCantidad.Name = "LblCantidad";
            this.LblCantidad.Size = new System.Drawing.Size(62, 14);
            this.LblCantidad.Tag = "";
            this.LblCantidad.Text = "Cantidad";
            // 
            // LblNoRollos
            // 
            this.LblNoRollos.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LblNoRollos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.LblNoRollos.Location = new System.Drawing.Point(7, 232);
            this.LblNoRollos.Name = "LblNoRollos";
            this.LblNoRollos.Size = new System.Drawing.Size(226, 22);
            this.LblNoRollos.Text = "No. rollos: 0";
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
        public System.Windows.Forms.PictureBox Regresar;
        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Label LblOrdenVenta;
        public System.Windows.Forms.PictureBox MenuVentas;
        private System.Windows.Forms.Button FinalizarEscaneo;
        private System.Windows.Forms.Panel Body;
        public System.Windows.Forms.Label LblNoRollos;
        private System.Windows.Forms.Label LblCantidad;
        private System.Windows.Forms.TextBox TxtCantidadCLN;
        private System.Windows.Forms.Label LblLote;
        private System.Windows.Forms.TextBox TxtLoteCLN;
        private System.Windows.Forms.CheckBox ChkCantidadReferenciaCLN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtCantidadReferenciaCLN;
        private System.Windows.Forms.Label LblUMI;
        private System.Windows.Forms.TextBox TxtUMI;
        private System.Windows.Forms.Label LblUMV;
        private System.Windows.Forms.TextBox TxtUMS;
        private System.Windows.Forms.Label LblArticulo;
        private System.Windows.Forms.Label LblAlmacen;
        private System.Windows.Forms.TextBox TxtAlmacen;
        private System.Windows.Forms.TextBox TxtArticulo;
    }
}