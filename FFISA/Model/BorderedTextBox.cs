using System;
using System.Drawing;
using System.Windows.Forms;

namespace FFISA.Model
{
    public class BorderedTextBox : TextBox
    {
        public static string Perfil { get; set; }
        public static string Usuario { get; set; }
        public static string UsuarioMin { get; set; }
        public static string Sociedad { get; set; }
        private Color _borderColor = Color.Green; // Valor por defecto

        // Propiedad BorderColor (sin inicialización "en línea")
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }

        public BorderedTextBox()
        {
            // Opcional: Inicializar el color aquí si prefieres
            _borderColor = Color.Green;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Dibujar el borde personalizado
            using (Pen pen = new Pen(_borderColor, 2)) // Grosor: 2px
            {
                Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                e.Graphics.DrawRectangle(pen, rect);
            }
        }
    }
}

