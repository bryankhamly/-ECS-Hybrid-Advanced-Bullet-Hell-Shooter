using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PooledObject
{
	public Waypoint myWaypoint;
	public float speed;
	public Transform currentPoint;
	public int currentIndex;

	public List<Enemy> myGroup;

	void Start ()
	{
		currentIndex = 1;
	}

	private void OnEnable ()
	{
		ResetHurt ();
	}

	void OnDisable ()
	{
		myGroup.Remove(this);
		var enemyHealth = GetComponent<EnemyHealth> ();
		ResetHurt ();
		enemyHealth.health = enemyHealth.maxHealth;
		enemyHealth.dead = false;
		currentIndex = 1;
	}

	void ResetHurt ()
	{
		var sprite = GetComponent<SpriteRenderer> ();
		sprite.color = Color.white;
		sprite.material.color = Color.white;
	}
}