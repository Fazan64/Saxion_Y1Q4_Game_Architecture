using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NUnit.Framework;

namespace Engine.Internal
{
    public class GameForm : Form
    {
        public GameForm(string title)
        {
            BackColor = Color.Black; // background color
            DoubleBuffered = true;   // to avoid flickering

            SuspendLayout(); // avoid artifacts while changing
            Text = title;
            ClientSize = new Size(640, 480);
            ResumeLayout();
        }
    }
}
