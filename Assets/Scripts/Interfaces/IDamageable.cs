using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
	int maxHealth { get; set; }
	int health { get; set; }
	void TakeDamage (int value);
}