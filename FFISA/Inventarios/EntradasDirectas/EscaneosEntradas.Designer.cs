namespace FFISA.Inventarios.EntradasDirectas
{
    partial class EscaneosEntradas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EscaneosEntradas));
            this.Footer = new System.Windows.Forms.Panel();
            this.PtbEliminar = new System.Windows.Forms.PictureBox();
            this.Regresar = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.lblOC = new System.Windows.Forms.Label();
            this.LvDetailsEntrada = new System.Windows.Forms.ListView();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Footer.Controls.Add(this.PtbEliminar);
            this.Footer.Controls.Add(this.Regresar);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 290);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(240, 30);
            // 
            // PtbEliminar
            // 
            this.PtbEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.PtbEliminar.Image = ((System.Drawing.Image)(resources.GetObject("PtbEliminar.Image")));
            this.PtbEliminar.Location = new System.Drawing.Point(208, 3);
            this.PtbEliminar.Name = "PtbEliminar";
            this.PtbEliminar.Size = new System.Drawing.Size(25, 25);
            this.PtbEliminar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PtbEliminar.Click += new System.EventHandler(this.PtbEliminar_Click);
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
            this.label6.Location = new System.Drawing.Point(6, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(229, 15);
            this.label6.Text = "DETALLE DOCUMENTO";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.lblOC);
            this.Body.Controls.Add(this.LvDetailsEntrada);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // lblOC
            // 
            this.lblOC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblOC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(118)))), ((int)(((byte)(189)))));
            this.lblOC.Location = new System.Drawing.Point(6, 7);
            this.lblOC.Name = "lblOC";
            this.lblOC.Size = new System.Drawing.Size(166, 20);
            this.lblOC.Text = "FOLIO: XXXXXX";
            // 
            // LvDetailsEntrada
            // 
            this.LvDetailsEntrada.FullRowSelect = true;
            this.LvDetailsEntrada.Location = new System.Drawing.Point(6, 36);
            this.LvDetailsEntrada.Name = "LvDetailsEntrada";
            this.LvDetailsEntrada.Size = new System.Drawing.Size(229, 180);
            this.LvDetailsEntrada.TabIndex = 21;
            this.LvDetailsEntrada.View = System.Windows.Forms.View.Details;
            // 
            // EscaneosEntradas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.Body);
            this.Controls.Add(this.Footer);
            this.Controls.Add(this.Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "EscaneosEntradas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Footer.ResumeLayout(false);
            this.Header.ResumeLayout(false);
            this.Body.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Footer;
        private System.Windows.Forms.Panel Header;
        public System.Windows.Forms.PictureBox Regresar;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.PictureBox PtbEliminar;
        private System.Windows.Forms.Panel Body;
        private System.Windows.Forms.Label lblOC;
        private System.Windows.Forms.ListView LvDetailsEntrada;
    }
}