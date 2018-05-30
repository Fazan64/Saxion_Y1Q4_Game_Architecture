using System;
using System.Drawing;
using System.Windows.Forms;


namespace Spagetti
{
	class Ball
	{
		private string name;
		private Image image;
		private float x , y;
		public float mx = 1, my; // mx needs init value, hmm
		public bool boosting = false;
		public int stunnedCounter = 0;

		public Ball( string name, string file )
		{
			this.name = name;
			this.image = Image.FromFile(file);
			init();
		}

		public void init()
		{
			this.x = 320 - 5;
			this.y = 240 - 5;
			this.mx = 0.5f * Math.Sign( this.mx ); // to looser
			Console.WriteLine( Math.Sign( this.mx ) );
			this.my = 0.15f * (float)(1.0 + Game.random.NextDouble()-0.5); // random around = or - 0.15f;
			this.boosting = false;
			this.stunnedCounter = 1000; 
		}

		public void Update( Graphics graphics )
		{
			stunnedCounter--;
			if( stunnedCounter <= 0 ) {

				// Ranging mx to -16..16 using Min/Max
				x += Math.Min( 16, Math.Max( -16, boosting ? mx * 2.0f : mx ) ); // limit mx to 2x paddlewith
				y += Math.Max( -16, Math.Min( boosting ? my * 2.0f : my, 16 ) ); // limit to same

			if ( y < 0 )
			{
			y = 0;
			my *= -1;
			}
			if( y > 479-16)
			{
			y = 479-16;
			my *= -1;
			}
			}



			// render;
			graphics.DrawImage(image, x, y);
			if( boosting )
			{
				graphics.DrawEllipse(Pens.White, x - 5, y - 5, 26, 26); 
			}
		}

		public void Resolve( float x, float y, float mx, float my)
		{
			this.x = x;
			this.y = y;
			this.mx = mx;
			this.my = my;
			boosting = false;
		}

		public void SetBoost( bool value )
		{
			this.boosting = value;
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
