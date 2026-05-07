namespace FFISA.Main
{
    partial class MenuPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrincipal));
            this.LblMainTitle = new System.Windows.Forms.Label();
            this.Footer = new System.Windows.Forms.Panel();
            this.ImpresionENB = new System.Windows.Forms.PictureBox();
            this.RegresarENB = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Panel();
            this.LblUsuario = new System.Windows.Forms.Label();
            this.LblPerfilUsuario = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.Produccion = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblcompras = new System.Windows.Forms.Label();
            this.Inventarios = new System.Windows.Forms.PictureBox();
            this.Ventas = new System.Windows.Forms.PictureBox();
            this.Compras = new System.Windows.Forms.PictureBox();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblMainTitle
            // 
            this.LblMainTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.LblMainTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblMainTitle.ForeColor = System.Drawing.Color.White;
            this.LblMainTitle.Location = new System.Drawing.Point(49, 12);
            this.LblMainTitle.Name = "LblMainTitle";
            this.LblMainTitle.Size = new System.Drawing.Size(143, 15);
            this.LblMainTitle.Text = "Sociedad";
            this.LblMainTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Footer.Controls.Add(this.ImpresionENB);
            this.Footer.Controls.Add(this.RegresarENB);
            this.Footer.Controls.Add(this.LblMainTitle);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 290);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(240, 30);
            // 
            // ImpresionENB
            // 
            this.ImpresionENB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.ImpresionENB.Image = ((System.Drawing.Image)(resources.GetObject("ImpresionENB.Image")));
            this.ImpresionENB.Location = new System.Drawing.Point(206, 3);
            this.ImpresionENB.Name = "ImpresionENB";
            this.ImpresionENB.Size = new System.Drawing.Size(25, 25);
            this.ImpresionENB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImpresionENB.Click += new System.EventHandler(this.Impresion_Click);
            // 
            // RegresarENB
            // 
            this.RegresarENB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.RegresarENB.Image = ((System.Drawing.Image)(resources.GetObject("RegresarENB.Image")));
            this.RegresarENB.Location = new System.Drawing.Point(4, 3);
            this.RegresarENB.Name = "RegresarENB";
            this.RegresarENB.Size = new System.Drawing.Size(25, 25);
            this.RegresarENB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.RegresarENB.Click += new System.EventHandler(this.Regresar_Click);
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Header.Controls.Add(this.LblUsuario);
            this.Header.Controls.Add(this.LblPerfilUsuario);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(240, 30);
            // 
            // LblUsuario
            // 
            this.LblUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.LblUsuario.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Bold);
            this.LblUsuario.ForeColor = System.Drawing.Color.White;
            this.LblUsuario.Location = new System.Drawing.Point(4, 3);
            this.LblUsuario.Name = "LblUsuario";
            this.LblUsuario.Size = new System.Drawing.Size(154, 24);
            this.LblUsuario.Text = "Bienvenido planfis.fisfiber@gmail.com";
            // 
            // LblPerfilUsuario
            // 
            this.LblPerfilUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.LblPerfilUsuario.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Bold);
            this.LblPerfilUsuario.ForeColor = System.Drawing.Color.White;
            this.LblPerfilUsuario.Location = new System.Drawing.Point(164, 3);
            this.LblPerfilUsuario.Name = "LblPerfilUsuario";
            this.LblPerfilUsuario.Size = new System.Drawing.Size(72, 24);
            this.LblPerfilUsuario.Text = "Perfil";
            this.LblPerfilUsuario.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.Produccion);
            this.Body.Controls.Add(this.label3);
            this.Body.Controls.Add(this.label2);
            this.Body.Controls.Add(this.label1);
            this.Body.Controls.Add(this.lblcompras);
            this.Body.Controls.Add(this.Inventarios);
            this.Body.Controls.Add(this.Ventas);
            this.Body.Controls.Add(this.Compras);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // Produccion
            // 
            this.Produccion.Image = ((System.Drawing.Image)(resources.GetObject("Produccion.Image")));
            this.Produccion.Location = new System.Drawing.Point(34, 168);
            this.Produccion.Name = "Produccion";
            this.Produccion.Size = new System.Drawing.Size(58, 58);
            this.Produccion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(29, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.Text = "Producción";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(134, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.Text = "Inventarios";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(143, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 20);
            this.label1.Text = "Ventas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblcompras
            // 
            this.lblcompras.BackColor = System.Drawing.Color.Transparent;
            this.lblcompras.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblcompras.ForeColor = System.Drawing.Color.Gray;
            this.lblcompras.Location = new System.Drawing.Point(30, 34);
            this.lblcompras.Name = "lblcompras";
            this.lblcompras.Size = new System.Drawing.Size(65, 20);
            this.lblcompras.Text = "Compras";
            this.lblcompras.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Inventarios
            // 
            this.Inventarios.Image = ((System.Drawing.Image)(resources.GetObject("Inventarios.Image")));
            this.Inventarios.Location = new System.Drawing.Point(143, 168);
            this.Inventarios.Name = "Inventarios";
            this.Inventarios.Size = new System.Drawing.Size(58, 58);
            this.Inventarios.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Inventarios.Click += new System.EventHandler(this.Inventarios_Click);
            // 
            // Ventas
            // 
            this.Ventas.Image = ((System.Drawing.Image)(resources.GetObject("Ventas.Image")));
            this.Ventas.Location = new System.Drawing.Point(143, 57);
            this.Ventas.Name = "Ventas";
            this.Ventas.Size = new System.Drawing.Size(58, 58);
            this.Ventas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Ventas.Click += new System.EventHandler(this.Ventas_Click);
            // 
            // Compras
            // 
            this.Compras.Image = ((System.Drawing.Image)(resources.GetObject("Compras.Image")));
            this.Compras.Location = new System.Drawing.Point(34, 57);
            this.Compras.Name = "Compras";
            this.Compras.Size = new System.Drawing.Size(58, 58);
            this.Compras.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Compras.Click += new System.EventHandler(this.Compras_Click);
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.Body);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.Footer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "MenuPrincipal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Footer.ResumeLayout(false);
            this.Header.ResumeLayout(false);
            this.Body.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Footer;
        private System.Windows.Forms.Panel Header;
        public System.Windows.Forms.PictureBox RegresarENB;
        public System.Windows.Forms.PictureBox ImpresionENB;
        public System.Windows.Forms.Label LblMainTitle;
        public System.Windows.Forms.Label LblPerfilUsuario;
        public System.Windows.Forms.Label LblUsuario;
        private System.Windows.Forms.Panel Body;
        private System.Windows.Forms.PictureBox Produccion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblcompras;
        private System.Windows.Forms.PictureBox Inventarios;
        private System.Windows.Forms.PictureBox Ventas;
        private System.Windows.Forms.PictureBox Compras;

    }
}