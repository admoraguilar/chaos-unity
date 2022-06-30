using UnityEngine;
using WaterToolkit.Weapons;

namespace ProjectCHAOS.Powerups
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Powerups/Explosive")]
	public class ExplosiveBulletPowerup : PowerupSpec
	{
		public override void Use()
		{
			WeaponHandler weaponHandler = references.Get<WeaponHandler>();
			weaponHandler.OnFire += OnWeaponFire;
		}

		public override void Revoke()
		{
			WeaponHandler weaponHandler = references.Get<WeaponHandler>();
			weaponHandler.OnFire -= OnWeaponFire;
		}

		public override PowerupSpec Clone()
		{
			ExplosiveBulletPowerup powerup = CreateInstance<ExplosiveBulletPowerup>();
			return FinishClone(powerup);
		}

		private void OnWeaponFire(WeaponFireInfo fireInfo)
		{
			Debug.Log("Explosive test");
			fireInfo.bullet.OnEndLifetime += OnEndLifetime;

			void OnEndLifetime(BulletLifeInfo bulletLifeInfo)
			{
				Collider[] colliders = Physics.OverlapSphere(bulletLifeInfo.hit.position, 20f);

				string result = string.Empty;
				foreach(Collider collider in colliders) {
					result += $"{collider.name}, ";

					//MeshRenderer meshRenderer = collider.GetComponent<MeshRenderer>();
					//meshRenderer.material.color = new Color(
					//	Random.Range(0, 1f),
					//	Random.Range(0, 1f),
					//	Random.Range(0, 1f));
				}

				Debug.Log($"Explode: {result}");

				fireInfo.bullet.OnEndLifetime -= OnEndLifetime;
			}
		}
	}
}
