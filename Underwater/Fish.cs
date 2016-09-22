using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Underwater
{
	public class Fish : IGameObject
	{

		#region variables

		GraphicsDeviceManager graphics;
		ContentManager content;
		SpriteManager spriteManager;
		Texture2D texture;
		Vector2 position;
		Rectangle sourceRectangle;
		Random random;
		float speed, rotation, scale, layerDepth;
		int spriteNumber, spriteTime, minimumSprite, ySourceRectangle;

		#endregion

		public Fish(GraphicsDeviceManager graphics, ContentManager content)
		{
			this.graphics = graphics;
			this.content = content;
		}

		public void initialize()
		{
			#region fixed variables
			spriteTime = 15;
			spriteNumber = 2;
			rotation = 0f;
			layerDepth = 1f;
			ySourceRectangle = 70;
			#endregion

			#region random variables

			random = new Random();
			minimumSprite = random.Next(4) * 3;
			speed = random.Next(-3, 3) * 0.2f;
			scale = random.Next(2, 4) * 0.5f;

			if (speed == 0)
				speed = 1;

			if (speed < 0)
				ySourceRectangle = 40;
			#endregion

			#region objects
			spriteManager = new SpriteManager();
			sourceRectangle = new Rectangle(32 * minimumSprite, ySourceRectangle, 32, 16);
			position = new Vector2(random.Next(700), random.Next(500));
			#endregion

			spriteManager.initialize();
		}

		public void loadContent()
		{
			texture = content.Load<Texture2D>("overheadfishcrdusty");
		}

		public void update()
		{
			#region movement limit
			if (position.X > 800)
			{
				sourceRectangle.Y = 40;
				speed *= -1;
			}

			if (position.X < 0)
			{
				sourceRectangle.Y = 70;
				speed *= -1;
			}
			#endregion

			position.X += speed;
		}

		public void draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				texture,
				position,
				spriteManager.animationSprite(sourceRectangle, spriteNumber, spriteTime),
				Color.White,
				rotation,
				Vector2.Zero,
				scale,
				SpriteEffects.None,
				layerDepth
			);
		}
	}
}

