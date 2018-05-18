using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
	public int maxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
	public int health { get { return _health; } set { _health = value; } }

	[SerializeField]
	private int _maxHealth;
	[SerializeField]
	private int _health;

	public bool invuln;
	float invulnTimer = 0;
	const float invulnTime = 1.5f;
	public bool dead;

	SpriteRenderer sprite;
	Color c255 = new Color ();
	Color c0 = new Color ();

	private void Start ()
	{
		if (sprite == null)
			sprite = GetComponentInChildren<SpriteRenderer> ();
		health = maxHealth;
		c255 = sprite.color;
		c0 = new Color (c255.r, c255.g, c255.b, 0);
	}

	private void Update ()
	{
		invulnTimer += Time.deltaTime;

		if (invulnTimer > invulnTime)
		{
			invuln = false;
		}
	}

	public void TakeDamage (int value)
	{
		if (invuln)
		{
			return;
		}

		if (!dead)
		{
			health -= value;

			//dead check
			if (health <= 0)
			{
				dead = true;
				return;
			}

			invuln = true;
			invulnTimer = 0;

			//Do invuln here
			StartCoroutine (InvulnFlash ());
		}
	}

	IEnumerator InvulnFlash ()
	{
		while (invuln)
		{
			sprite.color = c0;
			yield return new WaitForSeconds (0.2f);
			sprite.color = c255;
			yield return new WaitForSeconds (0.2f);
		}
	}
}