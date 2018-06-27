using System;
using Engine;
using System.Drawing;

namespace Spaghetti
{
    public class MainLevel : GameObject
    {
        private GameObject ball;
        private GameObject leftPaddle;
        private GameObject rightPaddle; // TEMP. Also AI for now. TODO add a player-controlled paddle

        public MainLevel() : base("MainLevel") {}

        void Start()
        {
            ball = new GameObject("Ball");
            ball.Add<Ball>();
            ball.Add<ImageRenderer>().SetImage("assets/ball.png");
            ball.Add<Rigidbody>();
            ball.Add<AABB>().rect = new Rect(-4f, -4f, 8f, 8f);

            leftPaddle  = AddPaddle(20f, "PaddleLeft", ball);
            rightPaddle = AddPaddle(game.size.x - 1f - 20f, "PaddleRight", ball);

            AddBooster();

            new GameObject("ScoreTracker").Add<ScoreTracker>();
        }

        private void AddBooster()
        {
            // Boost zone
            var go = new GameObject("Booster");

            // TODO Add the collision detection engine to remove this dependency.
            go.Add<Booster>().ball = ball;

            var imageRenderer = go.Add<ImageRenderer>();
            imageRenderer.SetImage("assets/booster.png");
            imageRenderer.pivot = Vector2.half;

            go.position = new Vector2(game.size.x * 0.5f, game.size.y * 0.25f);
        }

        private static GameObject AddPaddle(float x, string name, GameObject ball)
        {
            var go = new GameObject(name);
            go.position.x = x;
            go.Add<ImageRenderer>().SetImage("assets/paddle.png");
            go.Add<AABB>().rect = new Rect(-4f, -32f, 8f, 64f);
            go.Add<Paddle>();
            go.Add<PaddleAI>().SetBall(ball);

            return go;
        }
    }
}
