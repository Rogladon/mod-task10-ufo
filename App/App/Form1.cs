using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace App {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
            StartMoveMovePoint(new Point(0, 0), new Point(500, 500));
            this.Paint += MovePoint;
		}

        void StartMoveMovePoint(Point start, Point end) {
            this.start = start;
            this.end = end;
            this.position = new Point(start.X, start.Y);

            moveTimer = new Timer();
            moveTimer.Interval = 100;
            moveTimer.Tick += TimerTick;
            moveTimer.Start();
		}
        Timer moveTimer;
        Point start;
        Point end;
        Point position;
        private void MovePoint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            GraphicsState gs;
            gs = g.Save();
            g.ScaleTransform(0.5f, 0.5f);
            g.DrawLine(new Pen(Color.Black,2), start,end);

            float step = 10;
            int approxDegree = 5;

            float angle = Atan((end.Y - start.Y) / (start.X - end.X), approxDegree);

            position.Y -= (int)Math.Round(Sin(angle, approxDegree) * step);
            position.X += (int)Math.Round(Cos(angle, approxDegree) * step);

            if(Distance(position, end) < step) {
                moveTimer.Stop();
			}

            g.DrawEllipse(new Pen(Color.Red,10), (int)position.X, (int)position.Y, 1, 1);
            g.Restore(gs);
        }
        void TimerTick(object sender, EventArgs e) {
            Invalidate();
		}

		#region Math
        float Distance(Point p1, Point p2) {
            return (float)Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
		}
		float Factorial(int x) {
            float result = 1;
            for(int i = 1; i <=x; i++) {
                result *= i;
			}
            return result;
		}
        float Sin(float x, int degree = 5) {
            double res = 0;
            for (int i = 1; i < degree + 1; i++) {
                res += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 1) / Factorial(2 * i - 1);
            }
            return  (float)res;
        }
        float Cos(float x, int degree = 5) {
            double res = 0;
            for (int i = 1; i < degree + 1; i++) {
                res += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 2) / Factorial(2 * i - 2);
            }
            return (float)res;
        }
        float Atan(float x, int degree = 5) {
            double res = 0;
            if (-1 <= x && x <= 1) {
                for (int i = 1; i < degree + 1; i++) {
                    res += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 1) / (2 * i - 1);
                }
            } else {
                if (x >= 1) {
                    res += Math.PI / 2;
                    for (int i = 0; i < degree; i++) {
                        res -= Math.Pow(-1, i) / ((2 * i + 1) * Math.Pow(x, 2 * i + 1));
                    }
                } else {
                    res -= Math.PI / 2;
                    for (int i = 0; i < degree; i++) {
                        res -= Math.Pow(-1, i) / ((2 * i + 1) * Math.Pow(x, 2 * i + 1));
                    }
                }
            }
            return (float)res;
        }
		#endregion

	}
}
