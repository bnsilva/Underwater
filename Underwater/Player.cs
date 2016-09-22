using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Underwater
{
	public class Player : IGameObject
	{
		
		#region variables

		GraphicsDeviceManager graphics;
		ContentManager content;
		SpriteManager spriteManager;
		Texture2D texture;
		Vector2 position;
		Rectangle sourceRectangle;
		float speedX, speedY, acceleration, rotation, scale, layerDepth;
		int spriteNumber, spriteTime, pressureResist, temperatureResist, light, level;

		#endregion

		public Player(GraphicsDeviceManager graphics, ContentManager content)
		{
			this.graphics = graphics;
			this.content = content;
		}

		public void initialize()
		{
			#region initializing fixed variables
			level = 0;
			spriteTime = 15;
			spriteNumber = 2;
			speedX = 0f;
			speedY = 0f;
			acceleration = 0.03f;
			rotation = 0f;
			scale = 1f;
			layerDepth = 1f;
			pressureResist = 10 * level;
			temperatureResist = 10 * level;
			light = 1 * level;

			#endregion

			#region initializing objects
			sourceRectangle = new Rectangle(0, 74, 32, 11);
			spriteManager = new SpriteManager();
			#endregion

			spriteManager.initialize();
		}

		public void loadContent()
		{
			texture = content.Load<Texture2D>("overheadfishcrdusty");
		}

		public void update()
		{

			#region player control

			if (InputManager.KeyboardKeyPressed(Keys.Space))
				upLevel();

			if (InputManager.KeyboardKeyPressed(Keys.Up))
				speedY -= acceleration;
			
			if (InputManager.KeyboardKeyPressed(Keys.Down))
				speedY += acceleration;


			if (InputManager.KeyboardKeyPressed(Keys.Right))
			{
				speedX += acceleration;
				sourceRectangle.Y = 74;
			}

			if (InputManager.KeyboardKeyPressed(Keys.Left))
			{
				speedX -= acceleration;
				sourceRectangle.Y = 44;
			}
			#endregion

			position.Y += speedY;
			position.X += speedX;
			speedX *= 0.98f;
			speedY *= 0.98f;
		}

		public void draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				texture,
				position, 
				spriteManager.animationSprite(sourceRectangle, spriteNumber, spriteTime),
				Color.White,
				rotation,
				Vector2.Zero,
				scale,
				SpriteEffects.None,
				layerDepth
			); 
		}

		public Vector2 getPosition()
		{
			return position;
		}

		public void resetPosition(float x, float y)
		{
			position.X = x;
			position.Y = y;
		}

		public void upLevel()
		{
			level++;
			pressureResist = 10 * level;
			temperatureResist = 10 * level;
			light = 1 * level;
		}

		public int getLevel()
		{
			return level;
		}

	}
}

