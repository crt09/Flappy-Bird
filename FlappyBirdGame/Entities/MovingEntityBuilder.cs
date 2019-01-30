using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace FlappyBirdGame.Entities {
	public sealed class MovingEntityBuilder : GameComponent {

		// Singleton

		public static MovingEntityBuilder Current { get; private set; }
		public static MovingEntityBuilder Initialize(Game game) {
			return Current ?? (Current = new MovingEntityBuilder(game));
		}

		// Implementation

		private readonly List<MovingEntity> entities;

		private MovingEntityBuilder(Game game) : base(game) {
			entities = new List<MovingEntity>();

			Create(new PipesCouple(Game));
            Create(new Base(Game, true));
		}

		public void Create(MovingEntity entity) {
			entities.Add(entity);
		}

		public void Remove(MovingEntity entity) {
			entity.Destroy();
			entities.Remove(entity);
		}

		public void RemoveAllByType(Type type) {
            foreach (var entity in entities.Where(e => e.GetType() == type)) {
				entity.Destroy();
			}
            entities.RemoveAll(e => e.GetType() == type);
		}
	}
}