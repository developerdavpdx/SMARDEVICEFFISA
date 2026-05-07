namespace FFISA.Compras
{
    partial class CheckListOC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckListOC));
            this.Header = new System.Windows.Forms.Panel();
            this.MenuCompras = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Footer = new System.Windows.Forms.Panel();
            this.Autorizacion = new System.Windows.Forms.Button();
            this.Continuar = new System.Windows.Forms.PictureBox();
            this.Regresar = new System.Windows.Forms.PictureBox();
            this.Body = new System.Windows.Forms.Panel();
            this.SeleccionarTodos = new System.Windows.Forms.CheckBox();
            this.lblOC = new System.Windows.Forms.Label();
            this.ptbstatus = new System.Windows.Forms.PictureBox();
            this.lblestatusOC = new System.Windows.Forms.Label();
            this.pnlCheckListOC = new System.Windows.Forms.Panel();
            this.Header.SuspendLayout();
            this.Footer.SuspendLayout();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Header.Controls.Add(this.MenuCompras);
            this.Header.Controls.Add(this.label6);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(240, 30);
            // 
            // MenuCompras
            // 
            this.MenuCompras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.MenuCompras.Image = ((System.Drawing.Image)(resources.GetObject("MenuCompras.Image")));
            this.MenuCompras.Location = new System.Drawing.Point(4, 1);
            this.MenuCompras.Name = "MenuCompras";
            this.MenuCompras.Size = new System.Drawing.Size(29, 29);
            this.MenuCompras.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MenuCompras.Click += new System.EventHandler(this.MenuCompras_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(40, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(177, 14);
            this.label6.Text = "CHECKLIST";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Footer.Controls.Add(this.Autorizacion);
            this.Footer.Controls.Add(this.Continuar);
            this.Footer.Controls.Add(this.Regresar);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 290);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(240, 30);
            // 
            // Autorizacion
            // 
            this.Autorizacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Autorizacion.ForeColor = System.Drawing.Color.White;
            this.Autorizacion.Location = new System.Drawing.Point(75, 3);
            this.Autorizacion.Name = "Autorizacion";
            this.Autorizacion.Size = new System.Drawing.Size(92, 25);
            this.Autorizacion.TabIndex = 16;
            this.Autorizacion.Tag = "";
            this.Autorizacion.Text = "Autorización";
            this.Autorizacion.Click += new System.EventHandler(this.Autorizacion_Click);
            // 
            // Continuar
            // 
            this.Continuar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.Continuar.Enabled = false;
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
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.SeleccionarTodos);
            this.Body.Controls.Add(this.lblOC);
            this.Body.Controls.Add(this.ptbstatus);
            this.Body.Controls.Add(this.lblestatusOC);
            this.Body.Controls.Add(this.pnlCheckListOC);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 30);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 260);
            // 
            // SeleccionarTodos
            // 
            this.SeleccionarTodos.BackColor = System.Drawing.Color.Transparent;
            this.SeleccionarTodos.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.SeleccionarTodos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(118)))), ((int)(((byte)(189)))));
            this.SeleccionarTodos.Location = new System.Drawing.Point(8, 21);
            this.SeleccionarTodos.Name = "SeleccionarTodos";
            this.SeleccionarTodos.Size = new System.Drawing.Size(225, 20);
            this.SeleccionarTodos.TabIndex = 16;
            this.SeleccionarTodos.Text = "Seleccionar todos";
            this.SeleccionarTodos.Click += new System.EventHandler(this.SeleccionarTodos_Click);
            // 
            // lblOC
            // 
            this.lblOC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblOC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(118)))), ((int)(((byte)(189)))));
            this.lblOC.Location = new System.Drawing.Point(8, 3);
            this.lblOC.Name = "lblOC";
            this.lblOC.Size = new System.Drawing.Size(225, 16);
            this.lblOC.Text = "OC: XXXX";
            // 
            // ptbstatus
            // 
            this.ptbstatus.Image = ((System.Drawing.Image)(resources.GetObject("ptbstatus.Image")));
            this.ptbstatus.Location = new System.Drawing.Point(8, 186);
            this.ptbstatus.Name = "ptbstatus";
            this.ptbstatus.Size = new System.Drawing.Size(25, 25);
            this.ptbstatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbstatus.Visible = false;
            // 
            // lblestatusOC
            // 
            this.lblestatusOC.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblestatusOC.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblestatusOC.Location = new System.Drawing.Point(40, 192);
            this.lblestatusOC.Name = "lblestatusOC";
            this.lblestatusOC.Size = new System.Drawing.Size(193, 66);
            this.lblestatusOC.Text = "* Checklist OC";
            this.lblestatusOC.Visible = false;
            // 
            // pnlCheckListOC
            // 
            this.pnlCheckListOC.BackColor = System.Drawing.Color.White;
            this.pnlCheckListOC.Location = new System.Drawing.Point(8, 43);
            this.pnlCheckListOC.Name = "pnlCheckListOC";
            this.pnlCheckListOC.Size = new System.Drawing.Size(225, 140);
            // 
            // CheckListOC
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
            this.Name = "CheckListOC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Header.ResumeLayout(false);
            this.Footer.ResumeLayout(false);
            this.Body.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel Footer;
        public System.Windows.Forms.PictureBox Continuar;
        public System.Windows.Forms.PictureBox Regresar;
        private System.Windows.Forms.Button Autorizacion;
        public System.Windows.Forms.PictureBox MenuCompras;
        private System.Windows.Forms.Panel Body;
        private System.Windows.Forms.CheckBox SeleccionarTodos;
        private System.Windows.Forms.Label lblOC;
        private System.Windows.Forms.PictureBox ptbstatus;
        private System.Windows.Forms.Label lblestatusOC;
        private System.Windows.Forms.Panel pnlCheckListOC;
    }
}