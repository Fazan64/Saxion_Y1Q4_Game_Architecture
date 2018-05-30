using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spagetti
{
	class Game : Form
	{
		static void Main(string[] args)
		{
			Game game = new Game();
		}

		public static readonly Random random = new Random(1);
		private bool running = false;
		private Ball ball;
		private AiPaddle leftPaddle;
		private AiPaddle rightPaddle; // just for demo sake: mind that also the controls are commented now
		public Image b;
		private uint leftScore, rightScore;
		private Image fontSheet;

		public Game()
		{
			BackColor = System.Drawing.Color.Black; // background color
			DoubleBuffered = true; // avoid flickering
			SuspendLayout(); // avoid artifacts while changing
				Text = "Spagetti--";
				ClientSize = new System.Drawing.Size(640, 480);
			ResumeLayout();
			ball = new Ball("Ball", "assets/ball.png");
			leftPaddle = new AiPaddle("Left", "assets/paddle.png", 20, ball);
			rightPaddle = new AiPaddle("Right", "assets/paddle.png", 639-20, ball);
			fontSheet = Image.FromFile("assets/digits.png");

			b = Image.FromFile("assets/booster.png");

			Show(); // make form visible
			Run();
		}

		private void Run()
		{
			running = true;
			while( running ) // frame based loop, unsteady
			{
				Application.DoEvents();// pump form event queue, doing nothing with then
				Refresh(); // causes OnPaint event
			}
		}

		protected override void OnPaint( PaintEventArgs paint )
		{
			// update and paint all objects
			base.OnPaint( paint );
			ball.Update( paint.Graphics );
			leftPaddle.Update( paint.Graphics );
			rightPaddle.Update( paint.Graphics );

			paint.Graphics.DrawImage(b, 320-16, 120-16);


			CheckPaddleHit();

			// what am I doing here, forgot ;( 
			if( ball.X < 320+16 && ball.X + 16 > 320-16 && ball.Y < 120+16 && ball.Y + 16 > 120-16 )
			{
				ball.SetBoost( true ); // for 60 frames.... how long is that
			}

			CheckScore();
			DisplayScore( paint.Graphics );
		}

		private void CheckPaddleHit( )
		{
			// check for collisions with both paddles or when behind it and resolve
			if( ball.X < leftPaddle.X+8 && ball.X+16 > leftPaddle.X && ball.Y < leftPaddle.Y+64 && ball.Y+16 > leftPaddle.Y )
			{
				Console.WriteLine("Left Hit "+Math.Abs((leftPaddle.Y - 32) - (ball.Y + 8))); // just for testing
				// change y speed depending on hit offset in y
				float dy = Math.Abs((leftPaddle.Y - 32) - (ball.Y + 8)) / 50.0f;


				ball.Resolve( leftPaddle.X + 8, ball.Y, +Math.Abs( ball.mx ), dy * ball.my ); // hmm
			}
			if( ball.X < rightPaddle.X+8 && ball.X+16 > rightPaddle.X && ball.Y < rightPaddle.Y+64 && ball.Y+16 > rightPaddle.Y )
			{
				Console.WriteLine("Right Hit");
				float dy = Math.Abs((rightPaddle.Y - 32) - (ball.Y + 8)) / 50.0f;
				ball.Resolve( rightPaddle.X - 16, ball.Y, -Math.Abs( ball.mx ), dy * ball.my ); // awfull, but who cares, no one is gonna see this.
			}

		}

		private void CheckScore()
		{
			float x = ball.X;
			float y = ball.Y;

			if ( x < 0 ) // left score
			{
				rightScore++;
				ball.init();
			}
			if( x > 639-16) // right score
			{
				leftScore++;
				ball.init();
			}

		}

		private void DisplayScore( Graphics graphics)
		{
			int digits = 2;
			int leftX = 240; // left score first
			int y = 25;
			string leftScore = "000"+ this.leftScore.ToString(); // convert to string and add preceeding 000
			for( int d=0; d<digits; d++ ) { // 3 digits left to right
				int digit = leftScore[ leftScore.Length-digits + d ] - 48; // '0' => 0 etc
				Rectangle rect = new Rectangle( digit * fontSheet.Width/10, 0, fontSheet.Width/10, fontSheet.Height );
				graphics.DrawImage( fontSheet, leftX + d*fontSheet.Width/10, y, rect, GraphicsUnit.Pixel );
			}

			// oh no, same thing for right score !!!
			int rightX = 639-240-digits*fontSheet.Width/10;
			string rightScore = "000"+ this.rightScore.ToString(); // convert to string and add preceeding 000
			for( int d=0; d<digits; d++ ) { // 3 digits left to right
				int digit = rightScore[ rightScore.Length-digits + d ] - 48; // '0' => 0 etc
				Rectangle rect = new Rectangle( digit * fontSheet.Width/10, 0, fontSheet.Width/10, fontSheet.Height );
				graphics.DrawImage( fontSheet, rightX + d*fontSheet.Width/10, y, rect, GraphicsUnit.Pixel );
			}

		}
	}
}
