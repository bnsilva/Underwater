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
		Map[] maps;

		int numberOfMaps, depthMap;

		public MapManager(GraphicsDeviceManager graphics, ContentManager content, Player player)
		{
			this.graphics = graphics;
			this.content = content;
			this.player = player;
		}

		public void initialize()
		{
			numberOfMaps = 3;
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
					if (player.getLevel() > depthMap)
					{
						depthMap++;
						player.resetPosition(player.getPosition().X, 0);
						Map.mapChangedDown = false;
					}
					else
					{
						player.resetPosition(player.getPosition().X, graphics.GraphicsDevice.Viewport.Height);
						Map.mapChangedDown = false;
					}
				}
				else {
					player.resetPosition(player.getPosition().X, graphics.GraphicsDevice.Viewport.Height);
					Map.mapChangedDown = false;
				}
			}

			if (Map.mapChangedUp)
			{
				if (depthMap != 0)
				{
					depthMap--;
					player.resetPosition(player.getPosition().X, graphics.GraphicsDevice.Viewport.Height);
					Map.mapChangedUp = false;
				}
				else {
					player.resetPosition(player.getPosition().X, 0);
					Map.mapChangedUp = false;
				}
			}
		}
	}
}
