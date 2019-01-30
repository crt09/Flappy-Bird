using System;
using FlappyBirdGame.Additional;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBirdGame.UserInterface {
	public class Background : DrawableGameComponent {

		// Singleton

		private static Background instance;
		public static Background Initialize(Game game) {
			return instance ?? (instance = new Background(game));
		}

		// Fields

		private SpriteBatch backgroundSpriteBatch;
		private Texture2D backgroundTexture;

		// Ctor and override implementation

        public Background(Game game) : base(game) {
			Game.Components.Add(this);
		}

        public override void Initialize() {
			DrawOrder = (int)Drawer.Layer.Background;
			backgroundSpriteBatch = new SpriteBatch(GraphicsDevice);
	        base.Initialize();
        }

        protected override void LoadContent() {
	        var hours = DateTime.Now.Hour;
			if(hours >= 6 && hours < 22)
				backgroundTexture = Game.Content.Load<Texture2D>("background-day");
			else 
				backgroundTexture = Game.Content.Load<Texture2D>("background-night");

	        base.LoadContent();
        }

        public override void Draw(GameTime gameTime) {
	        backgroundSpriteBatch.Begin();
			backgroundSpriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
			backgroundSpriteBatch.End();
	        base.Draw(gameTime);
        }
	}
}