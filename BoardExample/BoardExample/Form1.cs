using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoardExample
{
    public partial class Form1 : Form
    {
        PointF SpacecraftLocation = new PointF(300, 300);
        PointF TargetLocation = new PointF(800, 300);

        private int _orbitRadius = 50;

        public Form1()
        {
            InitializeComponent();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            SpacecraftLocation = new PointF(float.Parse(txtSpacecraftX.Text), float.Parse(txtSpacecraftY.Text));

             TargetLocation = new PointF(float.Parse(txtTargetX.Text), float.Parse(txtTargetY.Text));

            var pointA = new CelestialObject
            {
                Agility = float.Parse(txtAgility.Text),
                Direction = float.Parse(txtSpacecraftDirection.Text),
                Location = SpacecraftLocation,
                Speed = 10
            };

            var pointB = new CelestialObject
            {
                Agility = 0,
                Direction = 90,
                Location = TargetLocation,
                Speed = 0
            };

            var route = new RouteCalculator().Execute(pointA, pointB, 50);



            Image image = new Bitmap(Width, Height);

            var graphics = Graphics.FromImage(image);

            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.Bicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            DrawTargetPoint(graphics);

            DrawSpacecraft(graphics);

            foreach (var pointF in route)
            {
                graphics.FillEllipse(new SolidBrush(Color.GreenYellow), pointF.X - 1, pointF.Y - 1, 2, 2);
            }

            BackgroundImage = image;
        }

        public void DrawTargetPoint(Graphics graphics)
        {
            if (TargetLocation == PointF.Empty) return;

            var color = Color.Maroon;

            graphics.FillEllipse(new SolidBrush(color), TargetLocation.X - 2, TargetLocation.Y - 2, 4, 4);
            graphics.DrawEllipse(new Pen(color), TargetLocation.X - 4, TargetLocation.Y - 4, 8, 8);


            color = Color.DarkGray;
            graphics.DrawEllipse(new Pen(color), TargetLocation.X - _orbitRadius, TargetLocation.Y - _orbitRadius, _orbitRadius * 2, _orbitRadius * 2);
        }

        public void DrawSpacecraft(Graphics graphics)
        {
            if (SpacecraftLocation == PointF.Empty) return;

            var color = Color.DarkOliveGreen;

            graphics.FillEllipse(new SolidBrush(color), SpacecraftLocation.X - 2, SpacecraftLocation.Y - 2, 4, 4);
            graphics.DrawEllipse(new Pen(color), SpacecraftLocation.X - 4, SpacecraftLocation.Y - 4, 8, 8);

            DrawArrow(graphics, color);
        }

        private void DrawArrow(Graphics graphics, Color color, int arrowSize = 4)
        {
            var direction = int.Parse(txtSpacecraftDirection.Text);

            var endArrowPoint = MoveObject(SpacecraftLocation, 12, direction);

            DrawArrow(graphics, new SpaceMapVector(SpacecraftLocation, endArrowPoint, direction), color, arrowSize);
        }


        public static void DrawArrow(Graphics graphics, SpaceMapVector line, Color color, int arrowSize = 4)
        {
            // Base arrow line
            graphics.DrawLine(new Pen(color), line.PointFrom.X, line.PointFrom.Y, line.PointTo.X, line.PointTo.Y);

            // Arrow left line
            var leftArrowLine = Move(line.PointTo, arrowSize, line.Direction + 150);
            graphics.DrawLine(new Pen(color), leftArrowLine.PointFrom.X, leftArrowLine.PointFrom.Y, leftArrowLine.PointTo.X, leftArrowLine.PointTo.Y);

            // Arrow right line
            var rightArrowLine = Move(line.PointTo, arrowSize, line.Direction - 150);
            graphics.DrawLine(new Pen(color), rightArrowLine.PointFrom.X, rightArrowLine.PointFrom.Y, rightArrowLine.PointTo.X, rightArrowLine.PointTo.Y);

        }

        public PointF MoveObject(PointF currentLocation, double speed, double angleInGraduses)
        {
            var angleInRadians = (angleInGraduses - 90) * (Math.PI) / 180; // (Math.PI / 180) * angleInGraduses;

            var x = (float)(currentLocation.X + speed * Math.Cos(angleInRadians));
            var y = (float)(currentLocation.Y + speed * Math.Sin(angleInRadians));

            return new PointF(x, y);
        }

        public static SpaceMapVector Move(PointF from, double speed, double angle)
        {
            var angleInRadians = (angle - 90) * (Math.PI) / 180;

            var x = (float)(from.X + speed * Math.Cos(angleInRadians));
            var y = (float)(from.Y + speed * Math.Sin(angleInRadians));

            return new SpaceMapVector(new PointF(from.X, from.Y), new PointF(x, y), angle);
        }
    }

}
