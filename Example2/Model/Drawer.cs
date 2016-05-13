using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example2.Model
{
    public enum Shape
    {
        Line,
        Pencil,
        Eraser,
        Rectangle,
        Triangle,
        Full,
        Circle,
        Trapezoid,
        Hexagon,
        Octagon,
        Star,
        Cube,
        Rhombus
    }
    class Drawer
    {
        Bitmap bmp;
        Graphics g;
        public Pen p;
        Point prevPoint;
        PictureBox pictureBox;
        GraphicsPath path;
       
        public Shape Shape { get; set; }

        public Drawer(PictureBox pictureBox)
        {
            Shape = Shape.Pencil;
            this.pictureBox = pictureBox;
            this.pictureBox.MouseMove += PictureBox_MouseMove;
            this.pictureBox.MouseDown += PictureBox_MouseDown;
            this.pictureBox.MouseUp += PictureBox_MouseUp;
            this.pictureBox.Paint += PictureBox_Paint;
            this.pictureBox.MouseClick += PictureBox_MouseClick;
            p = new Pen(Color.Black);
            Load("");
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (path != null)
            {
                e.Graphics.DrawPath(p, path);
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (path != null)
            {
                g.DrawPath(p, path);
                path = null;
            }
        }
        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (Shape == Shape.Full)
            {
                SimplePaint.MapFill m = new SimplePaint.MapFill();
                m.Fill(g, prevPoint, p.Color, ref bmp);
                g = Graphics.FromImage(bmp);
                pictureBox.Image = bmp;
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            prevPoint = e.Location;
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Draw(e.Location);
            }
        }

        public void Draw(Point currentPoint)
        {
           
            Point t1 = new Point((prevPoint.X + currentPoint.X) / 2, prevPoint.Y);
            Point t2 = new Point(prevPoint.X, currentPoint.Y);
            Point t3 = new Point(currentPoint.X, currentPoint.Y);
            int minx = 0; int miny = 0;
            if (prevPoint.X > currentPoint.X)
            {
                minx = currentPoint.X;
            }
            else { minx = prevPoint.X; }
            if (prevPoint.Y > currentPoint.Y)
            {
                miny = currentPoint.Y;
            }
            else { miny = prevPoint.Y; }

            switch (Shape)
            {
                case Shape.Line:
                    path = new GraphicsPath();
                    path.AddLine(prevPoint, currentPoint);
                    break;
                case Shape.Pencil:
                    if (p.Width < 2)
                    {
                        g.DrawLine(p, prevPoint, currentPoint);
                    }
                    else
                    {
                        g.DrawEllipse(p, prevPoint.X, prevPoint.Y, p.Width, p.Width);
                    }
                    prevPoint = currentPoint;
                    break;
                case Shape.Eraser:
                    g.DrawLine(new Pen(Color.White,p.Width), prevPoint, currentPoint);
                    prevPoint = currentPoint;
                    break;
                case Shape.Triangle:
                    path = new GraphicsPath();
                    path.AddLine(t1, t2);
                    path.AddLine(t2, t3);
                    path.AddLine(t1, t3);
                    break;
                case Shape.Hexagon:
                    path = new GraphicsPath();
                    Point[] arr4 =
                    {
                        new Point(prevPoint.X+(currentPoint.X-prevPoint.X)/3,prevPoint.Y),
                        new Point(currentPoint.X-(currentPoint.X-prevPoint.X)/3,prevPoint.Y),
                        new Point(currentPoint.X,(currentPoint.Y + prevPoint.Y) / 2),
                        new Point(currentPoint.X-(currentPoint.X-prevPoint.X)/3,currentPoint.Y),
                        new Point(prevPoint.X+(currentPoint.X-prevPoint.X)/3,currentPoint.Y),
                        new Point(prevPoint.X,(currentPoint.Y + prevPoint.Y) / 2),
                    };
                    path.AddPolygon(arr4);
                    break;
                case Shape.Rectangle:
                    path = new GraphicsPath();
                    path.AddRectangle(new Rectangle( minx, miny, Math.Abs(currentPoint.X-prevPoint.X), Math.Abs(currentPoint.Y-prevPoint.Y)));
                    break;
                case Shape.Rhombus:
                    path = new GraphicsPath();
                    Point[] arr2 ={
                        new Point(prevPoint.X+(currentPoint.X-prevPoint.X)/2,prevPoint.Y),
                        new Point(currentPoint.X,prevPoint.Y+(currentPoint.Y-prevPoint.Y)/2),
                        new Point(currentPoint.X-(currentPoint.X-prevPoint.X)/2,currentPoint.Y),
                        new Point(prevPoint.X,currentPoint.Y-(currentPoint.Y-prevPoint.Y)/2),
                    };
                    path.AddPolygon(arr2);
                    break;
                case Shape.Octagon:
                    path = new GraphicsPath();
                    Point[] arr5 =
                    {
                        new Point(prevPoint.X+(currentPoint.X-prevPoint.X)/3,prevPoint.Y),
                        new Point(currentPoint.X-(currentPoint.X-prevPoint.X)/3,prevPoint.Y),
                        new Point(currentPoint.X,prevPoint.Y+(currentPoint.Y-prevPoint.Y)/3),
                        new Point(currentPoint.X,currentPoint.Y-(currentPoint.Y-prevPoint.Y)/3),
                        new Point(currentPoint.X-(currentPoint.X-prevPoint.X)/3,currentPoint.Y),
                        new Point(prevPoint.X+(currentPoint.X - prevPoint.X) / 3,currentPoint.Y),
                        new Point(prevPoint.X,currentPoint.Y-(currentPoint.Y-prevPoint.Y)/3),
                        new Point(prevPoint.X,prevPoint.Y+(currentPoint.Y-prevPoint.Y)/3),
                    };
                    path.AddPolygon(arr5);
                    break;
                case Shape.Star:
                    path = new GraphicsPath();
                    Point[] arr6 =
                    {
                        new Point(prevPoint.X+(currentPoint.X-prevPoint.X)/2,prevPoint.Y),
                        new Point(currentPoint.X-(currentPoint.X-prevPoint.X)/4,prevPoint.Y+(currentPoint.Y-prevPoint.Y)/4),
                        new Point(currentPoint.X,prevPoint.Y+(currentPoint.Y-prevPoint.Y)/3),
                        new Point(currentPoint.X-(currentPoint.X-prevPoint.X)/3,currentPoint.Y-(currentPoint.Y-prevPoint.Y)/3),
                        new Point(currentPoint.X-(currentPoint.X-prevPoint.X)/7,currentPoint.Y),
                        new Point(prevPoint.X+(currentPoint.X-prevPoint.X)/2,currentPoint.Y-(currentPoint.Y-prevPoint.Y)/5),
                        new Point(prevPoint.X+(currentPoint.X-prevPoint.X)/5,currentPoint.Y),
                        new Point(prevPoint.X+(currentPoint.X-prevPoint.X)/3,prevPoint.Y+(currentPoint.Y-prevPoint.Y)/(2)),
                        new Point(prevPoint.X,prevPoint.Y+(currentPoint.Y-prevPoint.Y)/3),
                        new Point(prevPoint.X+(currentPoint.X-prevPoint.X)/4,prevPoint.Y+(currentPoint.Y-prevPoint.Y)/4),
                    };
                    path.AddPolygon(arr6);
                    break;
                case Shape.Trapezoid:
                    path = new GraphicsPath();
                    Point[] arr3 =
                    {
                        new Point(prevPoint.X+(currentPoint.X-prevPoint.X)/3,prevPoint.Y),
                        new Point(currentPoint.X-(currentPoint.X-prevPoint.X)/3,prevPoint.Y),
                        new Point(currentPoint.X,currentPoint.Y),
                        new Point(prevPoint.X,currentPoint.Y),
                    };
                    path.AddPolygon(arr3);
                    break;
                case Shape.Cube:
                    path = new GraphicsPath();
                    Point[] end1 =
                    {
                        new Point(prevPoint.X+(currentPoint.X-prevPoint.X)/4,prevPoint.Y),
                        new Point(currentPoint.X,prevPoint.Y),
                        new Point(currentPoint.X,currentPoint.Y-(currentPoint.Y - prevPoint.Y) / 4),
                        new Point(currentPoint.X-(currentPoint.X - prevPoint.X) / 4,currentPoint.Y),
                        new Point(prevPoint.X,currentPoint.Y),
                        new Point(prevPoint.X,prevPoint.Y+(currentPoint.Y-prevPoint.Y)/4),

                    };
                    path.AddPolygon(end1);

                    Point[] end2 =
                    {
                        new Point(prevPoint.X, prevPoint.Y+(currentPoint.Y-prevPoint.Y)/4),
                        new Point(currentPoint.X-(currentPoint.X-prevPoint.X)/4,prevPoint.Y+(currentPoint.Y-prevPoint.Y)/4),
                        new Point(currentPoint.X,prevPoint.Y),
                        new Point(currentPoint.X,currentPoint.Y-(currentPoint.Y - prevPoint.Y) / 4),
                        new Point(currentPoint.X-(currentPoint.X - prevPoint.X) / 4, currentPoint.Y),
                        new Point(currentPoint.X-(currentPoint.X-prevPoint.X)/4,prevPoint.Y+(currentPoint.Y-prevPoint.Y)/4),

                    };
                    path.AddPolygon(end2);
                    Point[] end3 =
                   {
                        new Point(prevPoint.X+(currentPoint.X-prevPoint.X)/4,prevPoint.Y),
                        new Point(prevPoint.X+(currentPoint.X-prevPoint.X)/4,currentPoint.Y-(currentPoint.Y-prevPoint.Y)/4),
                        new Point(currentPoint.X,currentPoint.Y-(currentPoint.Y - prevPoint.Y) / 4),
                        new Point(currentPoint.X-(currentPoint.X - prevPoint.X) / 4,currentPoint.Y),
                        new Point(prevPoint.X,currentPoint.Y),
                        new Point(prevPoint.X+(currentPoint.X-prevPoint.X)/4,currentPoint.Y-(currentPoint.Y-prevPoint.Y)/4),
                    };
                    path.AddPolygon(end3);

                    break;

            
                case Shape.Circle:
                    path = new GraphicsPath();
                    path.AddEllipse(new Rectangle(minx, miny, Math.Abs(currentPoint.X - prevPoint.X), Math.Abs(currentPoint.Y - prevPoint.Y)));
                    break;
                default:
                    break;
            }
            pictureBox.Refresh();
        }

        public void Load(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
                g = Graphics.FromImage(bmp);

                g.Clear(Color.White);
            }
            else
            {
                bmp = new Bitmap(fileName);
                g = Graphics.FromImage(bmp);
            }

            pictureBox.Image = bmp;
        }
        public void Save(string fileName)
        {
            bmp.Save(fileName);
        }
    }
}
