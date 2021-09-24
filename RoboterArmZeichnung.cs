using System.Collections.Generic;
using System.Drawing;
using System;
namespace LarsRoboterarm
{
    public static class RoboterArmZeichnung
    {
        /// <summary>
        /// zeichnet den roboterarm mit beliebig vielen gelenken auf das gegebene graphics objekt
        /// </summary>
        /// <param name="g">graphics oberfläche eines controls (z.B. picturebox oder panel)</param>
        /// <param name="angles">array mit winkeln von -90 bis +90 grad</param>
        public static void Draw(Graphics g, int[] angles)
        {
            //winkel überprufen und evtl exception werfen
            CheckAngles(angles);

            //ermitteln der winkel in denen der arm gezeichnet werden soll
            int[] drawAngles = new int[angles.Length];
            int lastDrawAngle = 0;
            for (int i = 0; i < angles.Length; i++)
            {
                //winkel von -90 bis +90 zu 180 bis 0
                int realAngle = 180 - (angles[i] + 90);
                if (i == 0)
                    drawAngles[i] = realAngle;
                else
                    //es muss der winkel des letzten gelenks berücksichtigt werden
                    drawAngles[i] = realAngle - (90 - lastDrawAngle);
                lastDrawAngle = drawAngles[i];
            }

            //zeichnung skalieren und Ursprund des koordinatensystems
            //um zeichnen zu erleichtern
            g.ScaleTransform(1F, -1F);
            g.TranslateTransform(0, -g.ClipBounds.Height);

            //2 punkte der linien und stift
            Point startPoint = new((int)g.ClipBounds.Width / 2, 0); //der erste punkt wird an (y = 0 | x = hälfte) gezeichnet
            Point endPoint = new();
            using Pen pen = new(Brushes.Black, 5);

            for (int i = 0; i < drawAngles.Length; i++)
            {
                //winkel in radianten umrechnen
                double angleRad = drawAngles[i] * Math.PI / 180;
                //start und endpunkte berechnen
                endPoint.X = (int)(startPoint.X + (Math.Cos(angleRad) * 60));
                endPoint.Y = (int)(startPoint.Y + (Math.Sin(angleRad) * 60));
                g.DrawLine(pen, startPoint, endPoint);
                //startpunkt des nächsten gelenks ist endpunkt dieses gelenks
                startPoint = endPoint;
            }
        }

        /// <summary>
        /// überprüft die winkel und wirft evtl eine exception falls diese nicht zwischen -90 und 90 liegen
        /// </summary>
        /// <param name="angles"></param>
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


