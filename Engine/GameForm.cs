using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NUnit.Framework;

namespace Engine.Internal
{
    /// A simple Form which encapsulates Windows Forms-related setup.
    public class GameForm : Form
    {
        public GameForm(string title, int resolutionX, int resolutionY)
        {
            BackColor = Color.Black; // background color
            DoubleBuffered = true;   // to avoid flickering

            SuspendLayout(); // avoid artifacts while changing
            Text = title;
            ClientSize = new Size(resolutionX, resolutionY);
            ResumeLayout();
        }
    }
}
