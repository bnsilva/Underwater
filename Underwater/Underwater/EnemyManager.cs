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
		FishBackground[] fishesBackground;
		Color color;
		int fishNumber, cont, depth;

		#endregion

		public EnemyManager(GraphicsDeviceManager graphics, ContentManager content, int depth, int fishNumber, Color color)
		{
			this.content = content;
			this.graphics = graphics;
			this.depth = depth;
			this.fishNumber = fishNumber;
			this.color = color;
		}

		public void initialize()
		{
			cont = 0;
			fishes = new Fish[fishNumber];
			fishesBackground = new FishBackground[fishNumber];
		}

		public void loadContent()
		{
			
		}

		public void update()
		{
			#region load fish lists
			if (cont < fishNumber)
			{
				fishes[cont] = new Fish(graphics, content, depth, color,depth, -1);
				fishes[cont].initialize();
				fishes[cont].loadContent();

				fishesBackground[cont] = new FishBackground(graphics, content);
				fishesBackground[cont].initialize();
				fishesBackground[cont].loadContent();

				cont++;
			}
			#endregion

			foreach (Fish fish in fishes)
				if(fish != null)
					fish.update();

			foreach (FishBackground fish in fishesBackground)
				if (fish != null)
					fish.update();
		}

		public void draw(SpriteBatch spriteBatch)
		{

			foreach (FishBackground fish in fishesBackground)
				if (fish != null)
					fish.draw(spriteBatch);
			
			foreach (Fish fish in fishes)
				if(fish != null)
					fish.draw(spriteBatch);
		}
	}
}

