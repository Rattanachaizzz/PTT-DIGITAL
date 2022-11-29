using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace AUTO_UPDATE
{
    public class Circlelb : Label
    {
        protected override void OnResize(EventArgs e)
        {
            using (var Circlelb = new GraphicsPath())
            {
                Circlelb.AddEllipse(new RectangleF(2, 2, this.Width - 5, this.Height - 5));
                this.Region = new Region(Circlelb);
            }
            base.OnResize(e);
        }
    }
}
