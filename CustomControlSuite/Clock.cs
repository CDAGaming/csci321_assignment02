using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace CustomControlSuite
{
    /**
     * Control Name: StopClock
     * 
     * Description: This control is able to serve as a unified clock and stopwatch
     * via usage of DateTime parsing and an inner stopwatch.
     * 
     * Use syncToCurrentTime
     * 
     * Usage: new Clock([syncToCurrentTime])
     */
    public partial class Clock : UserControl
    {
        private BufferedGraphicsContext CurrentContext;
        private float radius;
        private Point center;

        public DateTime currentTimeStamp = new DateTime();
        public bool isPaused;
        public int currentHour = 0, currentMinute = 0, currentSeconds = 0;
        public bool syncWithCurrentTime = false;
        private readonly Stopwatch innerWatch = new Stopwatch();

        public Clock()
        {
            InitializeData(true);
        }

        public Clock(bool syncToCurrentTime)
        {
            InitializeData(syncToCurrentTime);
        }

        private void InitializeData(bool syncToCurrentTime)
        {
            InitializeComponent();
            SyncSizeData();
            syncWithCurrentTime = syncToCurrentTime;
            isPaused = !syncWithCurrentTime;
        }

        public void StartTiming()
        {
            if (syncWithCurrentTime)
            {
                return;
            } else
            {
                isPaused = false;
                innerWatch.Start();
            }
        }

        public void PauseTiming()
        {
            if (syncWithCurrentTime)
            {
                return;
            } else
            {
                isPaused = true;
                innerWatch.Stop();
            }
        }

        public void ResetTiming()
        {
            if (syncWithCurrentTime)
            {
                return;
            } else
            {
                PauseTiming();
                innerWatch.Reset();
            }
        }

        private void Clock_Paint(object sender, PaintEventArgs e)
        {
            DrawClockData();
        }

        private void DrawLines(Graphics g)
        {
            Pen redPen = new Pen(Color.Red, radius * 0.07f);
            Pen blackPenHalfSize = new Pen(Color.Black, radius * 0.05f);
            Pen blackPenQuarterSize = new Pen(Color.Black, radius * 0.02f);

            // Draw Red Line data
            for (int i = 0; i < 4; i++)
            {
                float x = (float)Math.Cos(i * 90 * Math.PI / 180) * radius * .7f + center.X;
                float y = (float)Math.Sin(i * 90 * Math.PI / 180) * radius * .7f + center.Y;

                float x2 = (float)Math.Cos(i * 90 * Math.PI / 180) * radius * .8f + center.X;
                float y2 = (float)Math.Sin(i * 90 * Math.PI / 180) * radius * .8f + center.Y;

                g.DrawLine(redPen, x, y, x2, y2);
            }

            // Draw marks for each Hour in pairs of 3
            for (int i = 0; i < 12; i++)
            {

                if (i % 3 != 0)
                {
                    float x = (float)Math.Cos(i * 30 * Math.PI / 180) * radius * .7f + center.X;
                    float y = (float)Math.Sin(i * 30 * Math.PI / 180) * radius * .7f + center.Y;

                    float x2 = (float)Math.Cos(i * 30 * Math.PI / 180) * radius * .8f + center.X;
                    float y2 = (float)Math.Sin(i * 30 * Math.PI / 180) * radius * .8f + center.Y;

                    g.DrawLine(blackPenHalfSize, x, y, x2, y2);
                }
            }

            // Draw marks for each minute in pairs of 5
            for (int i = 0; i < 60; i++)
            {
                if (i % 5 != 0)
                {
                    float x = (float)Math.Cos(i * 6 * Math.PI / 180) * radius * .7f + center.X;
                    float y = (float)Math.Sin(i * 6 * Math.PI / 180) * radius * .7f + center.Y;

                    float x2 = (float)Math.Cos(i * 6 * Math.PI / 180) * radius * .8f + center.X;
                    float y2 = (float)Math.Sin(i * 6 * Math.PI / 180) * radius * .8f + center.Y;

                    g.DrawLine(blackPenQuarterSize, x, y, x2, y2);
                }
            }
        }

        // PaintNumbers: Paint Hour Numbers from 1 to 12
        private void DrawNumbers(Graphics g)
        {
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            Font numfont = new Font("Tahoma", radius * 0.11f, FontStyle.Bold);
            Brush numberBrush = new SolidBrush(Color.Black);

            for (int i = 1; i <= 12; i++)
            {
                float x = (float)Math.Cos((i * 30 - 90) * Math.PI / 180) * radius * .9f + center.X;
                float y = (float)Math.Sin((i * 30 - 90) * Math.PI / 180) * radius * .9f + center.Y;
                g.DrawString(i.ToString(), numfont, numberBrush, x, y, sf);
            }
        }

        // DrawHand: Draw the minute and second hands representing the currentTimeStamp
        private void DrawHands(Graphics g)
        {
            Pen hourPen = new Pen(Color.Red, radius * 0.05f);
            Pen minutePen = new Pen(Color.Black, radius * 0.03f);
            Pen secondPen = new Pen(Color.Red, radius * 0.01f);

            if (!isPaused)
            {
                if (syncWithCurrentTime)
                {
                    currentTimeStamp = DateTime.Now;
                } else
                {
                    currentTimeStamp = new DateTime() + TimeSpan.FromMilliseconds(innerWatch.ElapsedMilliseconds);
                }

                currentHour = currentTimeStamp.Hour;
                currentMinute = currentTimeStamp.Minute;
                currentSeconds = currentTimeStamp.Second;
            }


            float hx = (float)Math.Cos((currentHour * 30 - 90) * Math.PI / 180) * radius * .5f + center.X;
            float hy = (float)Math.Sin((currentHour * 30 - 90) * Math.PI / 180) * radius * .5f + center.Y;

            float mx = (float)Math.Cos((currentMinute * 6 - 90) * Math.PI / 180) * radius * .7f + center.X;
            float my = (float)Math.Sin((currentMinute * 6 - 90) * Math.PI / 180) * radius * .7f + center.Y;

            float sx = (float)Math.Cos((currentSeconds * 6 - 90) * Math.PI / 180) * radius * .75f + center.X;
            float sy = (float)Math.Sin((currentSeconds * 6 - 90) * Math.PI / 180) * radius * .75f + center.Y;

            g.DrawLine(hourPen, center.X, center.Y, hx, hy);
            g.DrawLine(minutePen, center.X, center.Y, mx, my);
            g.DrawLine(secondPen, center.X, center.Y, sx, sy);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            // For each tick on the main timer (Not the innerWatch)
            // We want to flush and re-render the clock to ensure accuracy
            DrawClockData();
        }

        // DrawClockData: Flush and re-render Clock Information
        private void DrawClockData()
        {
            CurrentContext = BufferedGraphicsManager.Current;
            BufferedGraphics bg = CurrentContext.Allocate(CreateGraphics(), ClientRectangle);

            bg.Graphics.Clear(Color.White);

            DrawLines(bg.Graphics);
            DrawNumbers(bg.Graphics);
            DrawHands(bg.Graphics);

            bg.Render();
        }

        // SyncSizeData: Synchronize Size data (Normally should only occur from resizing or initialization)
        private void SyncSizeData()
        {
            if (Height > Width)
            {
                radius = Width / 2;
            }
            else
            {
                radius = Height / 2;
            }
            center = new Point(Width / 2, Height / 2);
        }

        private void Clock_Resize(object sender, EventArgs e)
        {
            SyncSizeData();
            DrawClockData();
        }
    }
}
