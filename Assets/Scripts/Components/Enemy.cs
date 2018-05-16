using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PooledObject
{
	public Waypoint myWaypoint;
	public float speed;
	public Transform currentPoint;
	public int currentIndex;

	void Start ()
	{
		currentIndex = 1;
	}

	void OnDisable ()
	{
		currentIndex = 1;
	}
}