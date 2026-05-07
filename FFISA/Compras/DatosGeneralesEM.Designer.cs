namespace FFISA.Compras
{
    partial class DatosGeneralesEM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatosGeneralesEM));
            this.Footer = new System.Windows.Forms.Panel();
            this.MenuCompras = new System.Windows.Forms.PictureBox();
            this.Guardar = new System.Windows.Forms.PictureBox();
            this.Regresar = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.LblLote = new System.Windows.Forms.Label();
            this.TxtloteCLN = new System.Windows.Forms.TextBox();
            this.Lblcantidad = new System.Windows.Forms.Label();
            this.Lblarticulo = new System.Windows.Forms.Label();
            this.Lblordencompra = new System.Windows.Forms.Label();
            this.TxtOrdenCompraVAL = new System.Windows.Forms.TextBox();
            this.TxtCantidadCLNVAL = new System.Windows.Forms.TextBox();
            this.TxtArticuloVAL = new System.Windows.Forms.TextBox();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Footer.Controls.Add(this.MenuCompras);
            this.Footer.Controls.Add(this.Guardar);
            this.Footer.Controls.Add(this.Regresar);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 290);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(240, 30);
            // 
            // MenuCompras
            // 
            this.MenuCompras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.MenuCompras.Image = ((System.Drawing.Image)(resources.GetObject("MenuCompras.Image")));
            this.MenuCompras.Location = new System.Drawing.Point(108, 1);
            this.MenuCompras.Name = "MenuCompras";
            this.MenuCompras.Size = new System.Drawing.Size(29, 29);
            this.MenuCompras.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MenuCompras.Click += new System.EventHandler(this.MenuCompras_Click);
            // 
            // Guardar
            // 
            this.Guardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Guardar.Image = ((System.Drawing.Image)(resources.GetObject("Guardar.Image")));
            this.Guardar.Location = new System.Drawing.Point(208, 3);
            this.Guardar.Name = "Guardar";
            this.Guardar.Size = new System.Drawing.Size(25, 25);
            this.Guardar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Guardar.Click += new System.EventHandler(this.Guardar_Click);
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
            this.label6.Location = new System.Drawing.Point(8, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(225, 15);
            this.label6.Text = "NUEVA ENTRADA";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.LblLote);
            this.Body.Controls.Add(this.TxtloteCLN);
            this.Body.Controls.Add(this.Lblcantidad);
            this.Body.Controls.Add(this.Lblarticulo);
            this.Body.Controls.Add(this.Lblordencompra);
            this.Body.Controls.Add(this.TxtOrdenCompraVAL);
            this.Body.Controls.Add(this.TxtCantidadCLNVAL);
            this.Body.Controls.Add(this.TxtArticuloVAL);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // LblLote
            // 
            this.LblLote.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblLote.ForeColor = System.Drawing.Color.Gray;
            this.LblLote.Location = new System.Drawing.Point(7, 136);
            this.LblLote.Name = "LblLote";
            this.LblLote.Size = new System.Drawing.Size(110, 20);
            this.LblLote.Text = "Lote";
            // 
            // TxtloteCLN
            // 
            this.TxtloteCLN.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtloteCLN.Location = new System.Drawing.Point(7, 159);
            this.TxtloteCLN.Name = "TxtloteCLN";
            this.TxtloteCLN.Size = new System.Drawing.Size(227, 21);
            this.TxtloteCLN.TabIndex = 24;
            // 
            // Lblcantidad
            // 
            this.Lblcantidad.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.Lblcantidad.ForeColor = System.Drawing.Color.Gray;
            this.Lblcantidad.Location = new System.Drawing.Point(9, 194);
            this.Lblcantidad.Name = "Lblcantidad";
            this.Lblcantidad.Size = new System.Drawing.Size(110, 20);
            this.Lblcantidad.Text = "Cantidad";
            // 
            // Lblarticulo
            // 
            this.Lblarticulo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.Lblarticulo.ForeColor = System.Drawing.Color.Gray;
            this.Lblarticulo.Location = new System.Drawing.Point(9, 76);
            this.Lblarticulo.Name = "Lblarticulo";
            this.Lblarticulo.Size = new System.Drawing.Size(110, 20);
            this.Lblarticulo.Text = "Artículo";
            // 
            // Lblordencompra
            // 
            this.Lblordencompra.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.Lblordencompra.ForeColor = System.Drawing.Color.Gray;
            this.Lblordencompra.Location = new System.Drawing.Point(9, 23);
            this.Lblordencompra.Name = "Lblordencompra";
            this.Lblordencompra.Size = new System.Drawing.Size(114, 20);
            this.Lblordencompra.Text = "Orden de compra";
            // 
            // TxtOrdenCompraVAL
            // 
            this.TxtOrdenCompraVAL.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtOrdenCompraVAL.Enabled = false;
            this.TxtOrdenCompraVAL.Location = new System.Drawing.Point(9, 46);
            this.TxtOrdenCompraVAL.Name = "TxtOrdenCompraVAL";
            this.TxtOrdenCompraVAL.ReadOnly = true;
            this.TxtOrdenCompraVAL.Size = new System.Drawing.Size(225, 21);
            this.TxtOrdenCompraVAL.TabIndex = 22;
            // 
            // TxtCantidadCLNVAL
            // 
            this.TxtCantidadCLNVAL.Location = new System.Drawing.Point(9, 217);
            this.TxtCantidadCLNVAL.Name = "TxtCantidadCLNVAL";
            this.TxtCantidadCLNVAL.Size = new System.Drawing.Size(225, 21);
            this.TxtCantidadCLNVAL.TabIndex = 25;
            this.TxtCantidadCLNVAL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCantidadCLNVAL_KeyPress);
            // 
            // TxtArticuloVAL
            // 
            this.TxtArticuloVAL.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtArticuloVAL.Location = new System.Drawing.Point(9, 99);
            this.TxtArticuloVAL.Name = "TxtArticuloVAL";
            this.TxtArticuloVAL.Size = new System.Drawing.Size(225, 21);
            this.TxtArticuloVAL.TabIndex = 23;
            // 
            // DatosGeneralesEM
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
            this.Name = "DatosGeneralesEM";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Footer.ResumeLayout(false);
            this.Header.ResumeLayout(false);
            this.Body.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Footer;
        public System.Windows.Forms.PictureBox Guardar;
        public System.Windows.Forms.PictureBox Regresar;
        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.PictureBox MenuCompras;
        private System.Windows.Forms.Panel Body;
        private System.Windows.Forms.Label LblLote;
        private System.Windows.Forms.TextBox TxtloteCLN;
        private System.Windows.Forms.Label Lblcantidad;
        private System.Windows.Forms.Label Lblarticulo;
        private System.Windows.Forms.Label Lblordencompra;
        private System.Windows.Forms.TextBox TxtOrdenCompraVAL;
        private System.Windows.Forms.TextBox TxtCantidadCLNVAL;
        private System.Windows.Forms.TextBox TxtArticuloVAL;
    }
}