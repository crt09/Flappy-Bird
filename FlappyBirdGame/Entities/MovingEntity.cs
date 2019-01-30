using System;
using FlappyBirdGame.Interfaces;
using FlappyBirdGame.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBirdGame.Entities {
	public abstract class MovingEntity : DrawableGameComponent, IObserver {

		protected abstract Rectangle[] GetBoundingRectangle();

		protected SpriteBatch EntitySpriteBatch;
		protected Texture2D EntityTexture;
        protected Vector2 EntityPosition;
        protected SoundEffect HitSound;
        protected event Action<Bird> BirdTouchAction;

        public bool IsMoving { get; protected set; }
        public float EntitySpeed => 2;

        protected MovingEntity(Game game) : base(game) {
			Game.Components.Add(this);
			Bird.Current?.AddObserver(this);			
        }

        public void Destroy() {
	        Game.Components.Remove(this);
	        Bird.Current?.RemoveObserver(this);
			Dispose();
        }

		public override void Initialize() {
			EntitySpriteBatch = new SpriteBatch(GraphicsDevice);
			EntityPosition.X = GraphicsDevice.PresentationParameters.BackBufferWidth;
            base.Initialize();
		}

		protected override void LoadContent() {
			HitSound = Game.Content.Load<SoundEffect>("sounds/hit");
            base.LoadContent();
		}

		public override void Update(GameTime gameTime) {
			if(IsMoving) EntityPosition.X -= EntitySpeed;
			if (EntityPosition.X + EntityTexture.Width <= 0) {
				MovingEntityBuilder.Current.Remove(this);
			}
			base.Update(gameTime);
		}

		public void Notify(IObservable sender) {
			var bird = sender as Bird;
			foreach (var entityRectangle in GetBoundingRectangle()) {
				// kill bird if it collides with entity
				if (bird != null && bird.Enabled && entityRectangle.Intersects(bird.GetBoundingRectangle())) {					
					BirdTouchAction?.Invoke(bird);
                }               
			}    		
			// change moving state depending on the bird alive state
			if (bird != null) IsMoving = bird.Alive;
        }		
	}
}