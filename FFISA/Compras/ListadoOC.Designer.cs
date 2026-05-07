namespace FFISA.Compras
{
    partial class ListadoOC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListadoOC));
            this.Footer = new System.Windows.Forms.Panel();
            this.MenuCompras = new System.Windows.Forms.PictureBox();
            this.Continuar = new System.Windows.Forms.PictureBox();
            this.Regresar = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Panel();
            this.Lbltitle = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.TxtOrdenCompra = new System.Windows.Forms.TextBox();
            this.lblTotalOC = new System.Windows.Forms.Label();
            this.lbltotalOCtitle = new System.Windows.Forms.Label();
            this.ptbBuscarENB = new System.Windows.Forms.PictureBox();
            this.lblrangosfecha = new System.Windows.Forms.Label();
            this.LvOrdenesCompra = new System.Windows.Forms.ListView();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Footer.Controls.Add(this.MenuCompras);
            this.Footer.Controls.Add(this.Continuar);
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
            this.MenuCompras.Location = new System.Drawing.Point(106, 1);
            this.MenuCompras.Name = "MenuCompras";
            this.MenuCompras.Size = new System.Drawing.Size(29, 29);
            this.MenuCompras.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MenuCompras.Click += new System.EventHandler(this.MenuCompras_Click);
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
            this.Lbltitle.Text = "LISTADO ORDENES DE COMPRA";
            this.Lbltitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.TxtOrdenCompra);
            this.Body.Controls.Add(this.lblTotalOC);
            this.Body.Controls.Add(this.lbltotalOCtitle);
            this.Body.Controls.Add(this.ptbBuscarENB);
            this.Body.Controls.Add(this.lblrangosfecha);
            this.Body.Controls.Add(this.LvOrdenesCompra);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // TxtOrdenCompra
            // 
            this.TxtOrdenCompra.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtOrdenCompra.Location = new System.Drawing.Point(6, 33);
            this.TxtOrdenCompra.Name = "TxtOrdenCompra";
            this.TxtOrdenCompra.Size = new System.Drawing.Size(104, 21);
            this.TxtOrdenCompra.TabIndex = 30;
            this.TxtOrdenCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtOrdenCompra_KeyPress);
            // 
            // lblTotalOC
            // 
            this.lblTotalOC.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalOC.Location = new System.Drawing.Point(167, 215);
            this.lblTotalOC.Name = "lblTotalOC";
            this.lblTotalOC.Size = new System.Drawing.Size(66, 20);
            this.lblTotalOC.Text = "0";
            // 
            // lbltotalOCtitle
            // 
            this.lbltotalOCtitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbltotalOCtitle.ForeColor = System.Drawing.Color.Gray;
            this.lbltotalOCtitle.Location = new System.Drawing.Point(6, 215);
            this.lbltotalOCtitle.Name = "lbltotalOCtitle";
            this.lbltotalOCtitle.Size = new System.Drawing.Size(174, 20);
            this.lbltotalOCtitle.Text = "Total de OC encontradas: ";
            // 
            // ptbBuscarENB
            // 
            this.ptbBuscarENB.Image = ((System.Drawing.Image)(resources.GetObject("ptbBuscarENB.Image")));
            this.ptbBuscarENB.Location = new System.Drawing.Point(117, 29);
            this.ptbBuscarENB.Name = "ptbBuscarENB";
            this.ptbBuscarENB.Size = new System.Drawing.Size(25, 25);
            this.ptbBuscarENB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbBuscarENB.Click += new System.EventHandler(this.ptbBuscar_Click);
            // 
            // lblrangosfecha
            // 
            this.lblrangosfecha.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblrangosfecha.ForeColor = System.Drawing.Color.Gray;
            this.lblrangosfecha.Location = new System.Drawing.Point(6, 6);
            this.lblrangosfecha.Name = "lblrangosfecha";
            this.lblrangosfecha.Size = new System.Drawing.Size(227, 20);
            this.lblrangosfecha.Text = "Orden de compra";
            // 
            // LvOrdenesCompra
            // 
            this.LvOrdenesCompra.FullRowSelect = true;
            this.LvOrdenesCompra.Location = new System.Drawing.Point(6, 60);
            this.LvOrdenesCompra.Name = "LvOrdenesCompra";
            this.LvOrdenesCompra.Size = new System.Drawing.Size(229, 152);
            this.LvOrdenesCompra.TabIndex = 29;
            this.LvOrdenesCompra.View = System.Windows.Forms.View.Details;
            this.LvOrdenesCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LvOrdenesCompra_KeyPress);
            // 
            // ListadoOC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.Body);
            this.Controls.Add(this.Footer);
            this.Controls.Add(this.Header);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "ListadoOC";
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
        public System.Windows.Forms.PictureBox MenuCompras;
        private System.Windows.Forms.Panel Body;
        private System.Windows.Forms.TextBox TxtOrdenCompra;
        private System.Windows.Forms.Label lblTotalOC;
        private System.Windows.Forms.Label lbltotalOCtitle;
        private System.Windows.Forms.PictureBox ptbBuscarENB;
        private System.Windows.Forms.Label lblrangosfecha;
        private System.Windows.Forms.ListView LvOrdenesCompra;
    }
}