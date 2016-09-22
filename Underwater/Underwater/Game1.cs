using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Underwater
{
	public class Game1 : Game
	{
		#region variables
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Player player;
		MapManager mapManager;
		InputManager inputManager;
		Song backgroundSound;
		Random random;
		List<SoundEffect> soundEffects;

		#endregion

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			player = new Player(graphics, Content);
			player.initialize();

			inputManager = new InputManager(this);
			Components.Add(inputManager);

			mapManager = new MapManager(graphics, Content, player);
			mapManager.initialize();

			soundEffects = new List<SoundEffect>();
			random = new Random();

			SoundEffect.MasterVolume = 0.1f;

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			mapManager.loadContent();
			player.loadContent();

			#region sound background
			backgroundSound = Content.Load<Song>("sound_background_01");
			MediaPlayer.Play(backgroundSound);
			MediaPlayer.IsRepeating = true;
			#endregion

			#region sound effects

			soundEffects.Add(Content.Load<SoundEffect>("whale01"));
			soundEffects.Add(Content.Load<SoundEffect>("whale02"));

			// Play that can be manipulated after the fact
			var instance = soundEffects[0].CreateInstance();
			instance.IsLooped = false;
			instance.Play();

			#endregion
		}

		protected override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			mapManager.update();

			#region sound effects random
			if (random.Next(2000) == 0)
				soundEffects[0].CreateInstance().Play();
			
			else if (random.Next(2000) == 1)
				soundEffects[1].CreateInstance().Play();

			#endregion

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			mapManager.draw(spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}

