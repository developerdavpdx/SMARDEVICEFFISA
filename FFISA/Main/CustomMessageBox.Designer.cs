using System;
using System.Drawing;
using System.Windows.Forms;

namespace FFISA.Main
{
    partial class CustomMessageBox : BaseForm
    {
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomMessageBox));
            this.PnlMainContainer = new System.Windows.Forms.Panel();
            this.PnlCustomMessage = new System.Windows.Forms.Panel();
            this.PnlCenter = new System.Windows.Forms.Panel();
            this.PtbMessageIcon2 = new System.Windows.Forms.PictureBox();
            this.PnlHeader = new System.Windows.Forms.Panel();
            this.TxtMensaje = new System.Windows.Forms.TextBox();
            this.BtnAceptar = new System.Windows.Forms.Button();
            this.PtbMessageIcon = new System.Windows.Forms.PictureBox();
            this.LblCaption = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PtbPBP = new System.Windows.Forms.PictureBox();
            this.PnlMainContainer.SuspendLayout();
            this.PnlCustomMessage.SuspendLayout();
            this.PnlCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlMainContainer
            // 
            this.PnlMainContainer.BackColor = System.Drawing.Color.Gainsboro;
            this.PnlMainContainer.Controls.Add(this.PnlCustomMessage);
            this.PnlMainContainer.Location = new System.Drawing.Point(9, 44);
            this.PnlMainContainer.Name = "PnlMainContainer";
            this.PnlMainContainer.Size = new System.Drawing.Size(220, 220);
            // 
            // PnlCustomMessage
            // 
            this.PnlCustomMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(118)))), ((int)(((byte)(189)))));
            this.PnlCustomMessage.Controls.Add(this.PnlCenter);
            this.PnlCustomMessage.Location = new System.Drawing.Point(5, 4);
            this.PnlCustomMessage.Name = "PnlCustomMessage";
            this.PnlCustomMessage.Size = new System.Drawing.Size(210, 210);
            // 
            // PnlCenter
            // 
            this.PnlCenter.BackColor = System.Drawing.Color.White;
            this.PnlCenter.Controls.Add(this.PtbMessageIcon2);
            this.PnlCenter.Controls.Add(this.PnlHeader);
            this.PnlCenter.Controls.Add(this.TxtMensaje);
            this.PnlCenter.Controls.Add(this.BtnAceptar);
            this.PnlCenter.Controls.Add(this.PtbMessageIcon);
            this.PnlCenter.Controls.Add(this.LblCaption);
            this.PnlCenter.Location = new System.Drawing.Point(5, 5);
            this.PnlCenter.Name = "PnlCenter";
            this.PnlCenter.Size = new System.Drawing.Size(202, 201);
            // 
            // PtbMessageIcon2
            // 
            this.PtbMessageIcon2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.PtbMessageIcon2.Image = ((System.Drawing.Image)(resources.GetObject("PtbMessageIcon2.Image")));
            this.PtbMessageIcon2.Location = new System.Drawing.Point(5, 125);
            this.PtbMessageIcon2.Name = "PtbMessageIcon2";
            this.PtbMessageIcon2.Size = new System.Drawing.Size(20, 20);
            this.PtbMessageIcon2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PtbMessageIcon2.Visible = false;
            // 
            // PnlHeader
            // 
            this.PnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(118)))), ((int)(((byte)(189)))));
            this.PnlHeader.Location = new System.Drawing.Point(-1, 30);
            this.PnlHeader.Name = "PnlHeader";
            this.PnlHeader.Size = new System.Drawing.Size(202, 5);
            // 
            // TxtMensaje
            // 
            this.TxtMensaje.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TxtMensaje.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtMensaje.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.TxtMensaje.ForeColor = System.Drawing.Color.Black;
            this.TxtMensaje.Location = new System.Drawing.Point(4, 39);
            this.TxtMensaje.Multiline = true;
            this.TxtMensaje.Name = "TxtMensaje";
            this.TxtMensaje.ReadOnly = true;
            this.TxtMensaje.Size = new System.Drawing.Size(193, 157);
            this.TxtMensaje.TabIndex = 29;
            // 
            // BtnAceptar
            // 
            this.BtnAceptar.BackColor = System.Drawing.Color.White;
            this.BtnAceptar.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.BtnAceptar.ForeColor = System.Drawing.Color.Black;
            this.BtnAceptar.Location = new System.Drawing.Point(163, 4);
            this.BtnAceptar.Name = "BtnAceptar";
            this.BtnAceptar.Size = new System.Drawing.Size(28, 20);
            this.BtnAceptar.TabIndex = 28;
            this.BtnAceptar.Text = "OK";
            // 
            // PtbMessageIcon
            // 
            this.PtbMessageIcon.BackColor = System.Drawing.Color.Transparent;
            this.PtbMessageIcon.Image = ((System.Drawing.Image)(resources.GetObject("PtbMessageIcon.Image")));
            this.PtbMessageIcon.Location = new System.Drawing.Point(3, 3);
            this.PtbMessageIcon.Name = "PtbMessageIcon";
            this.PtbMessageIcon.Size = new System.Drawing.Size(25, 25);
            this.PtbMessageIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // LblCaption
            // 
            this.LblCaption.BackColor = System.Drawing.Color.White;
            this.LblCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LblCaption.ForeColor = System.Drawing.Color.Black;
            this.LblCaption.Location = new System.Drawing.Point(34, 7);
            this.LblCaption.Name = "LblCaption";
            this.LblCaption.Size = new System.Drawing.Size(60, 19);
            this.LblCaption.Text = "* AVISO *";
            this.LblCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // PtbPBP
            // 
            this.PtbPBP.BackColor = System.Drawing.Color.Transparent;
            this.PtbPBP.Image = ((System.Drawing.Image)(resources.GetObject("PtbPBP.Image")));
            this.PtbPBP.Location = new System.Drawing.Point(62, 280);
            this.PtbPBP.Name = "PtbPBP";
            this.PtbPBP.Size = new System.Drawing.Size(110, 30);
            this.PtbPBP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // CustomMessageBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.PtbPBP);
            this.Controls.Add(this.PnlMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimizeBox = false;
            this.Name = "CustomMessageBox";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.PnlMainContainer.ResumeLayout(false);
            this.PnlCustomMessage.ResumeLayout(false);
            this.PnlCenter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public Panel PnlMainContainer;
        public Panel PnlCustomMessage;
        public Panel PnlCenter;
        public Panel PnlHeader;
        public TextBox TxtMensaje;
        public Button BtnAceptar;
        public PictureBox PtbMessageIcon;
        public Label LblCaption;
        private PictureBox pictureBox1;
        private PictureBox PtbPBP;
        public PictureBox PtbMessageIcon2;
    }
}
