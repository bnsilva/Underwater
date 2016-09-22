using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Underwater
{
	public class SpriteManager : IGameObject
	{

		Rectangle finalRectangle;
		int spriteNumber, time, spriteJump;

		public void initialize()
		{
			finalRectangle = new Rectangle();

			spriteNumber = 0;
			time = 0;
			spriteJump = 1;
		}

		public void loadContent() { }

		public void update() { }

		public void draw(SpriteBatch spriteBatch) { }

		/// <summary>
		/// Movimenta o retangulo de recorte do sprite pela imagem
		/// </summary>
		/// <returns>O recorte atual do sprite.</returns>
		/// <param name="rectangle">Retangulo inicial do recorte do sprite.</param>
		/// <param name="spriteNumber">numero de sprites na animação.</param>
		/// <param name="time">Tempo entre um sprite e o outro</param>
		public Rectangle animationSprite(Rectangle rectangle, int spriteNumber, int time)
		{
			if (this.time == 0)
			{
				this.spriteNumber += spriteJump;

				if (this.spriteNumber > spriteNumber)
				{
					this.spriteJump = -1;
					this.spriteNumber -= 2;
				}

				if (this.spriteNumber < 0)
				{
					this.spriteJump = 1;
					this.spriteNumber = 1;
				}

				this.time = time;
			}
			this.time--;

			finalRectangle.X = rectangle.X + (this.spriteNumber*rectangle.Width);
			finalRectangle.Y = rectangle.Y;
			finalRectangle.Width = rectangle.Width;
			finalRectangle.Height = rectangle.Height;

			return finalRectangle;
		}
	}
}

