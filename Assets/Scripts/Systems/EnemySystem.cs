using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

//This System had so many bugs !!, punishment for being lazy not caching some vars

public class EnemySystem : ComponentSystem
{
	private struct Data
	{
		public int Length;
		public EntityArray Entity;
		public GameObjectArray GameObject;

		public ComponentArray<Enemy> enemy;
	}

	[Inject] private Data data;

	protected override void OnUpdate ()
	{
		for (var i = 0; i < data.Length; i++)
		{
			if (data.enemy[i])
			{
				if (data.enemy[i].myWaypoint.waypointName != "")
				{
					data.enemy[i].currentPoint = data.enemy[i].myWaypoint.points[data.enemy[i].currentIndex];
					var diff = data.GameObject[i].transform.position - data.enemy[i].currentPoint.position;
					diff.Normalize ();

					if (Vector2.Distance (data.GameObject[i].transform.position, data.enemy[i].currentPoint.position) > 0.1f)
					{
						data.GameObject[i].transform.Translate (diff.x * data.enemy[i].speed * Time.deltaTime, diff.y * data.enemy[i].speed * Time.deltaTime, 0, Space.Self);
					}
					else
					{
						if (data.enemy[i].currentIndex + 1 >= data.enemy[i].myWaypoint.points.Count)
						{
							Enemy e = data.enemy[i];
							e.ReturnToPool ();
						}
						else
						{
							data.enemy[i].currentIndex += 1;
							data.enemy[i].currentPoint = data.enemy[i].myWaypoint.points[data.enemy[i].currentIndex];
						}
					}
				}
			}

		}
	}
}