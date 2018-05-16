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
		var enemyHealth = GetComponent<EnemyHealth>();
		enemyHealth.health = enemyHealth.maxHealth;
		enemyHealth.dead = false;
		currentIndex = 1;
	}
}