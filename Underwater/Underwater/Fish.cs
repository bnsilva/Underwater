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
		protected Color color;
		protected Texture2D texture;
		protected Vector2 position;
		Rectangle sourceRectangle;
		Random random;
		float speed, rotation, scale, layerDepth;
		int spriteNumber, spriteTime, minimumSprite, ySourceRectangle, type, depth;

		#endregion

		public Fish(GraphicsDeviceManager graphics, ContentManager content, int type, Color color,int depth, int layerDepth)
		{
			this.graphics = graphics;
			this.content = content;
			this.type = type-1;
			this.color = color;
			this.depth = depth;
			this.layerDepth = layerDepth;
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
			minimumSprite = type * 3;
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
			position = new Vector2(random.Next(graphics.GraphicsDevice.Viewport.Width), random.Next(graphics.GraphicsDevice.Viewport.Height));
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
			if (position.X > graphics.GraphicsDevice.Viewport.Width)
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
			position.Y += (float)Math.Cos(position.X / 10) * .2f;
		}

		public void draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				texture,
				position,
				spriteManager.animationSprite(sourceRectangle, spriteNumber, spriteTime),
				color,
				rotation,
				Vector2.Zero,
				scale,
				SpriteEffects.None,
				layerDepth
			);
		}
	}
}

