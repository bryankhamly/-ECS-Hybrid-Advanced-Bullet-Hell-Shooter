using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

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
				mytransform.localPosition += mytransform.up.normalized * bullet.speed * dt;
			}
		}
	}
}