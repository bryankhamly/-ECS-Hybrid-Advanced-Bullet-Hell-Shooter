using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For combat stuff, mainly dank bullets

public enum BulletOwner
{
	Player,
	Enemy
}

public class CustomCollision : MonoBehaviour
{
	public BulletOwner bulletOwner;
	public LayerMask layerMask;
	public Color colliderColor;
	public float radius;
	public Vector2 offset;

	void Update ()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll ((Vector2) transform.position + offset, radius, layerMask);

		if (colliders.Length > 0)
		{
			foreach (var item in colliders)
			{
				var damageInterface = item.GetComponent<IDamageable> ();

				if (bulletOwner == BulletOwner.Player)
				{
					var bullet = GetComponentInParent<PlayerBullet> ();
					var bulletStat = bullet.bulletStat;
					var bulletDamage = bulletStat.damage;
					var bulletType = bulletStat.bulletType;

					damageInterface.TakeDamage (bulletDamage);

					if (bulletType == BulletType.Normal)
					{
						bullet.ReturnToPool ();
					}
					else if (bulletType == BulletType.Piercing)
					{
						//Nothing, let it ReturnToPool once its out of the camera.
					}

				}
				else if (bulletOwner == BulletOwner.Enemy)
				{
					//Once I add in EnemyBullet
					var bullet = GetComponentInParent<TouhouBullet>();
					var bulletDamage = bullet.damage;
					//Do damage to player here
					damageInterface.TakeDamage (bulletDamage);
					bullet.ReturnToPool();
				}
			}
		}

	}

	private void OnDrawGizmos ()
	{
		Gizmos.color = colliderColor;
		Gizmos.DrawSphere ((Vector2) transform.position + offset, radius);
	}
}