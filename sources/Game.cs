using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using NUnit.Framework;

using Spaghetti;
using Engine;

static class Program
{
    static void Main(string[] args)
    {
        var game = new Game();
    }
}

// TEMP In Engine for now
namespace Engine
{
    public class Game : Form
    {
        public const float FixedDeltaTime = 1f / 100f;

        public static Game main { get; private set; }

        public static readonly Random random = new Random(1);

        private readonly UpdateManager updateManager;
        private readonly RenderingManager renderingManager;
        private readonly EventsManager eventsManager;

        private bool isRunning = false;

        public Vector2 size  => new Vector2(ClientSize.Width, ClientSize.Height);

        public Game()
        {
            Assert.IsNull(main);
            main = this;

            updateManager = new UpdateManager();
            renderingManager = new RenderingManager();
            eventsManager = new EventsManager();

            BackColor = System.Drawing.Color.Black; // background color
            DoubleBuffered = true; // avoid flickering

            SuspendLayout(); // avoid artifacts while changing
            Text = "Spagetti--";
            ClientSize = new System.Drawing.Size(640, 480);
            ResumeLayout();

            var level = new MainLevel();

            Show(); // make form visible
            Run();
        }

        public void Add(EngineObject engineObject)
        {
            updateManager.Add(engineObject);
            renderingManager.Add(engineObject);
            eventsManager.Add(engineObject);
        }

        public void Remove(EngineObject engineObject)
        {
            updateManager.Remove(engineObject);
            renderingManager.Remove(engineObject);
            eventsManager.Remove(engineObject);
        }

        internal void Post(IEngineEvent engineEvent)
        {
            eventsManager.Post(engineEvent);
        }

        private void Run()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            isRunning = true;

            float previous = stopwatch.ElapsedMilliseconds / 1000f;
            float lag = 0f;

            while (isRunning)
            {
                float current = stopwatch.ElapsedMilliseconds / 1000f;
                float elapsed = current - previous;
                lag += elapsed;
                previous = current;

                Debug.WriteLine(1f / elapsed);

                Application.DoEvents();// pump form event queue, doing nothing with then

                while (lag >= FixedDeltaTime)
                {
                    eventsManager.DeliverEvents();
                    UpdateFixedTimestep();
                    lag -= FixedDeltaTime;
                }

                //Debug.WriteLine("refresh");
                Refresh(); // causes OnPaint event
            }
        }

        protected override void OnPaint(PaintEventArgs paint)
        {
            base.OnPaint(paint);

            renderingManager.Render(paint.Graphics);

            //Debug.WriteLine("paint");
        }

        private void UpdateFixedTimestep()
        {
            updateManager.Step();
        }
    }
}
