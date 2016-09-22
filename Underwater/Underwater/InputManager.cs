using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Underwater
{
	public class InputManager: GameComponent
	{
		#region Variables
		private static KeyboardState keyboardState = Microsoft.Xna.Framework.Input.Keyboard.GetState();

		private static List<Keys> keysPressedLastFrame = new List<Keys>();

		private static MouseState mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
		#endregion

		public InputManager(Game game)
			: base(game)
		{
		}

		#region keyboard manager
		public static bool KeyboardKeyJustPressed(Keys key)
		{
			return keyboardState.IsKeyDown(key) &&
				keysPressedLastFrame.Contains(key) == false;
		}

		public static bool KeyboardKeyPressed(Keys key)
		{
			return keyboardState.IsKeyDown (key);
		}
		#endregion

		#region mouse manager
		public static Vector2 MousePosition()
		{
			return new Vector2 (mouseState.Position.X, mouseState.Position.Y);
		}
		#endregion

		#region update
		public override void Update(GameTime gameTime)
		{
			// Handle keyboard input
			keysPressedLastFrame = new List<Keys> (keyboardState.GetPressedKeys ());
			keyboardState = Microsoft.Xna.Framework.Input.Keyboard.GetState ();

			// Handle mouse input
			mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
		}
		#endregion
	}
}

