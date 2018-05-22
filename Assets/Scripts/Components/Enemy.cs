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

	private void OnEnable ()
	{
		var sprite = GetComponent<SpriteRenderer> ();
		sprite.color = Color.white;
		sprite.material.color = Color.white;
	}

	void OnDisable ()
	{
		var enemyHealth = GetComponent<EnemyHealth> ();
		var sprite = GetComponent<SpriteRenderer> ();
		sprite.color = Color.white;
		sprite.material.color = Color.white;
		enemyHealth.health = enemyHealth.maxHealth;
		enemyHealth.dead = false;
		currentIndex = 1;
	}
}