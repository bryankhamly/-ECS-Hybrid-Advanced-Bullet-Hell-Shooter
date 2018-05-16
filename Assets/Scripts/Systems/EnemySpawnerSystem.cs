using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawnerSystem : ComponentSystem
{
	private struct Data
	{
		public int Length;
		public EntityArray Entity;
		public GameObjectArray GameObject;

		public ComponentArray<WaypointManager> wayPointManager;
		public ComponentArray<EnemySpawner> enemySpawner;
	}

	[Inject] private Data data;

	protected override void OnUpdate ()
	{
		for (var i = 0; i < data.Length; i++)
		{
			var wManager = data.wayPointManager[i];
			var eSpawner = data.enemySpawner[i];
			eSpawner.timer += Time.deltaTime;

			if (eSpawner.timer >= eSpawner.timerPerSpawn)
			{
				//Get random spawnpoint
				int randomNumber = (int) Random.Range (0, wManager.wayPoints.Count - 1);
				var enemy = eSpawner.enemyPrefab.GetPooledInstance<PooledObject> ();
				var e = enemy.GetComponent<Enemy> ();
				enemy.transform.position = wManager.wayPoints[randomNumber].points[0].position;
				e.myWaypoint = wManager.wayPoints[randomNumber];
				//Spawn
				eSpawner.timer = 0;
			}
		}
	}
}