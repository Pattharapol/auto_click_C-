using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoClick
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);

        private void frmMain_Load(object sender, EventArgs e)
        {
            //this.TopMost = true;
            lblCount.Text = "0";
        }

        private const int MOUSEEVENTF_MOVE = 0x0001; /* mouse move */
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
        private const int MOUSEEVENTF_LEFTUP = 0x0004; /* left button up */
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */

        [DllImport("user32.dll",
            CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private int count = 1;
        private int totalCount = 0;

        private void btnStart_Click(object sender, EventArgs e)
        {
            var t = Task.Run(() =>
            {
                while (count != 0)
                {
                    Random random = new Random();
                    int width = Screen.PrimaryScreen.Bounds.Width;
                    int height = Screen.PrimaryScreen.Bounds.Height;

                    int cursorHeight = random.Next(150, height - 150);
                    int cursorWidth = random.Next(350, width - 350);

                    int randInt = random.Next(100000, 500000);
                    //int randInt = random.Next(800, 1500);
                    this.BeginInvoke(new ThreadStart(delegate
                    {
                        lblRandom.Text = $"{randInt.ToString()}";
                    }));

                    SetCursorPos(cursorWidth, cursorHeight);
                    //Thread.Sleep(180000);
                    Thread.Sleep(randInt);
                    //Thread.Sleep(5000);
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 300, 300, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 300, 300, 0);
                    this.BeginInvoke(new ThreadStart(delegate
                    {
                        lblCount.Text = totalCount.ToString() + " clicks";
                    }));

                    totalCount++;
                }
            });

            //SetCursorPos(85, 55);
            //timer.Start();
            //while (count != 0)
            //{
            //    //if (count == 50)
            //    //{
            //    //    count = 1;
            //    //    SetCursorPos(85, 55);
            //    //    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 85, 55, 0);
            //    //    mouse_event(MOUSEEVENTF_LEFTUP, 0, 85, 55, 0);
            //    //    Thread.Sleep(3000);
            //    //}
            //    //else
            //    //{
            //    //    lblCount.Text = totalCount.ToString() + " clicks";
            //    //    lblCount.Update();
            //    //    SetCursorPos(200, 200);
            //    //    Thread.Sleep(500);
            //    //    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 300, 300, 0);
            //    //    mouse_event(MOUSEEVENTF_LEFTUP, 0, 300, 300, 0);
            //    //}
            //    //count++;
            //    //totalCount++;

            //    lblCount.Text = totalCount.ToString() + " clicks";
            //    lblCount.Update();
            //    SetCursorPos(800, 450);
            //    Thread.Sleep(180000);
            //    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 300, 300, 0);
            //    mouse_event(MOUSEEVENTF_LEFTUP, 0, 300, 300, 0);
            //}
        }

        private void timer_Tick(object sender, EventArgs e)
        {
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnStop_Click(sender, e);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            count = 0;
            Thread.Sleep(10);
        }
    }
}