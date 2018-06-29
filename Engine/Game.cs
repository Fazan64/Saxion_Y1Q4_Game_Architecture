using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using NUnit.Framework;
using Engine.Internal;

namespace Engine
{
    public class Game
    {
        public const float FixedDeltaTime = 1f / 100f;

        public static Game main { get; private set; }

        // TODO extract into a service.
        public static readonly Random random = new Random(1);

        private readonly HashSet<EngineObject> addedObjects;

        private readonly UpdateManager    updateManager;
        private readonly PhysicsManager   physicsManager;
        private readonly RenderingManager renderingManager;
        private readonly EventsManager    eventsManager;

        private readonly GameForm form;

        private bool isRunning = false;

        public Vector2 size  => new Vector2(form.ClientSize.Width, form.ClientSize.Height);

        public Game()
        {
            Assert.IsNull(main);
            main = this;

            addedObjects = new HashSet<EngineObject>();

            updateManager    = new UpdateManager();
            physicsManager   = new PhysicsManager();
            renderingManager = new RenderingManager();
            eventsManager    = new EventsManager();

            form = new GameForm("Penne");
            form.Paint += OnFormPaint;
        }

        public void Add(EngineObject engineObject)
        {
            updateManager.Add(engineObject);
            physicsManager.Add(engineObject);
            renderingManager.Add(engineObject);
            eventsManager.Add(engineObject);

            addedObjects.Add(engineObject);
        }

        public void Remove(EngineObject engineObject)
        {
            updateManager.Remove(engineObject);
            physicsManager.Remove(engineObject);
            renderingManager.Remove(engineObject);
            eventsManager.Remove(engineObject);

            addedObjects.Remove(engineObject);
        }

        public void DestroyAll()
        {
            foreach (EngineObject engineObject in addedObjects)
            {
                engineObject.Destroy();
            }
        }

        internal void Post(IBroadcastEvent engineEvent)
        {
            eventsManager.Post(engineEvent);
        }

        public void Run()
        {
            Assert.IsFalse(isRunning);
            form.Show();

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

                //Debug.WriteLine($"fps: {1f / elapsed}");

                Application.DoEvents(); // pump form event queue, doing nothing with them

                while (lag >= FixedDeltaTime)
                {
                    UpdateFixedTimestep();
                    lag -= FixedDeltaTime;
                }

                form.Refresh(); // causes OnPaint event
            }
        }

        void OnFormPaint(object sender, PaintEventArgs paint)
        {
            renderingManager.Render(paint.Graphics);
            //Debug.WriteLine("paint");
        }

        private void UpdateFixedTimestep()
        {
            physicsManager.Step();
            eventsManager.DeliverEvents();
            updateManager.Step();
        }
    }
}
