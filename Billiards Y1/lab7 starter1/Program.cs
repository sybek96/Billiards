using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace lab7_starter1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Changed to use game loop from Graphics class
        /// </summary>

        [STAThread]
        static void Main()
        {
            DateTime currentUpdateTime;
            DateTime lastUpdateTime;
            TimeSpan frameTime;

            currentUpdateTime = DateTime.Now;
            lastUpdateTime = DateTime.Now;

            Form1 form = new Form1();
            form.Show();
            while (form.Created == true)
            {
                currentUpdateTime = DateTime.Now;
                frameTime = currentUpdateTime - lastUpdateTime;
                if (frameTime.TotalMilliseconds > 30)
                {
                    Application.DoEvents();
                    form.UpdateWorld();
                    form.Refresh();
                    lastUpdateTime = DateTime.Now;
                }

            }
        }
    }
}
