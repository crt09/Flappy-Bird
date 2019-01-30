using FlappyBirdGame.Additional;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
// ReSharper disable InvertIf

namespace FlappyBirdGame.Player {
	public sealed class Input {

		private Game game;
		public Input(Game game) {
			this.game = game;
		}

		private KeyboardState previousKeyboardState;
        private MouseState previousMouseState;
		private KeyboardState currentKeyboardState;
        private MouseState currentMouseState;

        public bool MainKeyPressed() {
	        if (!game.IsActive) return false;

	        previousKeyboardState = currentKeyboardState;
	        previousMouseState = currentMouseState;
	        currentKeyboardState = Keyboard.GetState();
	        currentMouseState = Mouse.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up)) {
		        return true;
	        }
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released) {
				var windowRect = new Rectangle(0, 0, (int)Window.Size.X, (int)Window.Size.Y);
				if(windowRect.Contains(new Point(currentMouseState.X, currentMouseState.Y)))
					return true;
            }           
            return false;
        }
	}
}