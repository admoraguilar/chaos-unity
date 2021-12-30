
namespace ProjectCHAOS.Inputs
{
	public sealed class Controller<T> : Controller where T : class, IController
	{
		public T controller => _controller;
		private T _controller = null;

		public bool isReady => _isReady;
		private bool _isReady = false;

		public void Initialize(T controller)
		{
			_controller = controller;
			_isReady = true;
		}

		public void Deinitialize()
		{
			_controller = null;
			_isReady = false;
		}
	}

	public abstract class Controller { }
}
