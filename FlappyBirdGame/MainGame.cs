﻿using FlappyBirdGame.Entities;
using FlappyBirdGame.Player;
using FlappyBirdGame.UserInterface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FlappyBirdGame {
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class MainGame : Game {
		private readonly GraphicsDeviceManager graphics;

		public MainGame() {
			graphics = new GraphicsDeviceManager(this);			
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize() {
			graphics.PreferredBackBufferWidth = 288;
			graphics.PreferredBackBufferHeight = 512;
			graphics.ApplyChanges();
            Additional.Window.GetSize(GraphicsDevice);
			IsMouseVisible = true;

			Background.Initialize(this);
			Bird.Initialize(this);
			MovingEntityBuilder.Initialize(this);
			UiBuilder.Initialize(this);
           
            base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent() {

        }

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent() {
			
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime) {
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.Black);
		
		

			base.Draw(gameTime);
		}
	}
}
