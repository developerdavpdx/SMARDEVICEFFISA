using System;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

public class BaseForm : Form
{
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // 🔹 Liberar imágenes y recursos gráficos de controles
            LiberarRecursosGraficos(this);

            // 🔹 Liberar bitmaps o iconos en campos del formulario
            LiberarCamposGraficos();
        }

        base.Dispose(disposing);
    }

    private void LiberarRecursosGraficos(Control parent)
    {
        foreach (Control ctrl in parent.Controls)
        {
            // 🖼 PictureBox: solo Image
            if (ctrl is PictureBox)
            {
                PictureBox pb = (PictureBox)ctrl;

                if (pb.Image != null)
                {
                    try { pb.Image.Dispose(); }
                    catch { }
                    pb.Image = null;
                }
            }
            // 📋 Otros controles que podrían tener BackgroundImage en CF (Panel, Form, etc.)
            else
            {
                try
                {
                    PropertyInfo prop = ctrl.GetType().GetProperty("BackgroundImage");
                    if (prop != null)
                    {
                        Image bg = prop.GetValue(ctrl, null) as Image;
                        if (bg != null)
                        {
                            try { bg.Dispose(); }
                            catch { }
                            prop.SetValue(ctrl, null, null);
                        }
                    }
                }
                catch { }
            }

            // 🔄 Recursivo usando Controls.Count (no HasChildren en CF)
            if (ctrl.Controls != null && ctrl.Controls.Count > 0)
            {
                LiberarRecursosGraficos(ctrl);
            }
        }
    }

    private void LiberarCamposGraficos()
    {
        // Usar reflexión para encontrar cualquier campo Bitmap o Icon en el formulario
        FieldInfo[] campos = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (FieldInfo campo in campos)
        {
            object valor = campo.GetValue(this);

            if (valor is Bitmap)
            {
                try { ((Bitmap)valor).Dispose(); }
                catch { }
                campo.SetValue(this, null);
            }
            else if (valor is Icon)
            {
                try { ((Icon)valor).Dispose(); }
                catch { }
                campo.SetValue(this, null);
            }
            else if (valor is Image)
            {
                try { ((Image)valor).Dispose(); }
                catch { }
                campo.SetValue(this, null);
            }
        }
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();
        // 
        // BaseForm
        // 
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
        this.ClientSize = new System.Drawing.Size(240, 320);
        this.Location = new System.Drawing.Point(0, 0);
        this.Name = "BaseForm";
        this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        this.ResumeLayout(false);

    }
}
