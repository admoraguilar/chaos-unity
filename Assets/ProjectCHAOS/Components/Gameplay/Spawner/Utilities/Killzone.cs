using UnityEngine;
using ProjectCHAOS.Systems;
using ProjectCHAOS.Gameplay.Characters.Players;

namespace ProjectCHAOS.Gameplay.Spawners
{
	public class Killzone : MonoBehaviour
	{
		public LayerMask layerMask;

		private void OnTriggerEnter(Collider other)
		{
			GameObject go = other.gameObject;
			if(layerMask.Includes(go.layer)) {
				PlayerCharacter playerCharacter = go.GetComponentInParent<PlayerCharacter>();
				if(playerCharacter != null) {
					playerCharacter.health.Kill();
				} else {
					Destroy(go);
				}	
			}
		}
	}
}
