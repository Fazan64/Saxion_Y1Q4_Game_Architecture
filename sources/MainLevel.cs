using System;
using Engine;
using System.Drawing;

namespace Pong
{
    /// The scene.
    public class MainLevel : GameObject
    {
        private GameObject ball;
        private GameObject leftPaddle;
        private GameObject rightPaddle; // TEMP. Also AI for now. TODO add a player-controlled paddle

        public MainLevel() : base("MainLevel") {}

        void Start()
        {
            AddTopAndBottomWalls();
            AddSideWalls();

            ball = AddBall();

            leftPaddle  = AddPaddle(20f, "PaddleLeft", ball);
            rightPaddle = AddPaddle(game.size.x - 1f - 20f, "PaddleRight", ball);

            AddBooster();

            new GameObject("ScoreTracker").Add<ScoreTracker>();
        }

        private void AddTopAndBottomWalls()
        {
            const float height = 100f;
            Rect rect;

            rect = new Rect(0f, -height    , game.size.x, height);
            AddAABBCollider("WallTop", rect);

            rect = new Rect(0f, game.size.y, game.size.x, height);
            AddAABBCollider("WallBottom", rect);
        }

        private void AddSideWalls()
        {
            const float width = 100f;
            Rect rect;

            rect = new Rect(-width     , 0f, width, game.size.y);
            AddAABBCollider("WallLeft", rect).Add<PointScoreDetector>().isRightPlayer = false;

            rect = new Rect(game.size.x, 0f, width, game.size.y);
            AddAABBCollider("WallRight", rect).Add<PointScoreDetector>().isRightPlayer = true;
        }

        private static GameObject AddAABBCollider(string name, Rect rect)
        {
            var go = new GameObject(name);
            go.Add<AABBCollider>().rect = rect;
            return go;
        }

        private static GameObject AddBall()
        {
            var go = new GameObject("Ball");
            go.Add<Ball>();
            go.Add<ImageRenderer>().SetImage("assets/ball.png");
            go.Add<Rigidbody>();
            go.Add<AABBCollider>().rect = new Rect(-4f, -4f, 8f, 8f);

            return go;
        }

        private void AddBooster()
        {
            // Boost zone
            var go = new GameObject("Booster");
            go.Add<Booster>();

            var imageRenderer = go.Add<ImageRenderer>();
            imageRenderer.SetImage("assets/booster.png");
            imageRenderer.pivot = Vector2.half;

            var aabb = go.Add<AABBCollider>();
            aabb.rect = Rect.FromCenterAndHalfDiagonal(Vector2.zero, Vector2.one * 16f);
            aabb.isTrigger = true;

            go.position = new Vector2(game.size.x * 0.5f, game.size.y * 0.25f);
        }

        private static GameObject AddPaddle(float x, string name, GameObject ball)
        {
            var go = new GameObject(name);
            go.position.x = x;
            go.Add<ImageRenderer>().SetImage("assets/paddle.png");
            go.Add<AABBCollider>().rect = new Rect(-4f, -32f, 8f, 64f);
            go.Add<Paddle>();
            go.Add<PaddleAI>().SetBall(ball); // TODO Make PaddleAI|s find the ball themselves.

            return go;
        }
    }
}
