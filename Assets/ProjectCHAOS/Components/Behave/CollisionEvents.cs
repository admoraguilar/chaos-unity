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
		public event Action<Collider> OnTriggerEnterResponse = delegate { };
		public event Action<Collider> OnTriggerStayResponse = delegate { };
		public event Action<Collider> OnTriggerExitResponse = delegate { };

		public void Clear()
		{
			OnEnableResponse = delegate { };
			OnDisableResponse = delegate { };
			OnCollisionEnterResponse = delegate { };
			OnCollisionStayResponse= delegate { };
			OnCollisionExitResponse= delegate { };
			OnTriggerEnterResponse = delegate { };
			OnTriggerStayResponse = delegate { };
			OnTriggerExitResponse = delegate { };
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

		private void OnTriggerEnter(Collider other)
		{
			OnTriggerEnterResponse(other);
		}

		private void OnTriggerStay(Collider other)
		{
			OnTriggerStayResponse(other);
		}

		private void OnTriggerExit(Collider other)
		{
			OnTriggerExitResponse(other);
		}
	}
}
