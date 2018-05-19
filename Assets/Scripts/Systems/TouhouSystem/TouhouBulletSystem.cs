using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

//Inspired by Touhou and Bullet Heaven 2
//Bryan Khamly

public class TouhouBulletSystem : ComponentSystem
{
	private struct Data
	{
		public int Length;
		public EntityArray Entity;
		public GameObjectArray GameObject;

		public ComponentArray<Transform> transforms;
		public ComponentArray<TouhouBullet> touhouBullet;
	}

	[Inject] private Data data;

	protected override void OnUpdate ()
	{
		for (var i = 0; i < data.Length; i++)
		{
			var bullet = data.touhouBullet[i];
			var mytransform = data.transforms[i];
			var dt = Time.deltaTime;

			if (bullet.speed > 0)
			{
				float strafeAngle = bullet.strafe * dt;
				mytransform.eulerAngles = new Vector3 (mytransform.eulerAngles.x, mytransform.eulerAngles.y, mytransform.eulerAngles.z + strafeAngle);

				bullet.speed += bullet.acceleration * dt;
				mytransform.localPosition += mytransform.up.normalized * bullet.speed * dt;
			}
		}
	}
}