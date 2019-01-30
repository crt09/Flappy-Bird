namespace FlappyBirdGame.Player {
	public sealed class Physics {
		public Physics(Bird instance) {
			player = instance;
		}

		public Physics WithRotation() {
			enableAngleHandling = true;
			return this;
		}

		public void HandleUpdate() {
			HandlePosition();
			if (enableAngleHandling) HandleAngle();
		}

		public void ApplyImpulse() {
	        impulseAccelerator = 0;
	        rotationAccelerator = 0.07f;
	        impulseHandling = true;
	        rotationHandling = true;
        }

		private readonly Bird player;
		private bool enableAngleHandling;     
		
		private bool impulseHandling;
		private bool rotationHandling;

        private static float ImpulseSpeed => 10f;
        private float impulseAccelerator;
        private float rotationAccelerator = 0.07f;

        private void HandlePosition() {
	        if (player.PositionY < 0) player.PositionY = 0;

            if (impulseHandling) {
		        if (impulseAccelerator < 10f) {
			        player.PositionY -= ImpulseSpeed - impulseAccelerator;
			        impulseAccelerator += 1.0f;
		        } else {
			        impulseHandling = false;
                }
	        } else {
				player.PositionY += ImpulseSpeed - impulseAccelerator;
		        if (impulseAccelerator > 0)
			        impulseAccelerator -= 0.5f;
            }
        }

        private void HandleAngle() {
			if (rotationHandling) {
		        if (player.Angle > DegreesToRadians(-30)) {
			        player.Angle -= DegreesToRadians(15);
		        } else {
			        rotationHandling = false;
		        }
            } else {
		        if (player.Angle < DegreesToRadians(90))
			        player.Angle += DegreesToRadians(5) - rotationAccelerator;

		        if (rotationAccelerator > 0) rotationAccelerator -= 0.002f;

		        if (player.Angle > DegreesToRadians(90)) {
			        player.Angle = DegreesToRadians(90);
		        }
	        }
        } 

		public float DegreesToRadians(float angle) {
			return (float)(System.Math.PI / 180) * angle;
		}
	}
}