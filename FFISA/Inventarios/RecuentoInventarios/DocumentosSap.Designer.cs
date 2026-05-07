namespace FFISA.Inventarios.RecuentoInventarios
{
    partial class DocumentosSap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentosSap));
            this.Footer = new System.Windows.Forms.Panel();
            this.Continuar = new System.Windows.Forms.PictureBox();
            this.Regresar = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Panel();
            this.Lbltitle = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.TxtRecuentoInventario = new System.Windows.Forms.TextBox();
            this.lblTotalRecuentos = new System.Windows.Forms.Label();
            this.lbltotalRecuentosTitle = new System.Windows.Forms.Label();
            this.ptbBuscarRecuento = new System.Windows.Forms.PictureBox();
            this.lblrangosfecha = new System.Windows.Forms.Label();
            this.LvOrdenesVenta = new System.Windows.Forms.ListView();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.Body.SuspendLayout();
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
            this.Lbltitle.Text = "LISTADO RECUENTOS SAP";
            this.Lbltitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.TxtRecuentoInventario);
            this.Body.Controls.Add(this.lblTotalRecuentos);
            this.Body.Controls.Add(this.lbltotalRecuentosTitle);
            this.Body.Controls.Add(this.ptbBuscarRecuento);
            this.Body.Controls.Add(this.lblrangosfecha);
            this.Body.Controls.Add(this.LvOrdenesVenta);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // TxtRecuentoInventario
            // 
            this.TxtRecuentoInventario.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtRecuentoInventario.Location = new System.Drawing.Point(6, 32);
            this.TxtRecuentoInventario.Name = "TxtRecuentoInventario";
            this.TxtRecuentoInventario.Size = new System.Drawing.Size(104, 21);
            this.TxtRecuentoInventario.TabIndex = 30;
            this.TxtRecuentoInventario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtRecuento_KeyPress);
            // 
            // lblTotalRecuentos
            // 
            this.lblTotalRecuentos.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalRecuentos.Location = new System.Drawing.Point(112, 214);
            this.lblTotalRecuentos.Name = "lblTotalRecuentos";
            this.lblTotalRecuentos.Size = new System.Drawing.Size(66, 20);
            this.lblTotalRecuentos.Text = "0";
            // 
            // lbltotalRecuentosTitle
            // 
            this.lbltotalRecuentosTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbltotalRecuentosTitle.ForeColor = System.Drawing.Color.Gray;
            this.lbltotalRecuentosTitle.Location = new System.Drawing.Point(6, 214);
            this.lbltotalRecuentosTitle.Name = "lbltotalRecuentosTitle";
            this.lbltotalRecuentosTitle.Size = new System.Drawing.Size(117, 20);
            this.lbltotalRecuentosTitle.Text = "Total Recuentos:";
            // 
            // ptbBuscarRecuento
            // 
            this.ptbBuscarRecuento.Image = ((System.Drawing.Image)(resources.GetObject("ptbBuscarRecuento.Image")));
            this.ptbBuscarRecuento.Location = new System.Drawing.Point(117, 28);
            this.ptbBuscarRecuento.Name = "ptbBuscarRecuento";
            this.ptbBuscarRecuento.Size = new System.Drawing.Size(25, 25);
            this.ptbBuscarRecuento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbBuscarRecuento.Click += new System.EventHandler(this.ptbBuscar_Click);
            // 
            // lblrangosfecha
            // 
            this.lblrangosfecha.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblrangosfecha.ForeColor = System.Drawing.Color.Gray;
            this.lblrangosfecha.Location = new System.Drawing.Point(6, 5);
            this.lblrangosfecha.Name = "lblrangosfecha";
            this.lblrangosfecha.Size = new System.Drawing.Size(227, 20);
            this.lblrangosfecha.Text = "Recuento de inventario";
            // 
            // LvOrdenesVenta
            // 
            this.LvOrdenesVenta.FullRowSelect = true;
            this.LvOrdenesVenta.Location = new System.Drawing.Point(6, 59);
            this.LvOrdenesVenta.Name = "LvOrdenesVenta";
            this.LvOrdenesVenta.Size = new System.Drawing.Size(229, 152);
            this.LvOrdenesVenta.TabIndex = 29;
            this.LvOrdenesVenta.View = System.Windows.Forms.View.Details;
            this.LvOrdenesVenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LvRecuentosSap_KeyPress);
            // 
            // DocumentosSap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.Body);
            this.Controls.Add(this.Footer);
            this.Controls.Add(this.Header);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "DocumentosSap";
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
        private System.Windows.Forms.Panel Body;
        private System.Windows.Forms.TextBox TxtRecuentoInventario;
        private System.Windows.Forms.Label lblTotalRecuentos;
        private System.Windows.Forms.Label lbltotalRecuentosTitle;
        private System.Windows.Forms.PictureBox ptbBuscarRecuento;
        private System.Windows.Forms.Label lblrangosfecha;
        private System.Windows.Forms.ListView LvOrdenesVenta;
    }
}