using System;
using UnityEngine;

namespace ProjectCHAOS.Behave
{
	public class CollisionEvents : MonoBehaviour
	{
		public event Action OnEnableResponse = delegate { };
		public event Action OnDisableResponse = delegate { };

		public event Action<Collision> OnCollisionEnterResponse = delegate { };
		public event Action<Collision> OnCollisionStayResponse = delegate { };
		public event Action<Collision> OnCollisionExitResponse = delegate { };

		public void Clear()
		{
			OnEnableResponse = delegate { };
			OnDisableResponse = delegate { };
			OnCollisionEnterResponse = delegate { };
			OnCollisionStayResponse= delegate { };
			OnCollisionExitResponse= delegate { };
		}

		private void OnEnable()
		{
			OnEnableResponse();
		}

		private void OnDisable()
		{
			OnDisableResponse();
		}

		private void OnCollisionEnter(Collision collision)
		{
			OnCollisionEnterResponse(collision);
		}

		private void OnCollisionStay(Collision collision)
		{
			OnCollisionStayResponse(collision);
		}

		private void OnCollisionExit(Collision collision)
		{
			OnCollisionExitResponse(collision);
		}
	}
}
