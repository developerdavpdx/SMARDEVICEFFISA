namespace FFISA.Inventarios
{
    partial class MenuInventarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuInventarios));
            this.Footer = new System.Windows.Forms.Panel();
            this.PlantillaImpresion = new System.Windows.Forms.PictureBox();
            this.Regresar = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Panel();
            this.Lbltitle = new System.Windows.Forms.Label();
            this.lblOC = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.TraspasoMercancia = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ED = new System.Windows.Forms.Label();
            this.RecuentoInventarios = new System.Windows.Forms.PictureBox();
            this.SalidaDirecta = new System.Windows.Forms.PictureBox();
            this.EntradaDirecta = new System.Windows.Forms.PictureBox();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Footer.Controls.Add(this.PlantillaImpresion);
            this.Footer.Controls.Add(this.Regresar);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 290);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(240, 30);
            // 
            // PlantillaImpresion
            // 
            this.PlantillaImpresion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.PlantillaImpresion.Image = ((System.Drawing.Image)(resources.GetObject("PlantillaImpresion.Image")));
            this.PlantillaImpresion.Location = new System.Drawing.Point(206, 1);
            this.PlantillaImpresion.Name = "PlantillaImpresion";
            this.PlantillaImpresion.Size = new System.Drawing.Size(31, 28);
            this.PlantillaImpresion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PlantillaImpresion.Click += new System.EventHandler(this.PlantillaImpresion_Click);
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
            this.Lbltitle.Location = new System.Drawing.Point(4, 6);
            this.Lbltitle.Name = "Lbltitle";
            this.Lbltitle.Size = new System.Drawing.Size(233, 20);
            this.Lbltitle.Text = "INVENTARIOS";
            this.Lbltitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblOC
            // 
            this.lblOC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblOC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(118)))), ((int)(((byte)(189)))));
            this.lblOC.Location = new System.Drawing.Point(4, 33);
            this.lblOC.Name = "lblOC";
            this.lblOC.Size = new System.Drawing.Size(233, 15);
            this.lblOC.Text = "SELECCIONE LA OPCIÓN DESEADA";
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.TraspasoMercancia);
            this.Body.Controls.Add(this.label3);
            this.Body.Controls.Add(this.label2);
            this.Body.Controls.Add(this.label1);
            this.Body.Controls.Add(this.ED);
            this.Body.Controls.Add(this.RecuentoInventarios);
            this.Body.Controls.Add(this.SalidaDirecta);
            this.Body.Controls.Add(this.EntradaDirecta);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // TraspasoMercancia
            // 
            this.TraspasoMercancia.Image = ((System.Drawing.Image)(resources.GetObject("TraspasoMercancia.Image")));
            this.TraspasoMercancia.Location = new System.Drawing.Point(23, 166);
            this.TraspasoMercancia.Name = "TraspasoMercancia";
            this.TraspasoMercancia.Size = new System.Drawing.Size(80, 70);
            this.TraspasoMercancia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TraspasoMercancia.Click += new System.EventHandler(this.TraspasoMercancia_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(31, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 28);
            this.label3.Text = "Traspaso Mercancía";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(135, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 28);
            this.label2.Text = "Recuento Inventarios";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(131, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.Text = "Salida Directa";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ED
            // 
            this.ED.BackColor = System.Drawing.Color.Transparent;
            this.ED.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.ED.ForeColor = System.Drawing.Color.Gray;
            this.ED.Location = new System.Drawing.Point(15, 24);
            this.ED.Name = "ED";
            this.ED.Size = new System.Drawing.Size(107, 20);
            this.ED.Text = "Entrada Directa";
            this.ED.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // RecuentoInventarios
            // 
            this.RecuentoInventarios.Image = ((System.Drawing.Image)(resources.GetObject("RecuentoInventarios.Image")));
            this.RecuentoInventarios.Location = new System.Drawing.Point(137, 166);
            this.RecuentoInventarios.Name = "RecuentoInventarios";
            this.RecuentoInventarios.Size = new System.Drawing.Size(80, 70);
            this.RecuentoInventarios.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.RecuentoInventarios.Click += new System.EventHandler(this.RecuentoInventarios_Click);
            // 
            // SalidaDirecta
            // 
            this.SalidaDirecta.Image = ((System.Drawing.Image)(resources.GetObject("SalidaDirecta.Image")));
            this.SalidaDirecta.Location = new System.Drawing.Point(137, 47);
            this.SalidaDirecta.Name = "SalidaDirecta";
            this.SalidaDirecta.Size = new System.Drawing.Size(80, 70);
            this.SalidaDirecta.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SalidaDirecta.Click += new System.EventHandler(this.SalidaDirecta_Click);
            // 
            // EntradaDirecta
            // 
            this.EntradaDirecta.Image = ((System.Drawing.Image)(resources.GetObject("EntradaDirecta.Image")));
            this.EntradaDirecta.Location = new System.Drawing.Point(23, 47);
            this.EntradaDirecta.Name = "EntradaDirecta";
            this.EntradaDirecta.Size = new System.Drawing.Size(80, 70);
            this.EntradaDirecta.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.EntradaDirecta.Click += new System.EventHandler(this.EntradaDirecta_Click);
            // 
            // MenuInventarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.Body);
            this.Controls.Add(this.lblOC);
            this.Controls.Add(this.Footer);
            this.Controls.Add(this.Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "MenuInventarios";
            this.Text = "MenuVentas";
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
        private System.Windows.Forms.Label lblOC;
        public System.Windows.Forms.PictureBox PlantillaImpresion;
        private System.Windows.Forms.Panel Body;
        private System.Windows.Forms.PictureBox TraspasoMercancia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ED;
        private System.Windows.Forms.PictureBox RecuentoInventarios;
        private System.Windows.Forms.PictureBox SalidaDirecta;
        private System.Windows.Forms.PictureBox EntradaDirecta;
    }
}