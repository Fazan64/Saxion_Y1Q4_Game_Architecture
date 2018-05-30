using System;
using System.Drawing;
using System.Windows.Forms;


namespace Spagetti
{
	class AiPaddle
	{
		private string name;
		private Image image;
		private float x, y;
		private float mx, my;
		private Ball ball;

		public AiPaddle( string name, string file, float x, Ball ball )
		{
			this.name = name;
			this.image = Image.FromFile(file);
			Console.WriteLine(this.image.Size);
			this.x = x - 8;
			this.y = ball.Y-32+8;
			this.mx = 0;
			this.my = 0;
			this.ball = ball;
		}

		public void Update( Graphics graphics )
		{
		// input/events;
		// no input

		// move, track the ball's y;
		float factor = 1 - Math.Abs( x - ball.X) /640.0f;
		float targetY = ball.Y-32+8;
		y += factor * (targetY - y) / 125;

		// detect hitting wall
		if( y < 0+4 )
		{
		y = 0+4;
		}
		if( y > 479-4-64)
		{
		y = 479-4-64;
		}

		// render;
		graphics.DrawImage(image, x, y);
		}

		public float X
		{
			get
			{
				return x;
			}
		}
		public float Y
		{
			get
			{
				return y;
			}
		}
	}
}
