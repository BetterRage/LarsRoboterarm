using System.Collections.Generic;
using System.Drawing;
using System;
namespace LarsRoboterarm
{
    public static class RoboterArmZeichnung
    {
        public static void Draw(Graphics g, int[] angles)
        {
            CheckAngles(angles);
            int[] drawAngles = new int[angles.Length];
            int lastDrawAngle = 0;
            for (int i = 0; i < angles.Length; i++)
            {
                int realAngle = 180 - (angles[i] + 90);
                if (i == 0)
                    drawAngles[i] = realAngle;
                else
                    drawAngles[i] = realAngle - (90 - lastDrawAngle);
                lastDrawAngle = drawAngles[i];
            }
            g.ScaleTransform(1F, -1F);
            g.TranslateTransform(0, -g.ClipBounds.Height);

            Point startPoint = new((int)g.ClipBounds.Width / 2, 0);
            Point endPoint = new();
            using Pen pen = new(Brushes.Black, 5);

            for (int i = 0; i < drawAngles.Length; i++)
            {
                double angleRad = drawAngles[i] * Math.PI / 180;
                endPoint.X = (int)(startPoint.X + (Math.Cos(angleRad) * 60));
                endPoint.Y = (int)(startPoint.Y + (Math.Sin(angleRad) * 60));
                g.DrawLine(pen, startPoint, endPoint);
                startPoint = endPoint;
            }
        }

        private static void CheckAngles(int[] angles)
        {
            foreach (var item in angles)
            {
                if (item < -90 || item > 90)
                    throw new ArgumentException("Winkel von -90° bis 90° erwartet");
            }
        }
    }
}


