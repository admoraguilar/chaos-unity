using UnityEngine;
using System.Collections.Generic;
using ProjectCHAOS.Systems.FlowTrees;

namespace ProjectCHAOS.Gameplay.GameLoops
{
	public class GameLoopListener : MonoBehaviour
	{
		[SerializeField]
		protected Node _node = null;

		[SerializeField]
		protected List<GameObject> _disabledObjectsOnVisitList = new List<GameObject>();

		[SerializeField]
		protected List<GameObject> _activeObjectsList = new List<GameObject>();

		[SerializeField]
		protected bool _shouldDisableActiveObjectsOnLeave = true;

		protected virtual void OnVisit() 
		{ 
			foreach(GameObject go in _disabledObjectsOnVisitList) { go.SetActive(false); }
			foreach(GameObject go in _activeObjectsList) { go.SetActive(true); }
		}

		protected virtual void OnLeave() 
		{
			if(_shouldDisableActiveObjectsOnLeave) {
				foreach(GameObject go in _activeObjectsList) { go.SetActive(false); }
			}
		}
		
		protected virtual void OnResume() { }
		protected virtual void OnSuspend() { }

		protected virtual void OnEnable()
		{
			_node.OnVisit += OnVisit;
			_node.OnLeave += OnLeave;
			_node.OnResume += OnResume;
			_node.OnSuspend += OnSuspend;
		}

		protected virtual void OnDisable()
		{
			_node.OnVisit -= OnVisit;
			_node.OnLeave -= OnLeave;
			_node.OnResume -= OnResume;
			_node.OnSuspend -= OnSuspend;
		}
	}
}
