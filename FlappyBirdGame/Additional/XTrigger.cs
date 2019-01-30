namespace FlappyBirdGame.Additional {
	public struct XTrigger {
		private bool Activated { get; set; }
		public bool DoneWith(float positionX, float destinationPositionX) {
			if (Activated) return false;
			return (Activated = positionX <= destinationPositionX);
		}
	}
}