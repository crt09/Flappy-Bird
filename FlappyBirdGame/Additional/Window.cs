using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBirdGame.Additional {
	public static class Window {

		private static Vector2 size;
		public static Vector2 Size => size;

		public static void GetSize(GraphicsDevice graphics) {
			size.X = graphics.PresentationParameters.BackBufferWidth;
			size.Y = graphics.PresentationParameters.BackBufferHeight;
		}
	}
}