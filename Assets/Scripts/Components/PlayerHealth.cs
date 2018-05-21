using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//No system needed for this! 
//System works best when it can batch handle logic. This is only on 1 player object.

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

	public Transform lifeIcon;
	public List<Transform> lifeIcons;

	private void Start ()
	{
		foreach (Transform child in lifeIcon)
		{
			lifeIcons.Add (child);
		}

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
			Camera.main.GetComponent<CameraShake> ().shakeTimer = 0;
			health -= value;

			GameObject topkek = lifeIcons[lifeIcons.Count - 1].gameObject;
			lifeIcons.RemoveAt (lifeIcons.Count - 1);
			Destroy (topkek);

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