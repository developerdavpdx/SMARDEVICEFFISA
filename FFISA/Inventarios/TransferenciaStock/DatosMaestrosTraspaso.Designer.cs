namespace FFISA.Inventarios.TransferenciaStock
{
    partial class DatosMaestrosTraspaso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatosMaestrosTraspaso));
            this.Footer = new System.Windows.Forms.Panel();
            this.PtbGuardar = new System.Windows.Forms.PictureBox();
            this.Regresar = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Panel();
            this.LblOrdenVenta = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.LblTotalTraspasos = new System.Windows.Forms.Label();
            this.LblFolio = new System.Windows.Forms.Label();
            this.PtbValidaAlmacenDestino = new System.Windows.Forms.PictureBox();
            this.LblAlmacenDestino = new System.Windows.Forms.Label();
            this.TxtAlmacenDestinoVAL = new System.Windows.Forms.TextBox();
            this.LblComentarios = new System.Windows.Forms.Label();
            this.TxtComentariosCLN = new System.Windows.Forms.TextBox();
            this.LblAlmacenOrigen = new System.Windows.Forms.Label();
            this.TxtAlmacenOrigenVALCLN = new System.Windows.Forms.TextBox();
            this.LblCantidad = new System.Windows.Forms.Label();
            this.TxtCantidadVALCLN = new System.Windows.Forms.TextBox();
            this.LblLoteOrigen = new System.Windows.Forms.Label();
            this.TxtLoteOrigenVALCLN = new System.Windows.Forms.TextBox();
            this.LblArticulo = new System.Windows.Forms.Label();
            this.TxtArticuloVALCLN = new System.Windows.Forms.TextBox();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Footer.Controls.Add(this.PtbGuardar);
            this.Footer.Controls.Add(this.Regresar);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 290);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(240, 30);
            // 
            // PtbGuardar
            // 
            this.PtbGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.PtbGuardar.Image = ((System.Drawing.Image)(resources.GetObject("PtbGuardar.Image")));
            this.PtbGuardar.Location = new System.Drawing.Point(208, 3);
            this.PtbGuardar.Name = "PtbGuardar";
            this.PtbGuardar.Size = new System.Drawing.Size(25, 25);
            this.PtbGuardar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PtbGuardar.Click += new System.EventHandler(this.Guardar_Click);
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
            this.LblOrdenVenta.Text = "DATOS MAESTROS TRASPASO";
            this.LblOrdenVenta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.LblTotalTraspasos);
            this.Body.Controls.Add(this.LblFolio);
            this.Body.Controls.Add(this.PtbValidaAlmacenDestino);
            this.Body.Controls.Add(this.LblAlmacenDestino);
            this.Body.Controls.Add(this.TxtAlmacenDestinoVAL);
            this.Body.Controls.Add(this.LblComentarios);
            this.Body.Controls.Add(this.TxtComentariosCLN);
            this.Body.Controls.Add(this.LblAlmacenOrigen);
            this.Body.Controls.Add(this.TxtAlmacenOrigenVALCLN);
            this.Body.Controls.Add(this.LblCantidad);
            this.Body.Controls.Add(this.TxtCantidadVALCLN);
            this.Body.Controls.Add(this.LblLoteOrigen);
            this.Body.Controls.Add(this.TxtLoteOrigenVALCLN);
            this.Body.Controls.Add(this.LblArticulo);
            this.Body.Controls.Add(this.TxtArticuloVALCLN);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // LblTotalTraspasos
            // 
            this.LblTotalTraspasos.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LblTotalTraspasos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.LblTotalTraspasos.Location = new System.Drawing.Point(7, 238);
            this.LblTotalTraspasos.Name = "LblTotalTraspasos";
            this.LblTotalTraspasos.Size = new System.Drawing.Size(226, 18);
            this.LblTotalTraspasos.Text = "No. rollos: 0";
            // 
            // LblFolio
            // 
            this.LblFolio.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LblFolio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.LblFolio.Location = new System.Drawing.Point(6, 6);
            this.LblFolio.Name = "LblFolio";
            this.LblFolio.Size = new System.Drawing.Size(229, 17);
            this.LblFolio.Text = "Folio: XXXXXX";
            // 
            // PtbValidaAlmacenDestino
            // 
            this.PtbValidaAlmacenDestino.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.PtbValidaAlmacenDestino.Image = ((System.Drawing.Image)(resources.GetObject("PtbValidaAlmacenDestino.Image")));
            this.PtbValidaAlmacenDestino.Location = new System.Drawing.Point(210, 128);
            this.PtbValidaAlmacenDestino.Name = "PtbValidaAlmacenDestino";
            this.PtbValidaAlmacenDestino.Size = new System.Drawing.Size(25, 25);
            this.PtbValidaAlmacenDestino.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PtbValidaAlmacenDestino.Click += new System.EventHandler(this.ValidarAlmacen_Click);
            // 
            // LblAlmacenDestino
            // 
            this.LblAlmacenDestino.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblAlmacenDestino.ForeColor = System.Drawing.Color.Gray;
            this.LblAlmacenDestino.Location = new System.Drawing.Point(6, 111);
            this.LblAlmacenDestino.Name = "LblAlmacenDestino";
            this.LblAlmacenDestino.Size = new System.Drawing.Size(114, 14);
            this.LblAlmacenDestino.Text = "Almacén Destino";
            // 
            // TxtAlmacenDestinoVAL
            // 
            this.TxtAlmacenDestinoVAL.BackColor = System.Drawing.Color.White;
            this.TxtAlmacenDestinoVAL.Location = new System.Drawing.Point(6, 130);
            this.TxtAlmacenDestinoVAL.Name = "TxtAlmacenDestinoVAL";
            this.TxtAlmacenDestinoVAL.Size = new System.Drawing.Size(198, 21);
            this.TxtAlmacenDestinoVAL.TabIndex = 17;
            this.TxtAlmacenDestinoVAL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtAlmacenDestino_KeyPress);
            // 
            // LblComentarios
            // 
            this.LblComentarios.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblComentarios.ForeColor = System.Drawing.Color.Gray;
            this.LblComentarios.Location = new System.Drawing.Point(6, 194);
            this.LblComentarios.Name = "LblComentarios";
            this.LblComentarios.Size = new System.Drawing.Size(88, 14);
            this.LblComentarios.Text = "Comentarios";
            // 
            // TxtComentariosCLN
            // 
            this.TxtComentariosCLN.Location = new System.Drawing.Point(6, 211);
            this.TxtComentariosCLN.Name = "TxtComentariosCLN";
            this.TxtComentariosCLN.Size = new System.Drawing.Size(227, 21);
            this.TxtComentariosCLN.TabIndex = 7;
            // 
            // LblAlmacenOrigen
            // 
            this.LblAlmacenOrigen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblAlmacenOrigen.ForeColor = System.Drawing.Color.Gray;
            this.LblAlmacenOrigen.Location = new System.Drawing.Point(6, 67);
            this.LblAlmacenOrigen.Name = "LblAlmacenOrigen";
            this.LblAlmacenOrigen.Size = new System.Drawing.Size(110, 15);
            this.LblAlmacenOrigen.Text = "Almacén Origen";
            // 
            // TxtAlmacenOrigenVALCLN
            // 
            this.TxtAlmacenOrigenVALCLN.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtAlmacenOrigenVALCLN.Enabled = false;
            this.TxtAlmacenOrigenVALCLN.Location = new System.Drawing.Point(6, 85);
            this.TxtAlmacenOrigenVALCLN.Name = "TxtAlmacenOrigenVALCLN";
            this.TxtAlmacenOrigenVALCLN.Size = new System.Drawing.Size(229, 21);
            this.TxtAlmacenOrigenVALCLN.TabIndex = 3;
            // 
            // LblCantidad
            // 
            this.LblCantidad.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblCantidad.ForeColor = System.Drawing.Color.Gray;
            this.LblCantidad.Location = new System.Drawing.Point(146, 26);
            this.LblCantidad.Name = "LblCantidad";
            this.LblCantidad.Size = new System.Drawing.Size(64, 14);
            this.LblCantidad.Tag = "";
            this.LblCantidad.Text = "Cantidad";
            // 
            // TxtCantidadVALCLN
            // 
            this.TxtCantidadVALCLN.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtCantidadVALCLN.Enabled = false;
            this.TxtCantidadVALCLN.Location = new System.Drawing.Point(146, 43);
            this.TxtCantidadVALCLN.Name = "TxtCantidadVALCLN";
            this.TxtCantidadVALCLN.Size = new System.Drawing.Size(90, 21);
            this.TxtCantidadVALCLN.TabIndex = 2;
            // 
            // LblLoteOrigen
            // 
            this.LblLoteOrigen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblLoteOrigen.ForeColor = System.Drawing.Color.Gray;
            this.LblLoteOrigen.Location = new System.Drawing.Point(6, 26);
            this.LblLoteOrigen.Name = "LblLoteOrigen";
            this.LblLoteOrigen.Size = new System.Drawing.Size(131, 14);
            this.LblLoteOrigen.Tag = "";
            this.LblLoteOrigen.Text = "Lote Origen";
            // 
            // TxtLoteOrigenVALCLN
            // 
            this.TxtLoteOrigenVALCLN.BackColor = System.Drawing.Color.White;
            this.TxtLoteOrigenVALCLN.Location = new System.Drawing.Point(6, 43);
            this.TxtLoteOrigenVALCLN.MaxLength = 20;
            this.TxtLoteOrigenVALCLN.Name = "TxtLoteOrigenVALCLN";
            this.TxtLoteOrigenVALCLN.Size = new System.Drawing.Size(134, 21);
            this.TxtLoteOrigenVALCLN.TabIndex = 1;
            this.TxtLoteOrigenVALCLN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtLoteOrigen_KeyPress);
            // 
            // LblArticulo
            // 
            this.LblArticulo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblArticulo.ForeColor = System.Drawing.Color.Gray;
            this.LblArticulo.Location = new System.Drawing.Point(6, 153);
            this.LblArticulo.Name = "LblArticulo";
            this.LblArticulo.Size = new System.Drawing.Size(53, 14);
            this.LblArticulo.Text = "Artículo";
            // 
            // TxtArticuloVALCLN
            // 
            this.TxtArticuloVALCLN.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtArticuloVALCLN.Enabled = false;
            this.TxtArticuloVALCLN.Location = new System.Drawing.Point(6, 170);
            this.TxtArticuloVALCLN.Name = "TxtArticuloVALCLN";
            this.TxtArticuloVALCLN.Size = new System.Drawing.Size(227, 21);
            this.TxtArticuloVALCLN.TabIndex = 6;
            // 
            // DatosMaestrosTraspaso
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
            this.Name = "DatosMaestrosTraspaso";
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
        private System.Windows.Forms.Panel Body;
        private System.Windows.Forms.Label LblLoteOrigen;
        private System.Windows.Forms.TextBox TxtLoteOrigenVALCLN;
        private System.Windows.Forms.Label LblArticulo;
        private System.Windows.Forms.TextBox TxtArticuloVALCLN;
        private System.Windows.Forms.Label LblCantidad;
        private System.Windows.Forms.TextBox TxtCantidadVALCLN;
        private System.Windows.Forms.Label LblAlmacenOrigen;
        private System.Windows.Forms.TextBox TxtAlmacenOrigenVALCLN;
        private System.Windows.Forms.Label LblComentarios;
        private System.Windows.Forms.TextBox TxtComentariosCLN;
        public System.Windows.Forms.PictureBox PtbGuardar;
        public System.Windows.Forms.PictureBox Regresar;
        public System.Windows.Forms.PictureBox PtbValidaAlmacenDestino;
        private System.Windows.Forms.Label LblAlmacenDestino;
        private System.Windows.Forms.TextBox TxtAlmacenDestinoVAL;
        public System.Windows.Forms.Label LblFolio;
        public System.Windows.Forms.Label LblTotalTraspasos;
    }
}