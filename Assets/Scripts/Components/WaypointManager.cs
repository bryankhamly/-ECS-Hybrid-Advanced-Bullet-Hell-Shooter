using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
	public List<Waypoint> wayPoints;
	public bool drawWaypoints;

	void OnDrawGizmos ()
	{
		if (!drawWaypoints)
			return;

		if (wayPoints.Count <= 0)
			return;

		for (int i = 0; i < wayPoints.Count; i++)
		{
			for (int j = 0; j < wayPoints[i].points.Count - 1; j++)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawSphere (wayPoints[i].points[j].position, 0.25f); //Start Point
				Gizmos.DrawLine (wayPoints[i].points[j].position, wayPoints[i].points[j + 1].position); //Draw the Line
				Gizmos.DrawSphere (wayPoints[i].points[j + 1].position, 0.25f); //End Point
			}
		}
	}
}