using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using NUnit.Framework;
using Engine.Internal;

namespace Engine
{
    /// A single game instance. 
    /// To use the engine, create an instance of Game, 
    /// and call Run after creating gameobjects your game needs.
    public class Game
    {
        public const float FixedDeltaTime = 1f / 100f;

        public static Game main { get; private set; }

        // TODO extract into a service.
        public static readonly Random random = new Random(1);

        public readonly Vector2 size;

        private readonly HashSet<EngineObject> addedObjects;

        private readonly RenderingManager renderingManager;
        private readonly UpdateManager    updateManager;
        private readonly PhysicsManager   physicsManager;
        private readonly EventsManager    eventsManager;

        private readonly GameForm form;

        private bool isRunning = false;

        public Game(string title, int resolutionX, int resolutionY)
        {
            Assert.IsNull(main);
            main = this;

            addedObjects = new HashSet<EngineObject>();

            renderingManager = new RenderingManager();
            updateManager    = new UpdateManager();
            physicsManager   = new PhysicsManager();
            eventsManager    = new EventsManager();

            size = new Vector2(resolutionX, resolutionY);
            form = new GameForm(title, resolutionX, resolutionY);
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
                Application.DoEvents(); // pump form event queue, doing nothing with them
                form.Refresh(); // causes OnPaint event

                float current = stopwatch.ElapsedMilliseconds / 1000f;
                float elapsed = current - previous;
                lag += elapsed;
                previous = current;

                Debug.WriteLine($"fps: {1f / elapsed}");

                while (lag >= FixedDeltaTime)
                {
                    UpdateFixedTimestep();
                    Debug.WriteLine($"lag: {lag}");
                    lag -= FixedDeltaTime;
                }
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
