using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Auto_Update
{
    class ArtanPanel : Panel
    {
        //Fields
        private int borderRadius = 30;
        private float gradienArtle = 90F;
        private Color gradienTopColor = Color.DodgerBlue;
        private Color gradienButtomColor = Color.CadetBlue;

        //Construtor
        public ArtanPanel()
        { 
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Size = new Size(350,200);
        }

        //properties
        public int BorderRadius 
        { 
            get => borderRadius;
            set { borderRadius = value; this.Invalidate(); } 
        }
        public float GradienArtle
        { 
            get => gradienArtle;
            set { gradienArtle = value; this.Invalidate(); }
        }
        public Color GradienTopColor 
        { 
            get => gradienTopColor;
            set { gradienTopColor = value; this.Invalidate(); }
        }
        public Color GradienButtomColor
        { 
            get => gradienButtomColor; 
            set { gradienButtomColor = value; this.Invalidate(); }
        }

        //Methods
        private GraphicsPath GetArtanPath(RectangleF rectangle, float radius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.StartFigure();
            graphicsPath.AddArc(rectangle.Width - radius, rectangle.Height - radius, radius, radius, 0, 90);
            graphicsPath.AddArc(rectangle.X, rectangle.Height - radius, radius, radius, 90, 90);
            graphicsPath.AddArc(rectangle.X, rectangle.Y, radius, radius, 180, 90);
            graphicsPath.AddArc(rectangle.Width - radius, rectangle.Y, radius, radius, 270, 90);
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //Gradient
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            LinearGradientBrush brushArtan = new LinearGradientBrush(this.ClientRectangle, this.gradienTopColor ,this.gradienButtomColor, this.gradienArtle);
            Graphics graphicsArtan = e.Graphics;
            graphicsArtan.FillRectangle(brushArtan,ClientRectangle);

            //BorderRadius
            RectangleF rectangleF = new RectangleF(0,0,this.Width,this.Height);
            if (borderRadius>2)
            {
                using (GraphicsPath graphicsPath = GetArtanPath(rectangleF, borderRadius))
                using (Pen pen = new Pen(this.Parent.BackColor,2))
                {
                   //this.Region = new Region(g)
                   this.Region = new Region(graphicsPath);
                   e.Graphics.DrawPath(pen, graphicsPath);
                }
            }
            else
            { 
                this.Region = new Region(rectangleF);
            }
        }

    }
}
