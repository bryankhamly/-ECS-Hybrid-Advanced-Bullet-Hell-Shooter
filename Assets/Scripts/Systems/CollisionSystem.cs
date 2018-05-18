using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

//Object pooling with ECS is quite buggy atm

public class CollisionSystem : ComponentSystem
{
	private struct Data
	{
		public int Length;
		public EntityArray Entity;
		public GameObjectArray GameObject;

		public ComponentArray<CustomCollision> customCollision;
	}

	[Inject] private Data data;

	protected override void OnUpdate ()
	{
		for (var i = 0; i < data.Length; i++)
		{		
			/* 
			Collider2D[] colliders = Physics2D.OverlapCircleAll ((Vector2) data.GameObject[i].transform.position + data.customCollision[i].offset, data.customCollision[i].radius, data.customCollision[i].layerMask);

			if (colliders.Length > 0)
			{
				foreach (var item in colliders)
				{
					//Gunna need an interface to make this more modular [x]
					var damageInterface = item.GetComponent<IDamageable> ();
					if (data.customCollision[i].bulletOwner == BulletOwner.Player)
					{
						var bullet = data.GameObject[i].GetComponent<PlayerBullet> ();
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
					else if (data.customCollision[i].bulletOwner == BulletOwner.Enemy)
					{
						//Once I add in EnemyBullet
					}
				}
			}
			*/
		}
		
	}
}