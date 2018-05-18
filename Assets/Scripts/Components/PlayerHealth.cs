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

	private void Start ()
	{
		health = maxHealth;
	}

	public void TakeDamage (int value)
	{
		Debug.Log ("Ow " + value);
	}
}