using FlappyBirdGame.Additional;
using FlappyBirdGame.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBirdGame.UserInterface {
	public class UiBuilder : DrawableGameComponent {

		// Singleton

		public static UiBuilder Current { get; private set; }
		public static UiBuilder Initialize(Game game) {
			return Current ?? (Current = new UiBuilder(game));
        }

		// Ctor

		private UiBuilder(Game game) : base(game) {
			Game.Components.Add(this);
		}

		// Fields and properties

		private uint score;
        public uint Score {
			get => score;
			set {
				scoreParser.Refresh(value);
				score = value;
			}
		}

		private ScoreParser scoreParser;
		private SpriteBatch uiSpriteBatch;
		private Texture2D gameOverTexture;
		private Texture2D messageTexture;

		// XNA implementation

		public override void Initialize() {
			DrawOrder = (int)Drawer.Layer.UserInterface;
			scoreParser = new ScoreParser(Game);
			uiSpriteBatch = new SpriteBatch(GraphicsDevice);

			base.Initialize();
		}

		protected override void LoadContent() {
			gameOverTexture = Game.Content.Load<Texture2D>("ui/gameover");
			messageTexture = Game.Content.Load<Texture2D>("ui/message");

			base.LoadContent();
		}

		public override void Draw(GameTime gameTime) {
			uiSpriteBatch.Begin();

			var gameOverPosition
				= new Vector2(Window.Size.X / 2 - (float)gameOverTexture.Width / 2, Window.Size.Y / 2 - (float)gameOverTexture.Height / 2 - 40);
			var messagePosition
				= new Vector2(Window.Size.X / 2 - (float)messageTexture.Width / 2, Window.Size.Y / 2 - (float)messageTexture.Height / 2 - 50);

			if(showGameOver) uiSpriteBatch.Draw(gameOverTexture, gameOverPosition, Color.White);
			if(showMessage) uiSpriteBatch.Draw(messageTexture, messagePosition, Color.White);

			if (Bird.Current != null && !Bird.Current.Waiting) {
				scoreParser.Draw(
					uiSpriteBatch,
					new Vector2(Window.Size.X / 2 - (float)scoreParser.GetOverallWidth() / 2, 30));
            }			

			uiSpriteBatch.End();

			base.Draw(gameTime);
		}

		private bool showGameOver;
		private bool showMessage = true;

		// Public working interface

		public void ShowGameOver() => showGameOver = true;
		public void ShowMessage() => showMessage = true;
		public void HideGameOver() => showGameOver = false;
		public void HideMessage() => showMessage = false;
    }
}