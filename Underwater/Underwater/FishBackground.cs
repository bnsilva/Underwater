using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Underwater
{
	public class FishBackground : IGameObject
	{

		#region variables

		GraphicsDeviceManager graphics;
		ContentManager content;
		Texture2D texture;
		Vector2 position;
		Random random;
		SpriteEffects effect;
		float speed, rotation, scale, layerDepth;
		int type;

		#endregion

		public FishBackground(GraphicsDeviceManager graphics, ContentManager content)
		{
			this.graphics = graphics;
			this.content = content;
		}

		public void initialize()
		{
			#region random variables
			random = new Random();
			position = new Vector2(random.Next(graphics.GraphicsDevice.Viewport.Width),
								   random.Next(graphics.GraphicsDevice.Viewport.Height));
			
			speed = random.Next(-10, 10) * 0.01f;
			if (speed == 0f)
				speed = 0.2f;

			#endregion

			#region fixe variables
			scale = random.Next(5, 10) * 0.05f;
			rotation = 0f;
			layerDepth = 1f;
			type = random.Next(2);
			#endregion

		}

		public void loadContent()
		{
			if(type == 0)
				texture = content.Load<Texture2D>("fishBackground");

			if (type == 1)
				texture = content.Load<Texture2D>("fishBackground2");
		}

		public void update()
		{
			#region movement limit

			if (position.X > graphics.GraphicsDevice.Viewport.Width)
				speed *= -1;

			if (position.X < 0)
				speed *= -1;
			
			#endregion

			#region sprite direction control
			if (speed > 0)
				effect = SpriteEffects.None;
			else
				effect = SpriteEffects.FlipHorizontally;
			#endregion
		
			position.X += speed;
		}

		public void draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				texture, 
				position, 
				new Rectangle(new Point(0, 0), new Point(texture.Width, texture.Height)), 
				Color.White, 
				rotation, 
				Vector2.Zero, 
				scale, 
				effect, 
				layerDepth
			);
		}
	}
}
