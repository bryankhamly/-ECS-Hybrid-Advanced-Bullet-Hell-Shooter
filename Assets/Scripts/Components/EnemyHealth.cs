using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Could be a system with I used IComponentData but meh.

public class EnemyHealth : MonoBehaviour, IDamageable
{
	public int maxHealth = 100;
	public int health = 100;
	public bool dead;

	public Animator anim;

	void Start ()
	{
		health = maxHealth;
		anim = GetComponent<Animator>();
	}

	public void TakeDamage (int value)
	{
		if (!dead)
		{
			health -= value;
			anim.SetTrigger("damage");

			if (health <= 0)
			{
				dead = true;
				GetComponent<Enemy> ().ReturnToPool ();
			}
		}
	}
}