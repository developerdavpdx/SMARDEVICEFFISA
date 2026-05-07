namespace FFISA.Inventarios.TransferenciaStock
{
    partial class TransferenciasPendientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferenciasPendientes));
            this.Footer = new System.Windows.Forms.Panel();
            this.Eliminar = new System.Windows.Forms.PictureBox();
            this.GenerarEntrega = new System.Windows.Forms.Button();
            this.Detalle = new System.Windows.Forms.PictureBox();
            this.Regresar = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Panel();
            this.LblTitleDocPend = new System.Windows.Forms.Label();
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
            this.Footer.Controls.Add(this.Eliminar);
            this.Footer.Controls.Add(this.GenerarEntrega);
            this.Footer.Controls.Add(this.Detalle);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 290);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(240, 30);
            // 
            // Eliminar
            // 
            this.Eliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Eliminar.Image = ((System.Drawing.Image)(resources.GetObject("Eliminar.Image")));
            this.Eliminar.Location = new System.Drawing.Point(8, 3);
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.Size = new System.Drawing.Size(25, 25);
            this.Eliminar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Eliminar.Click += new System.EventHandler(this.PtbEliminar_Click);
            // 
            // GenerarEntrega
            // 
            this.GenerarEntrega.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.GenerarEntrega.ForeColor = System.Drawing.Color.White;
            this.GenerarEntrega.Location = new System.Drawing.Point(117, 3);
            this.GenerarEntrega.Name = "GenerarEntrega";
            this.GenerarEntrega.Size = new System.Drawing.Size(118, 25);
            this.GenerarEntrega.TabIndex = 25;
            this.GenerarEntrega.Tag = "";
            this.GenerarEntrega.Text = "Transferencia SAP";
            this.GenerarEntrega.Click += new System.EventHandler(this.Generar_Click);
            // 
            // Detalle
            // 
            this.Detalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Detalle.Image = ((System.Drawing.Image)(resources.GetObject("Detalle.Image")));
            this.Detalle.Location = new System.Drawing.Point(42, 3);
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
            this.Header.Controls.Add(this.LblTitleDocPend);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(240, 30);
            // 
            // LblTitleDocPend
            // 
            this.LblTitleDocPend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.LblTitleDocPend.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LblTitleDocPend.ForeColor = System.Drawing.Color.White;
            this.LblTitleDocPend.Location = new System.Drawing.Point(7, 8);
            this.LblTitleDocPend.Name = "LblTitleDocPend";
            this.LblTitleDocPend.Size = new System.Drawing.Size(184, 15);
            this.LblTitleDocPend.Text = "DOCUMENTOS PENDIENTES";
            this.LblTitleDocPend.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.lblcompras.Location = new System.Drawing.Point(6, 13);
            this.lblcompras.Name = "lblcompras";
            this.lblcompras.Size = new System.Drawing.Size(229, 32);
            this.lblcompras.Text = "LISTADO DE DOCUMENTOS PENDIENTES PARA TRANSFERENCIAS";
            this.lblcompras.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ptbBuscar
            // 
            this.ptbBuscar.Image = ((System.Drawing.Image)(resources.GetObject("ptbBuscar.Image")));
            this.ptbBuscar.Location = new System.Drawing.Point(190, 79);
            this.ptbBuscar.Name = "ptbBuscar";
            this.ptbBuscar.Size = new System.Drawing.Size(25, 25);
            this.ptbBuscar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbBuscar.Click += new System.EventHandler(this.ptbBuscar_Click);
            // 
            // lblrangosfecha
            // 
            this.lblrangosfecha.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblrangosfecha.ForeColor = System.Drawing.Color.Gray;
            this.lblrangosfecha.Location = new System.Drawing.Point(8, 60);
            this.lblrangosfecha.Name = "lblrangosfecha";
            this.lblrangosfecha.Size = new System.Drawing.Size(110, 20);
            this.lblrangosfecha.Text = "Rangos de fecha";
            // 
            // FF
            // 
            this.FF.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FF.Location = new System.Drawing.Point(97, 80);
            this.FF.Name = "FF";
            this.FF.Size = new System.Drawing.Size(83, 22);
            this.FF.TabIndex = 30;
            this.FF.Value = new System.DateTime(2025, 2, 20, 0, 0, 0, 0);
            // 
            // FI
            // 
            this.FI.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FI.Location = new System.Drawing.Point(8, 80);
            this.FI.Name = "FI";
            this.FI.Size = new System.Drawing.Size(83, 22);
            this.FI.TabIndex = 29;
            this.FI.Value = new System.DateTime(2025, 2, 20, 0, 0, 0, 0);
            // 
            // LvDocumentosPendientes
            // 
            this.LvDocumentosPendientes.FullRowSelect = true;
            this.LvDocumentosPendientes.Location = new System.Drawing.Point(6, 113);
            this.LvDocumentosPendientes.Name = "LvDocumentosPendientes";
            this.LvDocumentosPendientes.Size = new System.Drawing.Size(229, 134);
            this.LvDocumentosPendientes.TabIndex = 28;
            this.LvDocumentosPendientes.View = System.Windows.Forms.View.Details;
            this.LvDocumentosPendientes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LvEntradas_KeyPress);
            // 
            // TransferenciasPendientes
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
            this.Name = "TransferenciasPendientes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Footer.ResumeLayout(false);
            this.Header.ResumeLayout(false);
            this.Body.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Header;
        public System.Windows.Forms.PictureBox Regresar;
        public System.Windows.Forms.PictureBox Detalle;
        public System.Windows.Forms.PictureBox Eliminar;
        public System.Windows.Forms.Button GenerarEntrega;
        public System.Windows.Forms.Panel Footer;
        public System.Windows.Forms.Label LblTitleDocPend;
        private System.Windows.Forms.Panel Body;
        private System.Windows.Forms.Label lblcompras;
        private System.Windows.Forms.PictureBox ptbBuscar;
        private System.Windows.Forms.Label lblrangosfecha;
        private System.Windows.Forms.DateTimePicker FF;
        private System.Windows.Forms.DateTimePicker FI;
        private System.Windows.Forms.ListView LvDocumentosPendientes;

    }
}