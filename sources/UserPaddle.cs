using System;
using System.Drawing;
using System.Windows.Forms;


namespace Spagetti
{
	class UserPaddle
	{
		private string name;
		private Image image;
		private float x, y;
		private float mx, my;
		private Ball ball;

		public UserPaddle( string name, string file, float x, Ball ball )
		{
			this.name = name;
			this.image = Image.FromFile(file);
			Console.WriteLine(this.image.Size);
			this.x = x - 8;
			this.y = 240 - 32;
			this.mx = 0;
			this.my = 0;
			this.ball = ball;
		}

		public void Update( Graphics graphics )
		{
			// input/events;
				// no input

			// move, only y;
			y += my;

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

		public void Up()
		{
			my = my-0.1f > -0.1f ? my-0.1f : -0.1f;
		}

		public void Down()
		{
			my = my+0.1f < +0.1f ? my+0.1f : +0.1f;
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

