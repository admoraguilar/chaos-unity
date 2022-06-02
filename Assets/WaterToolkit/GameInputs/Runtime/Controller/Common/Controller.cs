using System;

namespace WaterToolkit.GameInputs
{
	public sealed class Controller<T> : Controller where T : class, IController
	{
		public event Action OnInitialize = delegate { };
		public event Action OnDeinitialize = delegate { };

		public T controller => _controller;
		private T _controller = null;

		public bool isReady => _isReady;
		private bool _isReady = false;

		public void Initialize(T controller)
		{
			_controller = controller;
			OnInitialize();
			_isReady = true;
		}

		public void Deinitialize()
		{
			OnDeinitialize();
			_controller = null;
			_isReady = false;
		}
	}

	public abstract class Controller { }
}
