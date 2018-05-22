using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Waypoint
{
	public string waypointName;
	public List<Transform> points;

	public Waypoint(string name, List<Transform> points, bool stopAtEnd, float stopXOffset)
	{
		this.waypointName = name;
		this.points = points;
	}
}