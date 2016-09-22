using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Underwater
{
	public class Map : IGameObject
	{

		#region variables
		GraphicsDeviceManager graphics;
		ContentManager content;
		Player player;
		Texture2D background;
		EnemyManager enemyManager;
		Color color;

		public static bool mapChangedDown, mapChangedUp;
		int pressure, temperature, lighting, depth;

		#endregion

		public Map(GraphicsDeviceManager graphics, ContentManager content, Player player, int depth)
		{
			this.content = content;
			this.graphics = graphics;
			this.player = player;
			this.depth = depth;
		}

		public void initialize()
		{
			enemyManager = new EnemyManager(graphics, content);
			enemyManager.initialize();

			mapChangedDown = false;
			mapChangedUp = false;

			pressure = 10 * depth;
			temperature = 10 * depth;
			lighting = 1 * depth;

			#region colorMap
			color = Color.White;
			if (depth == 1)
				color = Color.LightCoral;

			if (depth == 2)
				color = Color.CornflowerBlue;

			if (depth == 3)
				color = Color.Blue;

			if (depth == 4)
				color = Color.DarkBlue;
			#endregion
		}

		public void loadContent()
		{
			background = content.Load<Texture2D>("background");
			enemyManager.loadContent();
		}

		public void update() 
		{
			enemyManager.update();
			player.update();

			if (player.getPosition().Y > graphics.GraphicsDevice.Viewport.Height)
				mapChangedDown = true;

			if (player.getPosition().Y < 0)
				mapChangedUp = true;

		}

		public void draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(background, Vector2.Zero, color);
			enemyManager.draw(spriteBatch);
			player.draw(spriteBatch);
		}
	}
}

