namespace FFISA.Compras
{
    partial class SeleccionArticuloOC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeleccionArticuloOC));
            this.Footer = new System.Windows.Forms.Panel();
            this.MenuCompras = new System.Windows.Forms.PictureBox();
            this.Regresar = new System.Windows.Forms.PictureBox();
            this.Continuar = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.lblOC = new System.Windows.Forms.Label();
            this.lblTotalOC = new System.Windows.Forms.Label();
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
            this.Footer.Controls.Add(this.MenuCompras);
            this.Footer.Controls.Add(this.Regresar);
            this.Footer.Controls.Add(this.Continuar);
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
            this.Body.Controls.Add(this.lblOC);
            this.Body.Controls.Add(this.lblTotalOC);
            this.Body.Controls.Add(this.lbltotalOCtitle);
            this.Body.Controls.Add(this.LvArticulos);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // lblOC
            // 
            this.lblOC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblOC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(118)))), ((int)(((byte)(189)))));
            this.lblOC.Location = new System.Drawing.Point(9, 12);
            this.lblOC.Name = "lblOC";
            this.lblOC.Size = new System.Drawing.Size(224, 20);
            this.lblOC.Text = "OC: XXXX";
            // 
            // lblTotalOC
            // 
            this.lblTotalOC.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalOC.Location = new System.Drawing.Point(200, 229);
            this.lblTotalOC.Name = "lblTotalOC";
            this.lblTotalOC.Size = new System.Drawing.Size(34, 20);
            this.lblTotalOC.Text = "0";
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
            this.LvArticulos.Location = new System.Drawing.Point(9, 48);
            this.LvArticulos.Name = "LvArticulos";
            this.LvArticulos.Size = new System.Drawing.Size(225, 178);
            this.LvArticulos.TabIndex = 14;
            this.LvArticulos.View = System.Windows.Forms.View.Details;
            this.LvArticulos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LvArticulos_KeyPress);
            // 
            // SeleccionArticuloOC
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
            this.Name = "SeleccionArticuloOC";
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
        public System.Windows.Forms.PictureBox Regresar;
        public System.Windows.Forms.PictureBox MenuCompras;
        private System.Windows.Forms.Panel Body;
        private System.Windows.Forms.Label lblOC;
        private System.Windows.Forms.Label lblTotalOC;
        private System.Windows.Forms.Label lbltotalOCtitle;
        private System.Windows.Forms.ListView LvArticulos;
    }
}