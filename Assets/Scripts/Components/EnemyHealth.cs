using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Could be a system with I used IComponentData but meh.

public class EnemyHealth : MonoBehaviour, IDamageable
{
	public int maxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
	public int health { get { return _health; } set { _health = value; } }

	[SerializeField]
	private int _maxHealth;
	[SerializeField]
	private int _health;

	public bool dead;
	public PooledObject explosion;
	public PooledObject drop;

	public Animator anim;

	void Start ()
	{
		health = maxHealth;
		if (anim == null)
			anim = GetComponent<Animator> ();
	}

	public void TakeDamage (int value)
	{
		if (!dead)
		{
			health -= value;
			anim.SetTrigger ("damage");

			if (health <= 0)
			{
				var explosionXD = explosion.GetPooledInstance<PooledObject> ();
				explosionXD.transform.position = transform.position;

				var dropXD = drop.GetPooledInstance<PooledObject> ();
				dropXD.transform.position = transform.position;
				Rigidbody2D dropRb = dropXD.GetComponent<Rigidbody2D>();
				dropRb.AddForce(new Vector2(0, 250));

				dead = true;
				GetComponent<Enemy> ().ReturnToPool ();
			}
		}
	}
}