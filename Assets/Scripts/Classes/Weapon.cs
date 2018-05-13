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
	public float bulletDamage;
	public float bulletSpeed;

	[Header("Bullet Offsets")]
	public float angleSpread;
	public float xOffset; //Per bullet

	[Header("Effects")]
	public AudioClip shootSfx;
}
