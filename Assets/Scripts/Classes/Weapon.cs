using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon 
{
	[Header("Weapon Info")]
	public string name; 
	public bool requireInput;
	public Transform shootPoint;

	[Header("Weapon Stats")]
	public float fireRate;

	[Header("Bullet Stats")]
	public float bulletsToShoot;
	public float bulletSpeed;
	public float bulletDamage;
	public float bulletSpread;

	[Header("Effects")]
	public AudioClip shootSfx;

}
