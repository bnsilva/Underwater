using Microsoft.Xna.Framework.Graphics;

namespace Underwater
{
	public interface IGameObject
	{
		void initialize();

		void loadContent();

		void update();

		void draw(SpriteBatch spriteBatch);
	}
}
