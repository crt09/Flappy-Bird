using System.Collections.Generic;
using FlappyBirdGame.Additional;
using FlappyBirdGame.Entities;
using FlappyBirdGame.Interfaces;
using FlappyBirdGame.UserInterface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBirdGame.Player {
	public sealed class Bird : DrawableGameComponent, IObservable {

		// Singleton

		public static Bird Current { get; private set; }
		public static Bird Initialize(Game game) {
			return Current ?? (Current = new Bird(game));
        }

		// Observer

		private readonly List<IObserver> observers = new List<IObserver>();

		public void AddObserver(IObserver observer) {
			observers.Add(observer);
		}

		public void RemoveObserver(IObserver observer) {
			observers.Remove(observer);
		}

		public void NotifyAllObservers() {
			foreach (var observer in observers) {
				observer.Notify(this);
			}
		}		

		// Fields and properties

		private SpriteBatch birdSpriteBatch;
		private Texture2D birdTexture;
		private Vector2 birdPosition = Vector2.Zero;
		private Vector2 defaultBirdPosition = Vector2.Zero;

		private Physics birdPhysicsEngine;
		private Input birdInputManager;
		private Animation birdAnimation;

		private SoundEffect wingSound;

		// Public working interface

		public bool Alive { get; set; }
		public new bool Enabled { get; set; }
		public bool Waiting { get; set; } = true;

		public float PositionX {
			get => birdPosition.X;
            set => birdPosition.X = value;
		}

		public float PositionY {
			get => birdPosition.Y;
			set => birdPosition.Y = value;
		}

		public Texture2D Texture { set => birdTexture = value; }
		public float Angle { get; set; }

		// Ctor and XNA override implementation

        private Bird(Game game) : base(game) {
			Game.Components.Add(this);			
		}

		public override void Initialize() {
			DrawOrder = (int)Drawer.Layer.Bird;
			birdSpriteBatch = new SpriteBatch(GraphicsDevice);

			birdPhysicsEngine = new Physics(this).WithRotation();
			birdInputManager = new Input(Game);			

            defaultBirdPosition.X = 80f;
			defaultBirdPosition.Y = Window.Size.Y / 2;
			birdPosition = defaultBirdPosition;
			base.Initialize();
		}

		protected override void LoadContent() {
			birdTexture = Game.Content.Load<Texture2D>("bird/yellowbird-midflap");
			var animationFrames = new[] {
				"bird/yellowbird-downflap", "bird/yellowbird-midflap", "bird/yellowbird-upflap", "bird/yellowbird-midflap"
			};
			birdAnimation = new Animation(this, animationFrames, 50).DefaultIndex(1);

			wingSound = Game.Content.Load<SoundEffect>("sounds/wing");

            base.LoadContent();
		}

		public override void Update(GameTime gameTime) {	        
			HandleControl();
			if (Enabled) {
				birdPhysicsEngine.HandleUpdate();
            }
			birdAnimation.Enabled = Enabled | Waiting;
            NotifyAllObservers();
            base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			birdSpriteBatch.Begin();
			birdSpriteBatch.Draw(birdTexture, new Rectangle((int)birdPosition.X, (int)birdPosition.Y, birdTexture.Width, birdTexture.Height), null, Color.White,
				Angle, new Vector2(birdTexture.Width >> 1, birdTexture.Height >> 1), SpriteEffects.None, 1f);
			birdSpriteBatch.End();
			base.Draw(gameTime);
		}

        // Other methods      

        public Rectangle GetBoundingRectangle() =>
	        new Rectangle((int)birdPosition.X - birdTexture.Width / 2,(int)birdPosition.Y - birdTexture.Height / 2, birdTexture.Width, birdTexture.Height);       

        private void HandleControl() {
			if (birdInputManager.MainKeyPressed()) {
				// Start game
				if (Waiting) { Waiting = false; Enabled = true; Alive = true; }
				// Play
				if (Alive) {
					birdPhysicsEngine.ApplyImpulse();
					wingSound.Play(0.5f, 0, 0);
					UiBuilder.Current?.HideMessage();
                }
				// Restart
                if (!Enabled) {
					UiBuilder.Current?.HideGameOver();
					UiBuilder.Current?.ShowMessage();
					Restart();
				}
            }	
        }        

        public void Kill() {
			// Game over
	        UiBuilder.Current?.ShowGameOver();
            Alive = false;
        }
        public void Disable() => Enabled = false;

        public void Restart() {
	        Waiting = true;
			MovingEntityBuilder.Current?.RemoveAllByType(typeof(PipesCouple));
			MovingEntityBuilder.Current?.Create(new PipesCouple(Game));
			if(UiBuilder.Current != null) UiBuilder.Current.Score = 0;
			birdPosition = defaultBirdPosition;
			Angle = 0f;
        }
    }
}