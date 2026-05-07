namespace FFISA.Compras
{
    partial class ListadoEMPendientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListadoEMPendientes));
            this.Footer = new System.Windows.Forms.Panel();
            this.PtbEliminar = new System.Windows.Forms.PictureBox();
            this.GenerarEntrada = new System.Windows.Forms.Button();
            this.Detalle = new System.Windows.Forms.PictureBox();
            this.Regresar = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.Body = new System.Windows.Forms.Panel();
            this.lblcompras = new System.Windows.Forms.Label();
            this.ptbBuscar = new System.Windows.Forms.PictureBox();
            this.lblrangosfecha = new System.Windows.Forms.Label();
            this.FF = new System.Windows.Forms.DateTimePicker();
            this.FI = new System.Windows.Forms.DateTimePicker();
            this.LvDocumentosPendientes = new System.Windows.Forms.ListView();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Footer.Controls.Add(this.PtbEliminar);
            this.Footer.Controls.Add(this.GenerarEntrada);
            this.Footer.Controls.Add(this.Detalle);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 290);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(240, 30);
            // 
            // PtbEliminar
            // 
            this.PtbEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.PtbEliminar.Image = ((System.Drawing.Image)(resources.GetObject("PtbEliminar.Image")));
            this.PtbEliminar.Location = new System.Drawing.Point(8, 3);
            this.PtbEliminar.Name = "PtbEliminar";
            this.PtbEliminar.Size = new System.Drawing.Size(25, 25);
            this.PtbEliminar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PtbEliminar.Click += new System.EventHandler(this.PtbEliminar_Click);
            // 
            // GenerarEntrada
            // 
            this.GenerarEntrada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.GenerarEntrada.ForeColor = System.Drawing.Color.White;
            this.GenerarEntrada.Location = new System.Drawing.Point(89, 3);
            this.GenerarEntrada.Name = "GenerarEntrada";
            this.GenerarEntrada.Size = new System.Drawing.Size(63, 25);
            this.GenerarEntrada.TabIndex = 25;
            this.GenerarEntrada.Tag = "";
            this.GenerarEntrada.Text = "Entrada";
            this.GenerarEntrada.Click += new System.EventHandler(this.Generar_Click);
            // 
            // Detalle
            // 
            this.Detalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Detalle.Image = ((System.Drawing.Image)(resources.GetObject("Detalle.Image")));
            this.Detalle.Location = new System.Drawing.Point(208, 3);
            this.Detalle.Name = "Detalle";
            this.Detalle.Size = new System.Drawing.Size(25, 25);
            this.Detalle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Detalle.Click += new System.EventHandler(this.Detalle_Click);
            // 
            // Regresar
            // 
            this.Regresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Regresar.Image = ((System.Drawing.Image)(resources.GetObject("Regresar.Image")));
            this.Regresar.Location = new System.Drawing.Point(208, 3);
            this.Regresar.Name = "Regresar";
            this.Regresar.Size = new System.Drawing.Size(25, 25);
            this.Regresar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Regresar.Click += new System.EventHandler(this.Regresar_Click);
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Header.Controls.Add(this.Regresar);
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
            this.label6.Size = new System.Drawing.Size(184, 15);
            this.label6.Text = "DOCUMENTOS PENDIENTES";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.lblcompras);
            this.Body.Controls.Add(this.ptbBuscar);
            this.Body.Controls.Add(this.lblrangosfecha);
            this.Body.Controls.Add(this.FF);
            this.Body.Controls.Add(this.FI);
            this.Body.Controls.Add(this.LvDocumentosPendientes);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // lblcompras
            // 
            this.lblcompras.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblcompras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(118)))), ((int)(((byte)(189)))));
            this.lblcompras.Location = new System.Drawing.Point(6, 5);
            this.lblcompras.Name = "lblcompras";
            this.lblcompras.Size = new System.Drawing.Size(229, 20);
            this.lblcompras.Text = "LISTADO DE DOCUMENTOS";
            // 
            // ptbBuscar
            // 
            this.ptbBuscar.Image = ((System.Drawing.Image)(resources.GetObject("ptbBuscar.Image")));
            this.ptbBuscar.Location = new System.Drawing.Point(190, 56);
            this.ptbBuscar.Name = "ptbBuscar";
            this.ptbBuscar.Size = new System.Drawing.Size(25, 25);
            this.ptbBuscar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbBuscar.Click += new System.EventHandler(this.ptbBuscar_Click);
            // 
            // lblrangosfecha
            // 
            this.lblrangosfecha.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblrangosfecha.ForeColor = System.Drawing.Color.Gray;
            this.lblrangosfecha.Location = new System.Drawing.Point(8, 37);
            this.lblrangosfecha.Name = "lblrangosfecha";
            this.lblrangosfecha.Size = new System.Drawing.Size(110, 20);
            this.lblrangosfecha.Text = "Rangos de fecha";
            // 
            // FF
            // 
            this.FF.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FF.Location = new System.Drawing.Point(97, 57);
            this.FF.Name = "FF";
            this.FF.Size = new System.Drawing.Size(83, 22);
            this.FF.TabIndex = 30;
            this.FF.Value = new System.DateTime(2025, 2, 20, 0, 0, 0, 0);
            // 
            // FI
            // 
            this.FI.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FI.Location = new System.Drawing.Point(8, 57);
            this.FI.Name = "FI";
            this.FI.Size = new System.Drawing.Size(83, 22);
            this.FI.TabIndex = 29;
            this.FI.Value = new System.DateTime(2025, 2, 20, 0, 0, 0, 0);
            // 
            // LvDocumentosPendientes
            // 
            this.LvDocumentosPendientes.FullRowSelect = true;
            this.LvDocumentosPendientes.Location = new System.Drawing.Point(6, 90);
            this.LvDocumentosPendientes.Name = "LvDocumentosPendientes";
            this.LvDocumentosPendientes.Size = new System.Drawing.Size(229, 134);
            this.LvDocumentosPendientes.TabIndex = 28;
            this.LvDocumentosPendientes.View = System.Windows.Forms.View.Details;
            this.LvDocumentosPendientes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LvOrdenesCompra_KeyPress);
            // 
            // ListadoEMPendientes
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
            this.Name = "ListadoEMPendientes";
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
        public System.Windows.Forms.PictureBox Detalle;
        private System.Windows.Forms.Button GenerarEntrada;
        public System.Windows.Forms.PictureBox PtbEliminar;
        private System.Windows.Forms.Panel Body;
        private System.Windows.Forms.Label lblcompras;
        private System.Windows.Forms.PictureBox ptbBuscar;
        private System.Windows.Forms.Label lblrangosfecha;
        private System.Windows.Forms.DateTimePicker FF;
        private System.Windows.Forms.DateTimePicker FI;
        private System.Windows.Forms.ListView LvDocumentosPendientes;

    }
}