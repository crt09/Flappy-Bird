using System.Collections.Generic;
using FlappyBirdGame.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBirdGame.UserInterface {
	public class ScoreParser : GameComponent {

		private readonly List<Texture2D> numbersTextures;
		private readonly List<Texture2D> destinationTextures;

		public ScoreParser(Game game) : base(game) {
			numbersTextures = new List<Texture2D>();
			destinationTextures = new List<Texture2D>();
			for (int i = 0; i < 10; i++) {
				numbersTextures.Add(Game.Content.Load<Texture2D>($"ui/numbers/{i}"));
			}
			Refresh(0);
		}

		public void Refresh(uint value) {
			if (value == uint.MaxValue) {
				Bird.Current?.Kill();
			}

			destinationTextures.Clear();
			if(value == 0) {
				destinationTextures.Add(numbersTextures[0]);
				return;
			}

			for (; value != 0; value /= 10) {
				destinationTextures.Add(numbersTextures[(int)(value % 10)]);
			}
			destinationTextures.Reverse();
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 offset) {			
			for (int i = 0; i < destinationTextures.Count; i++) {
				int widthSum = 0;
				for (int j = 0; j < i; j++) {
					widthSum += destinationTextures[j].Width;
				}
				spriteBatch.Draw(destinationTextures[i], new Vector2(offset.X + widthSum, offset.Y), Color.White);
            }
		}

		public int GetOverallWidth() {
			int sum = 0;
			foreach (var texture in destinationTextures) {
				sum += texture.Width;
			}
			return sum;
		}
	}
}