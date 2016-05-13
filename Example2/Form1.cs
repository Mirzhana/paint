using Example2.Model;
using System;
using System.Windows.Forms;

namespace Example2
{
    public partial class Form1 : Form
    {
        Drawer d;
               public Form1()
        {
            InitializeComponent();
            d = new Drawer(pictureBox1);   
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = e.Location.ToString();  
        }
        private void button1_Click(object sender, EventArgs e) //Changing colors
        { 
            Button b = sender as Button;
            d.p.Color = b.BackColor;
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                d.Save(saveFileDialog1.FileName);
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                d.Load(openFileDialog1.FileName);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            d.Shape = Shape.Eraser;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            d.Shape = Shape.Line;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            d.Shape = Shape.Pencil;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            d.Shape = Shape.Rectangle;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            d.Shape = Shape.Triangle;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                d.p.Color = colorDialog1.Color;
                Button b = sender as Button;
                b.BackColor = colorDialog1.Color;
            }
        }
        private void button11_MouseClick(object sender, MouseEventArgs e)
        {
            d.Shape = Shape.Full;
        }
        private void button12_Click(object sender, EventArgs e)
        {
            d.Shape = Shape.Circle;
        }
         private void button14_Click(object sender, EventArgs e)
        {
            d.Shape = Shape.Trapezoid;
        }
    
        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           int width = Convert.ToInt32(comboBox1.SelectedItem);
            switch (width)
            {
                case 1:
                    d.p.Width = 1;
                    break;
                case 2:
                    d.p.Width = 2;
                    break;
                case 3:
                    d.p.Width = 3;
                    break;
                case 4:
                    d.p.Width = 4;
                    break;
                case 5:
                    d.p.Width = 5;
                    break;
                case 6:
                    d.p.Width = 6;
                    break;
                case 7:
                    d.p.Width = 7;
                    break;
                default:
                    break;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            d.Shape = Shape.Hexagon;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            d.Shape = Shape.Octagon;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            d.Shape = Shape.Star;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            d.Shape = Shape.Cube;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            d.Shape = Shape.Rhombus;
        }
    }
}
