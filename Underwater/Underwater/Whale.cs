using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Underwater
{
	public class Whale : IGameObject
	{
		#region variables
		GraphicsDeviceManager graphics;
		ContentManager content;
		Vector2 position;
		Texture2D texture;
		SpriteEffects effect;

		float speed, rotation, scale, layerDepth;

		#endregion

		public Whale(GraphicsDeviceManager graphics, ContentManager content)
		{
			this.graphics = graphics;
			this.content = content;
		}

		public void initialize()
		{
			position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
			speed = -0.05f;
			rotation = 0f;
			scale = 0.5f;
			layerDepth = 1f;
		}

		public void loadContent()
		{
			texture = content.Load<Texture2D>("whaleTexture");
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
				new Rectangle(new Point(0,0),new Point(texture.Width,texture.Height)), 
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
