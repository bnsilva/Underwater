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
		Texture2D texture, scannerTexture;
		Vector2 position;
		Rectangle sourceRectangle;
		float speedX, speedY, acceleration, rotation, scale, layerDepth;
		int spriteNumber, spriteTime, pressureResist, temperatureResist, light, level, contDamage, contHealing, life, contUpgrade;
		public bool isUpgrading;

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
			acceleration = 0.15f;
			rotation = 0f;
			scale = 1f;
			layerDepth = 1f;
			temperatureResist = 20;
			pressureResist = 1;
			light = 1;
			life = 50;
			contDamage = 0;
			contHealing = 0;
			contUpgrade = 0;
			isUpgrading = false;

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
			scannerTexture = content.Load<Texture2D>("scanner");
		}

		public void update()
		{

			#region player control

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

			#region moviment limit
			if (position.X > graphics.GraphicsDevice.Viewport.Width - 32)
				position.X = graphics.GraphicsDevice.Viewport.Width - 32;

			if (position.X < 0)
				position.X = 0;

			#endregion
		
			position.Y += speedY;
			position.X += speedX;
			speedX *= 0.97f;
			speedY *= 0.97f;
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

			if(Keyboard.GetState().IsKeyDown(Keys.Space))
				spriteBatch.Draw(
					scannerTexture,
					new Vector2(position.X, position.Y+10),
					null,
					Color.White,
					rotation,
					Vector2.Zero,
					scale,
					SpriteEffects.None,
					layerDepth
				);
		}

		public void resetPosition(float x, float y)
		{
			position.X = x;
			position.Y = y;
		}

		public void upLevel()
		{
			level++;
		}

		public void upTemperatureResist()
		{
			temperatureResist -= 5;
		}

		public void upPressureResist()
		{
			pressureResist ++;
		}

		public void upLight()
		{
			light++;
		}

		public void receiveDamage()
		{
			if (contDamage == 0)
			{
				if (life > 0)
				{
					life-=2;
					contDamage = 30;
				}
			}

			if (contDamage > 0)
				contDamage--;
		}

		public void receiveHealing()
		{
			if (contHealing == 0)
			{
				if (life < 50)
				{
					life+=2;
					contHealing = 15;
				}
			}

			if (contHealing > 0)
				contHealing--;
		}

		#region get Functions

		public int getUpgrade()
		{
			return contUpgrade;
		}

		public Vector3 getLevel()
		{
			return new Vector3(temperatureResist, pressureResist, light);
		}

		public int getLife()
		{
			return life;
		}

		public Vector2 getPosition()
		{
			return position;
		}

		public void getCollision(SpecialFish specialFish)
		{
			if (InputManager.KeyboardKeyPressed(Keys.Space))
			{
				if (position.X > specialFish.getPosition().X - 16 && position.X < specialFish.getPosition().X + 16)
				{
					if (position.Y > specialFish.getPosition().Y - 32 && position.Y < specialFish.getPosition().Y)
					{
						if (!specialFish.getStatus())
						{
							contUpgrade++;
							isUpgrading = true;

							if (contUpgrade == 200)
							{
								if (specialFish.getDepth() == 1)
								{
									upTemperatureResist();
									specialFish.scanning();
								}

								if (specialFish.getDepth() == 2)
								{
									upTemperatureResist();
									upPressureResist();
									specialFish.scanning();
								}

								if (specialFish.getDepth() == 3)
								{
									upTemperatureResist();
									upPressureResist();
									upLight();
									specialFish.scanning();
								}

								if (specialFish.getDepth() == 4)
								{
									upTemperatureResist();
									upPressureResist();
									upLight();
									specialFish.scanning();
								}
							}
						}
						else
						{
							contUpgrade = 0;
							isUpgrading = false;
						}
					}
					else 
					{
						contUpgrade = 0;
						isUpgrading = false;
					}
				}
				else
				{
					contUpgrade = 0;
					isUpgrading = false;
				}
			}
			else
			{
				contUpgrade = 0;
				isUpgrading = false;
			}
		}

		#endregion
	}
}

