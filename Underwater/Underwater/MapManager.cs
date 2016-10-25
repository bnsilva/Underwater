using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Underwater
{
	public class MapManager : IGameObject
	{
		GraphicsDeviceManager graphics;
		ContentManager content;
		Player player;
		Game1 game;
		Map[] maps;

		int numberOfMaps, depthMap;

		public MapManager(Game1 game, GraphicsDeviceManager graphics, ContentManager content, Player player)
		{
			this.graphics = graphics;
			this.content = content;
			this.player = player;
			this.game = game;
		}

		public void initialize()
		{
			numberOfMaps = 4;
			depthMap = 0;

			maps = new Map[numberOfMaps];

			for (int i = 0; i < numberOfMaps; i++)
			{
				maps[i] = new Map(graphics, content, player, i+1);
			}

			foreach (Map map in maps)
				map.initialize();
		}

		public void loadContent()
		{
			foreach (Map map in maps)
				map.loadContent();
		}

		public void update()
		{
			mapChanger();
			checkStatusPlayer();
			maps[depthMap].update();

		}

		public void draw(SpriteBatch spriteBatch) 
		{
			maps[depthMap].draw(spriteBatch);
		}

		public void mapChanger()
		{
			if (Map.mapChangedDown)
			{
				if (depthMap != maps.Length - 1)
				{
					depthMap++;
					player.resetPosition(player.getPosition().X, 0);
					Map.mapChangedDown = false;
				}
				else {
					player.resetPosition(player.getPosition().X, graphics.GraphicsDevice.Viewport.Height - 14);
					Map.mapChangedDown = false;
				}
			}

			if (Map.mapChangedUp)
			{
				if (depthMap != 0)
				{
					depthMap--;
					player.resetPosition(player.getPosition().X, graphics.GraphicsDevice.Viewport.Height - 15);
					Map.mapChangedUp = false;
				}
				else {
					player.resetPosition(player.getPosition().X, 0);
					Map.mapChangedUp = false;
				}
			}
		}

		public Map getMap()
		{
			return maps[depthMap];
		}

		public void checkStatusPlayer()
		{
			if (player.getLevel().X > maps[depthMap].getStatus().X ||
				player.getLevel().Y < maps[depthMap].getStatus().Y)
			{
				player.receiveDamage();
			}
			else
				player.receiveHealing();

			if (player.getLife() == 0)
			{
				gameOver();
			}
		}

		public void gameOver()
		{
			game.Exit();
		}
	}
}
