using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Underwater
{
	public class HUDManager : IGameObject
	{
		#region variables
		SpriteBatch spriteBatch;
		GraphicsDevice graphicsDevice;
		Player player;
		SpriteFont spriteFont;
		MapManager mapManager;
		Texture2D thermoIcon;
		ContentManager content;

		BarComponent lifeBar;
		BarComponent temperatureBar;
		BarComponent upgradeBar;

		TextComponent temperatureSea;
		TextComponent pressureSea;
		TextComponent lightSea;

		float lastTemperature, currentTemperature, lastLife, currentLife;
		#endregion

		public HUDManager(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice,ContentManager content, Player player, SpriteFont spriteFont, MapManager mapManager)
		{
			this.spriteBatch = spriteBatch;
			this.graphicsDevice = graphicsDevice;
			this.player = player;
			this.spriteFont = spriteFont;
			this.mapManager = mapManager;
			this.content = content;
		}

		public void initialize()
		{
			lifeBar = new BarComponent(new Vector2(10,10), new Vector2(100,20), 50f, 0, spriteBatch, graphicsDevice);
			temperatureBar = new BarComponent(new Vector2(10, 40), new Vector2(100, 30), 20f, 1, spriteBatch, graphicsDevice);
			upgradeBar = new BarComponent(player.getPosition(), new Vector2(50, 10), 200f, 2, spriteBatch, graphicsDevice);

			temperatureSea = new TextComponent("Temperatura do mar", new Vector2(10, 80), spriteBatch, spriteFont, graphicsDevice);
			pressureSea = new TextComponent("Pressao do mar", new Vector2(10, 110), spriteBatch, spriteFont, graphicsDevice);
			lightSea = new TextComponent("Luminosidade", new Vector2(10, 140), spriteBatch, spriteFont, graphicsDevice);

			lastTemperature = mapManager.getMap().getStatus().X;
			currentTemperature = mapManager.getMap().getStatus().X;
			lastLife = player.getLife();
			currentLife = player.getLife();
		}

		public void loadContent()
		{
			thermoIcon = content.Load<Texture2D>("thermoIcon");
		}

		public void update()
		{
			#region update temperature
			temperatureBar.update(lastTemperature);
			currentTemperature = mapManager.getMap().getStatus().X;

			if (lastTemperature != currentTemperature)
				lastTemperature = MathHelper.Lerp(lastTemperature, currentTemperature, 0.05f);
			#endregion

			#region update life
			lifeBar.update(lastLife);
			currentLife = player.getLife();
			if (lastLife != currentLife)
				lastLife = MathHelper.Lerp(lastLife, currentLife, 0.5f);

			#endregion

			upgradeBar.update(player.getUpgrade());
			upgradeBar.updatePosition(new Vector2(player.getPosition().X-9,player.getPosition().Y-20));

			temperatureSea.update(mapManager.getMap().getStatus().X.ToString(), Color.White);
			pressureSea.update(mapManager.getMap().getStatus().Y.ToString(), Color.White);
			lightSea.update(mapManager.getMap().getStatus().Z.ToString(), Color.White);
		}

		public void draw(SpriteBatch spriteBatch)
		{
			
			lifeBar.draw();
			temperatureBar.draw();

			if(player.isUpgrading)
				upgradeBar.draw();

			spriteBatch.Draw(thermoIcon, new Vector2(115, 38),null, Color.White,MathHelper.ToRadians(90),Vector2.Zero,.33f,SpriteEffects.None,0f);

			temperatureSea.draw();
			pressureSea.draw();
			lightSea.draw();
		}
	}
}
