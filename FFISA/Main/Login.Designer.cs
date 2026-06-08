namespace FFISA.Main
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.Body = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PtbPBP = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.exit = new System.Windows.Forms.PictureBox();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.txtusuario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PtbFiber = new System.Windows.Forms.PictureBox();
            this.btnLoginENB = new System.Windows.Forms.Button();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.White;
            this.Body.Controls.Add(this.pictureBox1);
            this.Body.Controls.Add(this.PtbPBP);
            this.Body.Controls.Add(this.lblVersion);
            this.Body.Controls.Add(this.exit);
            this.Body.Controls.Add(this.txtpassword);
            this.Body.Controls.Add(this.txtusuario);
            this.Body.Controls.Add(this.label2);
            this.Body.Controls.Add(this.label1);
            this.Body.Controls.Add(this.PtbFiber);
            this.Body.Controls.Add(this.btnLoginENB);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 0);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(240, 320);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // PtbPBP
            // 
            this.PtbPBP.BackColor = System.Drawing.Color.Transparent;
            this.PtbPBP.Image = ((System.Drawing.Image)(resources.GetObject("PtbPBP.Image")));
            this.PtbPBP.Location = new System.Drawing.Point(68, 252);
            this.PtbPBP.Name = "PtbPBP";
            this.PtbPBP.Size = new System.Drawing.Size(110, 30);
            this.PtbPBP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lblVersion
            // 
            this.lblVersion.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblVersion.Location = new System.Drawing.Point(77, 298);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(161, 20);
            this.lblVersion.Text = "FFISA PRODUCTIVO 1.88";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // exit
            // 
            this.exit.Image = ((System.Drawing.Image)(resources.GetObject("exit.Image")));
            this.exit.Location = new System.Drawing.Point(213, 2);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(25, 25);
            this.exit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // txtpassword
            // 
            this.txtpassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtpassword.Location = new System.Drawing.Point(42, 166);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.PasswordChar = '*';
            this.txtpassword.Size = new System.Drawing.Size(153, 21);
            this.txtpassword.TabIndex = 16;
            // 
            // txtusuario
            // 
            this.txtusuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtusuario.Location = new System.Drawing.Point(42, 125);
            this.txtusuario.Name = "txtusuario";
            this.txtusuario.Size = new System.Drawing.Size(153, 21);
            this.txtusuario.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(40, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.Text = "Contraseña";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(42, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 14);
            this.label1.Text = "Usuario";
            // 
            // PtbFiber
            // 
            this.PtbFiber.BackColor = System.Drawing.Color.Transparent;
            this.PtbFiber.Image = ((System.Drawing.Image)(resources.GetObject("PtbFiber.Image")));
            this.PtbFiber.Location = new System.Drawing.Point(90, 50);
            this.PtbFiber.Name = "PtbFiber";
            this.PtbFiber.Size = new System.Drawing.Size(52, 52);
            this.PtbFiber.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // btnLoginENB
            // 
            this.btnLoginENB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnLoginENB.ForeColor = System.Drawing.Color.White;
            this.btnLoginENB.Location = new System.Drawing.Point(90, 199);
            this.btnLoginENB.Name = "btnLoginENB";
            this.btnLoginENB.Size = new System.Drawing.Size(72, 35);
            this.btnLoginENB.TabIndex = 10;
            this.btnLoginENB.Tag = "disabled";
            this.btnLoginENB.Text = "Login";
            this.btnLoginENB.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.Body);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Login";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Body.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Body;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox PtbPBP;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.PictureBox exit;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.TextBox txtusuario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PtbFiber;
        private System.Windows.Forms.Button btnLoginENB;

    }
}