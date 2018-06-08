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
    class Game : Form
    {
        public const float FixedDeltaTime = 1f / 100f;

        public static Game main { get; private set; }

        public static readonly Random random = new Random(1);

        private readonly UpdateManager updateManager;
        private readonly RenderingManager renderingManager;

        private bool isRunning = false;
        private Ball ball;
        private AiPaddle leftPaddle;
        private AiPaddle rightPaddle; // just for demo sake: mind that also the controls are commented out now

        private Image b;
        private Image fontSheet;
        private uint leftScore, rightScore;

        public Vector2 size  => new Vector2(ClientSize.Width, ClientSize.Height);

        public Game()
        {
            Assert.IsNull(main);
            main = this;

            updateManager = new UpdateManager();
            renderingManager = new RenderingManager();

            BackColor = System.Drawing.Color.Black; // background color
            DoubleBuffered = true; // avoid flickering

            SuspendLayout(); // avoid artifacts while changing
            Text = "Spagetti--";
            ClientSize = new System.Drawing.Size(640, 480);
            ResumeLayout();

            fontSheet = Image.FromFile("assets/digits.png");
            b = Image.FromFile("assets/booster.png");

            AddGameObjects();

            Show(); // make form visible
            Run();
        }

        public void Add(EngineObject engineObject)
        {
            updateManager.Add(engineObject);
            renderingManager.Add(engineObject);
        }

        public void Remove(EngineObject engineObject)
        {
            updateManager.Remove(engineObject);
            renderingManager.Remove(engineObject);
        }

        private void Run()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            isRunning = true;

            float previous = stopwatch.ElapsedMilliseconds / 1000f;
            float lag = 0f;

            while (isRunning) // frame based loop, unsteady
            {
                float current = stopwatch.ElapsedMilliseconds / 1000f;
                float elapsed = current - previous;
                lag += elapsed;
                previous = current;

                Debug.WriteLine(1f / elapsed);

                Application.DoEvents();// pump form event queue, doing nothing with then

                while (lag >= FixedDeltaTime)
                {
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

            // TODO Move out everything below

            paint.Graphics.DrawImage(b, 320 - 16, 120 - 16);
            DisplayScore(paint.Graphics);

            //Debug.WriteLine("paint");
        }

        private void UpdateFixedTimestep()
        {
            updateManager.Step();

            // TODO Move out everything below

            CheckPaddleHit();

            // what am I doing here, forgot ;( 
            if (ball.x < 320 + 16 && ball.x + 16 > 320 - 16 && ball.y < 120 + 16 && ball.y + 16 > 120 - 16)
            {
                ball.SetBoost(true); // for 60 frames.... how long is that
            }

            CheckScore();
        }

        private void AddGameObjects()
        {
            ball = new Ball("Ball");
            ball.Add<ImageRenderer>().SetImage("assets/ball.png"); 

            leftPaddle = new AiPaddle("Left", 20, ball);
            leftPaddle.Add<ImageRenderer>().SetImage("assets/paddle.png");

            rightPaddle = new AiPaddle("Right", 639 - 20, ball);
            rightPaddle.Add<ImageRenderer>().SetImage("assets/paddle.png");
        }

        private void CheckPaddleHit()
        {
            // check for collisions with both paddles or when behind it and resolve
            if (ball.x < leftPaddle.x + 8 && ball.x + 16 > leftPaddle.x && ball.y < leftPaddle.y + 64 && ball.y + 16 > leftPaddle.y)
            {
                Console.WriteLine("Left Hit " + Math.Abs((leftPaddle.y - 32) - (ball.y + 8))); // just for testing
                                                                                               // change y speed depending on hit offset in y
                float dy = Math.Abs((leftPaddle.y - 32) - (ball.y + 8)) / 50.0f;

                ball.Resolve(leftPaddle.x + 8, ball.y, +Math.Abs(ball.velocity.x), dy * ball.velocity.y); // hmm
            }

            if (ball.x < rightPaddle.x + 8 && ball.x + 16 > rightPaddle.x && ball.y < rightPaddle.y + 64 && ball.y + 16 > rightPaddle.y)
            {
                Console.WriteLine("Right Hit");
                float dy = Math.Abs((rightPaddle.y - 32) - (ball.y + 8)) / 50.0f;
                ball.Resolve(rightPaddle.x - 16, ball.y, -Math.Abs(ball.velocity.x), dy * ball.velocity.y); // awful, but who cares, no one is gonna see this.
            }
        }

        private void CheckScore()
        {
            float x = ball.position.x;
            float y = ball.y;

            if (ball.position.x < 0f) // left score
            {
                rightScore++;
                ball.Reset();
            }
            else if (ball.position.x > 639f - 16f) // right score
            {
                leftScore++;
                ball.Reset();
            }
        }

        private void DisplayScore(Graphics graphics)
        {
            int digits = 2;
            int leftX = 240; // left score first
            int y = 25;
            string leftScore = "000" + this.leftScore.ToString(); // convert to string and add preceeding 000
            for (int d = 0; d < digits; d++)
            { // 3 digits left to right
                int digit = leftScore[leftScore.Length - digits + d] - 48; // '0' => 0 etc
                Rectangle rect = new Rectangle(digit * fontSheet.Width / 10, 0, fontSheet.Width / 10, fontSheet.Height);
                graphics.DrawImage(fontSheet, leftX + d * fontSheet.Width / 10, y, rect, GraphicsUnit.Pixel);
            }

            // oh no, same thing for right score !!!
            int rightX = 639 - 240 - digits * fontSheet.Width / 10;
            string rightScore = "000" + this.rightScore.ToString(); // convert to string and add preceeding 000
            for (int d = 0; d < digits; d++)
            { // 3 digits left to right
                int digit = rightScore[rightScore.Length - digits + d] - 48; // '0' => 0 etc
                Rectangle rect = new Rectangle(digit * fontSheet.Width / 10, 0, fontSheet.Width / 10, fontSheet.Height);
                graphics.DrawImage(fontSheet, rightX + d * fontSheet.Width / 10, y, rect, GraphicsUnit.Pixel);
            }
        }
    }
}
