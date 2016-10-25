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
		SpecialFish specialFish;
		Texture2D background, background2;
		EnemyManager enemyManager;
		Color color;
		Whale whale;

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
			enemyManager = new EnemyManager(graphics, content, depth, 20, Color.White);
			enemyManager.initialize();

			specialFish = new SpecialFish(graphics, content,depth);
			specialFish.initialize();

			whale = new Whale(graphics, content);
			whale.initialize();

			mapChangedDown = false;
			mapChangedUp = false;

			pressure = 1;
			temperature = 10;
			lighting = 1;


			#region typeMap define
			if (depth == 1)
			{
				temperature = 20;
				pressure = 1;
				lighting = 1;
			}

			if (depth == 2)
			{
				temperature = 15;
				pressure = 1;
				lighting = 1;
			}

			if (depth == 3)
			{
				temperature = 10;
				pressure = 2;
				lighting = 1;
			}

			if (depth == 4)
			{
				temperature = 5;
				pressure = 3;
				lighting = 2;
			}

			#endregion

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
			background2 = content.Load<Texture2D>("background2");
			enemyManager.loadContent();
			specialFish.loadContent();
			whale.loadContent();
		}

		public void update() 
		{
			enemyManager.update();
			player.update();
			specialFish.update();
			player.getCollision(specialFish);
			whale.update();

			#region mapChange
			if (player.getPosition().Y > graphics.GraphicsDevice.Viewport.Height - 14)
				mapChangedDown = true;
			else
				mapChangedDown = false;

			if (player.getPosition().Y < 0)
				mapChangedUp = true;
			else
				mapChangedUp = false;

			#endregion
		}

		public void draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(background, new Rectangle(new Point(0,0), new Point(graphics.GraphicsDevice.Viewport.Width,graphics.GraphicsDevice.Viewport.Height)),color);
			//spriteBatch.Draw(background2, new Rectangle(new Point(0, 0), new Point(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height)), new Color(Color.DarkBlue, 0.1f));

			if (depth == 3)
				whale.draw(spriteBatch);
			
			enemyManager.draw(spriteBatch);
			player.draw(spriteBatch);
			specialFish.draw(spriteBatch);
		}

		public Vector3 getStatus()
		{
			return new Vector3(temperature, pressure, lighting);
		}
	}
}

