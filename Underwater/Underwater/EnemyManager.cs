using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Underwater
{
	public class EnemyManager : IGameObject
	{

		#region variables

		GraphicsDeviceManager graphics;
		ContentManager content;
		Fish[] fishes;
		int fishNumber, cont;

		#endregion

		public EnemyManager(GraphicsDeviceManager graphics, ContentManager content)
		{
			this.content = content;
			this.graphics = graphics;
		}

		public void initialize()
		{
			fishNumber = 20;
			cont = 0;
			fishes = new Fish[fishNumber];
		}

		public void loadContent()
		{
			
		}

		public void update()
		{

			if (cont < fishNumber)
			{
				fishes[cont] = new Fish(graphics, content);
				fishes[cont].initialize();
				fishes[cont].loadContent();
				cont++;
			}

			foreach (Fish fish in fishes)
				if(fish != null)
					fish.update();
		}

		public void draw(SpriteBatch spriteBatch)
		{
			foreach (Fish fish in fishes)
				if(fish != null)
					fish.draw(spriteBatch);
		}
	}
}

