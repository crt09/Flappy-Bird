using System.Collections.Generic;
using FlappyBirdGame.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
// ReSharper disable InvertIf

namespace FlappyBirdGame.Additional {
	public class Animation : GameComponent {

		private readonly Bird playerInstance;
		private readonly List<Texture2D> frames;
		private readonly int delayMilliseconds;

		private int defaultIndex;
		private int currentIndex;
		private int delayRoot;

		public Animation(Bird player, IEnumerable<string> framesAssets, int delayMilliseconds) : base(player.Game) {
			Game.Components.Add(this);

			playerInstance = player;
			frames = new List<Texture2D>();
			foreach (var asset in framesAssets) {
				frames.Add(Game.Content.Load<Texture2D>(asset));
			}
            this.delayMilliseconds = delayMilliseconds;
		}

		public Animation DefaultIndex(int index) {
			defaultIndex = index;
			return this;
		}

		private bool enabled;
		public new bool Enabled {
			get => enabled;
			set {
				if (!value) playerInstance.Texture = frames[defaultIndex];
				enabled = value;
			}
		}

		public override void Update(GameTime gameTime) {
			if (!DelayPassed(gameTime)) return;
			HandleFrame();

            base.Update(gameTime);
		}

		private void HandleFrame() {
			if (Enabled) {
				if (currentIndex >= frames.Count) {
					currentIndex = 0;
				}
				playerInstance.Texture = frames[currentIndex++];
			}
        }

		private bool DelayPassed(GameTime gameTime) {
			delayRoot += gameTime.ElapsedGameTime.Milliseconds;
			if (delayRoot >= delayMilliseconds) {
				delayRoot = 0;
				return true;
			}
			return false;
		}
	}
}