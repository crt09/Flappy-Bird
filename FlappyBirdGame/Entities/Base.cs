using FlappyBirdGame.Additional;
using FlappyBirdGame.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBirdGame.Entities {
	public sealed class Base : MovingEntity {

		private XTrigger baseTrigger;
		private bool AtStart { get; }

		public Base(Game game) : base(game) { }
		public Base(Game game, bool atStart) : base(game) {
			AtStart = atStart;
		}

		public override void Initialize() {
            DrawOrder = (int)Drawer.Layer.Base;
			baseTrigger = new XTrigger();

            EntityTexture = Game.Content.Load<Texture2D>("entities/base");
			EntityPosition.Y = GraphicsDevice.PresentationParameters.BackBufferHeight - EntityTexture.Height;

            base.Initialize();
            if (AtStart) EntityPosition.X = 0;
            BirdTouchAction += OnBirdTouchAction;
		}		

		public override void Update(GameTime gameTime) {
			if (baseTrigger.DoneWith(EntityPosition.X, 0)) {
				MovingEntityBuilder.Current.Create(new Base(Game));
			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			EntitySpriteBatch.Begin();
			EntitySpriteBatch.Draw(EntityTexture, EntityPosition, Color.White);
			EntitySpriteBatch.End();

			base.Draw(gameTime);
		}

		protected override Rectangle[] GetBoundingRectangle() {
			return new[] {
				new Rectangle((int)EntityPosition.X, (int)EntityPosition.Y, EntityTexture.Width, EntityTexture.Height) 
			};
		}

		private void OnBirdTouchAction(Bird bird) {
			if (bird.Alive) {
				HitSound.Play(0.5f, 0, 0);
                bird.Kill();
			}
			bird.Disable();			
        }
	}
}