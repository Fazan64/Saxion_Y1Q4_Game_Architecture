using System;
using Engine;
using System.Drawing;

namespace Spaghetti
{
    public class MainLevel : GameObject, IRenderer, IEventReceiver<PointScoreEvent>
    {
        private Image fontSheet;
        private uint leftScore, rightScore;

        private GameObject ball;
        private AiPaddle leftPaddle;
        private AiPaddle rightPaddle; // TEMP. Also AI for now. TODO add a player-controlled paddle

        public MainLevel() : base("MainLevel")
        {
            fontSheet = Image.FromFile("assets/digits.png");
        }

        void Start()
        {
            ball = new GameObject("Ball");
            ball.Add<Ball>();
            ball.Add<ImageRenderer>().SetImage("assets/ball.png");
            ball.Add<Rigidbody>();

            leftPaddle = new AiPaddle("Left", 20, ball);
            leftPaddle.Add<ImageRenderer>().SetImage("assets/paddle.png");

            rightPaddle = new AiPaddle("Right", 639 - 20, ball);
            rightPaddle.Add<ImageRenderer>().SetImage("assets/paddle.png");

            // Boost zone
            var go = new GameObject("BoostZone");

            go.Add<BoostZone>().ball = ball;

            var imageRenderer = go.Add<ImageRenderer>();
            imageRenderer.SetImage("assets/booster.png");
            imageRenderer.pivot = Vector2.half;

            go.position = new Vector2(game.size.x * 0.5f, game.size.y * 0.25f);
        }

        void Update()
        {
            CheckPaddleHit();
        }

        void IRenderer.Render(Graphics graphics)
        {
            // BUG: Start is not guarranteed to be called before the first Render.
            //graphics.DrawImage(b, boostZonePosition.x - 16, boostZonePosition.y - 16);
            DisplayScore(graphics);
        }

        public void On(PointScoreEvent pointScore)
        {
            if (pointScore.rightPlayerScored)
            {
                rightScore++;
            }
            else
            {
                leftScore++;
            }
        }

        private void CheckPaddleHit()
        {
            Rigidbody ballRb = ball.Get<Rigidbody>();
            // check for collisions with both paddles or when behind it and resolve
            if (ball.x < leftPaddle.x + 8 && ball.x + 16 > leftPaddle.x && ball.y < leftPaddle.y + 64 && ball.y + 16 > leftPaddle.y)
            {
                Console.WriteLine("Left Hit " + Math.Abs((leftPaddle.y - 32) - (ball.y + 8))); // just for testing
                                                                                               // change y speed depending on hit offset in y
                float dy = Math.Abs((leftPaddle.y - 32) - (ball.y + 8)) / 50.0f;

                ball.Get<Ball>().Resolve(leftPaddle.x + 8, ball.y, +Math.Abs(ballRb.velocity.x), dy * ballRb.velocity.y); // hmm
            }

            if (ball.x < rightPaddle.x + 8 && ball.x + 16 > rightPaddle.x && ball.y < rightPaddle.y + 64 && ball.y + 16 > rightPaddle.y)
            {
                Console.WriteLine("Right Hit");
                float dy = Math.Abs((rightPaddle.y - 32) - (ball.y + 8)) / 50.0f;
                ball.Get<Ball>().Resolve(rightPaddle.x - 16, ball.y, -Math.Abs(ballRb.velocity.x), dy * ballRb.velocity.y); // awful, but who cares, no one is gonna see this.
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
