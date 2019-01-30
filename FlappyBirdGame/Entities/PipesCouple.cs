using System;
using System.Threading.Tasks;
using FlappyBirdGame.Additional;
using FlappyBirdGame.Player;
using FlappyBirdGame.UserInterface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBirdGame.Entities {
	public sealed class PipesCouple : MovingEntity {	

		private float positionYTop;
		private float positionYBottom;
		private int yPoint;
		private int CoupleSpan { get; } = 100;

		private XTrigger pipeTrigger;
		private XTrigger scoreTrigger;

		private SoundEffect scoreSound;		
		private SoundEffect dieSound;

		public PipesCouple(Game game) : base(game) { }

		public override void Initialize() {
			DrawOrder = (int)Drawer.Layer.Pipes;
			pipeTrigger = new XTrigger();
			scoreTrigger = new XTrigger();
			
			EntityTexture = Game.Content.Load<Texture2D>("entities/pipe");
            yPoint = new Random().Next(110, 290);
			positionYTop = yPoint - EntityTexture.Height - (CoupleSpan >> 1);
			positionYBottom = yPoint + (CoupleSpan >> 1);	

            base.Initialize();
			BirdTouchAction += OnBirdTouchAction;
		}

		protected override void LoadContent() {
			scoreSound = Game.Content.Load<SoundEffect>("sounds/point");			
			dieSound = Game.Content.Load<SoundEffect>("sounds/die");

            base.LoadContent();
		}

		public override void Update(GameTime gameTime) {
	        base.Update(gameTime);

	        if (pipeTrigger.DoneWith(EntityPosition.X, GraphicsDevice.PresentationParameters.BackBufferWidth >> 1)) {
				MovingEntityBuilder.Current.Create(new PipesCouple(Game));
	        }

			if (Bird.Current != null && scoreTrigger.DoneWith(EntityPosition.X + (float)EntityTexture.Width / 2, Bird.Current.PositionX)) {
				if(UiBuilder.Current != null) UiBuilder.Current.Score++;
				scoreSound.Play(0.5f, 0, 0);
			}
		}

        public override void Draw(GameTime gameTime) {
			EntitySpriteBatch.Begin();
			EntitySpriteBatch.Draw(
				EntityTexture, GetBoundingRectangle()[0], null, Color.White,
				0f, Vector2.Zero, SpriteEffects.FlipVertically, 1f);
			EntitySpriteBatch.Draw(
				EntityTexture, GetBoundingRectangle()[1], null, Color.White,
				0f, Vector2.Zero, SpriteEffects.None, 1f);
            EntitySpriteBatch.End();
            base.Draw(gameTime);
        }

        protected override Rectangle[] GetBoundingRectangle() {
			return new[] {
				new Rectangle((int)EntityPosition.X, (int)positionYTop, EntityTexture.Width, EntityTexture.Height),
				new Rectangle((int)EntityPosition.X, (int)positionYBottom, EntityTexture.Width, EntityTexture.Height)
			};
        }

		private void OnBirdTouchAction(Bird bird) {
			if (bird.Alive) {
				bird.Kill();
				HitSound.Play(0.5f, 0, 0);
                Task.Delay(200).ContinueWith((e) => { dieSound.Play(0.5f, 0, 0); });				
            }		
		}
	}
}