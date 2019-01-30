namespace FlappyBirdGame.Interfaces {
	public interface IObservable {
		void AddObserver(IObserver observer);
		void RemoveObserver(IObserver observer);
		void NotifyAllObservers();
	}
}