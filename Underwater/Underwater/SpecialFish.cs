using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Underwater
{
	public class SpecialFish : Fish
	{
		#region variables

		int depth;
		bool scanned;

		#endregion

		public SpecialFish(GraphicsDeviceManager graphics, ContentManager content,int depth) : base(graphics, content, 3, Color.White,depth, 1)
		{
			this.depth = depth;
			scanned = false;
		}

		public Vector2 getPosition()
		{
			return position;
		}

		public void scanning()
		{
			scanned = true;
		}

		public bool getStatus()
		{
			return scanned;
		}

		public int getDepth()
		{
			return depth;
		}
	}
}
