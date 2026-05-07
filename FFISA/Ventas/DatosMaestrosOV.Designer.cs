namespace FFISA.Ventas
{
    partial class DatosMaestrosOV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatosMaestrosOV));
            this.Footer = new System.Windows.Forms.Panel();
            this.MenuVentas = new System.Windows.Forms.PictureBox();
            this.Guardar = new System.Windows.Forms.PictureBox();
            this.Regresar = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Panel();
            this.LblOrdenVenta = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.LblUsuario = new System.Windows.Forms.Label();
            this.TxtUsuarioVAL = new System.Windows.Forms.TextBox();
            this.LblComentarios = new System.Windows.Forms.Label();
            this.LblCliente = new System.Windows.Forms.Label();
            this.LblOrdenVentaTitle = new System.Windows.Forms.Label();
            this.TxtOrdenVentaVAL = new System.Windows.Forms.TextBox();
            this.TxtComentariosVALCLN = new System.Windows.Forms.TextBox();
            this.TxtClienteVAL = new System.Windows.Forms.TextBox();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Footer.Controls.Add(this.MenuVentas);
            this.Footer.Controls.Add(this.Guardar);
            this.Footer.Controls.Add(this.Regresar);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 290);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(240, 30);
            // 
            // MenuVentas
            // 
            this.MenuVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.MenuVentas.Image = ((System.Drawing.Image)(resources.GetObject("MenuVentas.Image")));
            this.MenuVentas.Location = new System.Drawing.Point(108, 1);
            this.MenuVentas.Name = "MenuVentas";
            this.MenuVentas.Size = new System.Drawing.Size(29, 29);
            this.MenuVentas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MenuVentas.Click += new System.EventHandler(this.MenuEntregas_Click);
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
            this.LblOrdenVenta.Text = "ORDEN VENTA";
            this.LblOrdenVenta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.LblUsuario);
            this.Body.Controls.Add(this.TxtUsuarioVAL);
            this.Body.Controls.Add(this.LblComentarios);
            this.Body.Controls.Add(this.LblCliente);
            this.Body.Controls.Add(this.LblOrdenVentaTitle);
            this.Body.Controls.Add(this.TxtOrdenVentaVAL);
            this.Body.Controls.Add(this.TxtComentariosVALCLN);
            this.Body.Controls.Add(this.TxtClienteVAL);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // LblUsuario
            // 
            this.LblUsuario.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblUsuario.ForeColor = System.Drawing.Color.Gray;
            this.LblUsuario.Location = new System.Drawing.Point(7, 136);
            this.LblUsuario.Name = "LblUsuario";
            this.LblUsuario.Size = new System.Drawing.Size(110, 20);
            this.LblUsuario.Text = "Usuario";
            // 
            // TxtUsuarioVAL
            // 
            this.TxtUsuarioVAL.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtUsuarioVAL.Location = new System.Drawing.Point(7, 159);
            this.TxtUsuarioVAL.Name = "TxtUsuarioVAL";
            this.TxtUsuarioVAL.Size = new System.Drawing.Size(227, 21);
            this.TxtUsuarioVAL.TabIndex = 24;
            // 
            // LblComentarios
            // 
            this.LblComentarios.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblComentarios.ForeColor = System.Drawing.Color.Gray;
            this.LblComentarios.Location = new System.Drawing.Point(9, 194);
            this.LblComentarios.Name = "LblComentarios";
            this.LblComentarios.Size = new System.Drawing.Size(110, 20);
            this.LblComentarios.Text = "Comentario";
            // 
            // LblCliente
            // 
            this.LblCliente.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblCliente.ForeColor = System.Drawing.Color.Gray;
            this.LblCliente.Location = new System.Drawing.Point(9, 76);
            this.LblCliente.Name = "LblCliente";
            this.LblCliente.Size = new System.Drawing.Size(110, 20);
            this.LblCliente.Text = "Cliente";
            // 
            // LblOrdenVentaTitle
            // 
            this.LblOrdenVentaTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblOrdenVentaTitle.ForeColor = System.Drawing.Color.Gray;
            this.LblOrdenVentaTitle.Location = new System.Drawing.Point(9, 23);
            this.LblOrdenVentaTitle.Name = "LblOrdenVentaTitle";
            this.LblOrdenVentaTitle.Size = new System.Drawing.Size(114, 20);
            this.LblOrdenVentaTitle.Text = "Orden de venta";
            // 
            // TxtOrdenVentaVAL
            // 
            this.TxtOrdenVentaVAL.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtOrdenVentaVAL.Enabled = false;
            this.TxtOrdenVentaVAL.Location = new System.Drawing.Point(9, 46);
            this.TxtOrdenVentaVAL.Name = "TxtOrdenVentaVAL";
            this.TxtOrdenVentaVAL.ReadOnly = true;
            this.TxtOrdenVentaVAL.Size = new System.Drawing.Size(225, 21);
            this.TxtOrdenVentaVAL.TabIndex = 22;
            // 
            // TxtComentariosVALCLN
            // 
            this.TxtComentariosVALCLN.Location = new System.Drawing.Point(9, 217);
            this.TxtComentariosVALCLN.Name = "TxtComentariosVALCLN";
            this.TxtComentariosVALCLN.Size = new System.Drawing.Size(225, 21);
            this.TxtComentariosVALCLN.TabIndex = 25;
            // 
            // TxtClienteVAL
            // 
            this.TxtClienteVAL.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtClienteVAL.Location = new System.Drawing.Point(9, 99);
            this.TxtClienteVAL.Name = "TxtClienteVAL";
            this.TxtClienteVAL.Size = new System.Drawing.Size(225, 21);
            this.TxtClienteVAL.TabIndex = 23;
            // 
            // DatosMaestrosOV
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
            this.Name = "DatosMaestrosOV";
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
        private System.Windows.Forms.Label LblOrdenVenta;
        public System.Windows.Forms.PictureBox MenuVentas;
        private System.Windows.Forms.Panel Body;
        private System.Windows.Forms.Label LblUsuario;
        private System.Windows.Forms.TextBox TxtUsuarioVAL;
        private System.Windows.Forms.Label LblComentarios;
        private System.Windows.Forms.Label LblCliente;
        private System.Windows.Forms.Label LblOrdenVentaTitle;
        private System.Windows.Forms.TextBox TxtOrdenVentaVAL;
        private System.Windows.Forms.TextBox TxtComentariosVALCLN;
        private System.Windows.Forms.TextBox TxtClienteVAL;
    }
}