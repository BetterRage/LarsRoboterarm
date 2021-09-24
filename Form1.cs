using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LarsRoboterarm
{
    public partial class Form1 : Form
    {
        int[] angles = new int[5];
        public Form1()
        {
            InitializeComponent();
            trackBar1.ValueChanged += trackBarValueChanged;
            trackBar2.ValueChanged += trackBarValueChanged;
            trackBar3.ValueChanged += trackBarValueChanged;
            trackBar4.ValueChanged += trackBarValueChanged;
            trackBar5.ValueChanged += trackBarValueChanged;
        }

        private void trackBarValueChanged(object sender, EventArgs e)
        {
            if (sender is TrackBar trackBar)
            {
                switch (trackBar.Name)
                {
                    case "trackBar1":
                        angles[0] = trackBar.Value;
                        break;
                    case "trackBar2":
                        angles[1] = trackBar.Value;
                        break;
                    case "trackBar3":
                        angles[2] = trackBar.Value;
                        break;
                    case "trackBar4":
                        angles[3] = trackBar.Value;
                        break;
                    case "trackBar5":
                        angles[4] = trackBar.Value;
                        break;
                }
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            RoboterArmZeichnung.Draw(e.Graphics, angles);
        }
    }
}
