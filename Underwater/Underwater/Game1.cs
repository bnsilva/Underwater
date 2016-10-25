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
		HUDManager HudManager;
		SpriteFont spriteFont;
		List<SoundEffect> soundEffects;
		int cont;

		#endregion

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.IsFullScreen = false;
			graphics.PreferredBackBufferWidth = 800;
			graphics.PreferredBackBufferHeight = 600;
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			player = new Player(graphics, Content);
			player.initialize();

			inputManager = new InputManager(this);
			Components.Add(inputManager);

			mapManager = new MapManager(this, graphics, Content, player);
			mapManager.initialize();

			spriteFont = Content.Load<SpriteFont>("TextFont");
			HudManager = new HUDManager(spriteBatch, GraphicsDevice, Content,  player, spriteFont, mapManager);
			HudManager.initialize();

			soundEffects = new List<SoundEffect>();
			random = new Random();

			SoundEffect.MasterVolume = 0.1f;
			cont = 0;

			base.Initialize();
		}

		protected override void LoadContent()
		{
			
			mapManager.loadContent();
			player.loadContent();
			HudManager.loadContent();

			#region sound background
			backgroundSound = Content.Load<Song>("sound_background_02");
			//MediaPlayer.Play(backgroundSound);
			MediaPlayer.IsRepeating = true;
			#endregion

			#region sound effects



			soundEffects.Add(Content.Load<SoundEffect>("whale01"));
			soundEffects.Add(Content.Load<SoundEffect>("whale02"));

			// Play that can be manipulated after the fact
			var instance = soundEffects[0].CreateInstance();
			instance.IsLooped = false;

			#endregion
		}

		protected override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			mapManager.update();
			HudManager.update();

			#region sound effects random

			if (cont == 0)
			{
				if (random.Next(2000) == 0)
				{
					soundEffects[0].CreateInstance().Play();
					cont = 300;
				}

				if (random.Next(2000) == 1)
				{
					soundEffects[1].CreateInstance().Play();
					cont = 300;
				}
			}
			else
				cont--;
			#endregion

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			mapManager.draw(spriteBatch);
			HudManager.draw(spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}

