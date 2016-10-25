using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Underwater
{
	public class BarComponent
	{
		#region variables
		SpriteBatch spriteBatch;
		GraphicsDevice graphicsDevice;

		Vector2 position;
		Vector2 dimension;

		float valueMax;
		float valueCurrent;

		int type;

		bool enabled;

		#endregion

		/// <summary>
		/// Creates a new Bar Component for the HUD.
		/// </summary>
		/// <param name="position">Component position on the screen.</param>
		/// <param name="dimension">Component dimensions.</param>
		/// <param name="valueMax">Maximum value to be displayed.</param>
		/// <param name="spriteBatch">SpriteBatch that is required to draw the sprite.</param>
		/// <param name="graphicsDevice">Graphicsdevice that is required to create the semi transparent background texture.</param>
		public BarComponent(Vector2 position, Vector2 dimension, float valueMax,int type, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
		{
			this.position = position;
			this.dimension = dimension;
			this.valueMax = valueMax;
			this.spriteBatch = spriteBatch;
			this.graphicsDevice = graphicsDevice;
			this.type = type;
			enabled = true;
		}

		/// <summary>
		/// Sets whether the component should be drawn.
		/// </summary>
		/// <param name="enabled">enable the component</param>
		public void enable(bool enabled)
		{
			this.enabled = enabled;
		}

		/// <summary>
		/// Updates the text that is displayed after ":".
		/// </summary>
		/// <param name="valueCurrent">Text to be displayed.</param>
		public void update(float valueCurrent)
		{
			this.valueCurrent = valueCurrent;
		}

		/// <summary>
		/// Draws the BarComponent with the values set before.
		/// </summary>
		public void draw()
		{
			if (enabled)
			{
				float percent = valueCurrent / valueMax;

				Color backgroundColor = new Color(0, 0, 0, 128);
				Color barColor = new Color(0, 255, 0, 200);

				if (type == 0)
				{
					barColor = new Color(0, 255, 0, 200);
					if (percent < 0.60)
						barColor = new Color(255, 255, 0, 200);
					if (percent < 0.30)
						barColor = new Color(255, 0, 0, 200);
				}

				if (type == 1)
				{
					barColor = new Color(255, 0, 0, 255);
					if (percent <= 0.78)
					{
						barColor = new Color(255, 160, 40, 255);
					}
					if (percent <= 0.53)
					{
						barColor = new Color(40, 200, 255, 255);
					}
					if (percent <= 0.28)
					{
						barColor = new Color(55, 0, 255, 255);
					}
				}

				Rectangle backgroundRectangle = new Rectangle();
				backgroundRectangle.Width = (int)dimension.X;
				backgroundRectangle.Height = (int)dimension.Y;
				backgroundRectangle.X = (int)position.X;
				backgroundRectangle.Y = (int)position.Y;

				Texture2D dummyTexture = new Texture2D(graphicsDevice, 1,1);
				dummyTexture.SetData(new Color[] { backgroundColor });

				if(type!=1)
					spriteBatch.Draw(dummyTexture, backgroundRectangle, backgroundColor);

				backgroundRectangle.Width = (int)(dimension.X * 0.9);
				backgroundRectangle.Height = (int)(dimension.Y * 0.5);
				backgroundRectangle.X = (int)position.X + (int)(dimension.X * 0.05);
				backgroundRectangle.Y = (int)position.Y + (int)(dimension.Y * 0.25);

				spriteBatch.Draw(dummyTexture, backgroundRectangle, backgroundColor);

				backgroundRectangle.Width = (int)(dimension.X * 0.9 * percent);
				backgroundRectangle.Height = (int)(dimension.Y * 0.5);
				backgroundRectangle.X = (int)position.X + (int)(dimension.X * 0.05);
				backgroundRectangle.Y = (int)position.Y + (int)(dimension.Y * 0.25);

				dummyTexture = new Texture2D(graphicsDevice, 1, 1);
				dummyTexture.SetData(new Color[] { barColor });

				spriteBatch.Draw(dummyTexture, backgroundRectangle, barColor);
			}
		}

		public void updatePosition(Vector2 newPosition)
		{
			this.position = newPosition;
		}
	}
}
