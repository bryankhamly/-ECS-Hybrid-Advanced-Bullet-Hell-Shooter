using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon 
{
	[Header("Weapon Info")]
	public string name; 
	public bool requireInput; //else autoshoot!
	public Transform shootPoint;

	[Header("Weapon Stats")]
	public float fireRate;

	[Header("Bullet Stats")]
	public int bulletsToShoot;
	public float bulletDamage;
	public float bulletSpeed;

	[Header("Bullet Offsets")]
	public float angleSpread;//  \ | / etc
	public float xOffset;// |_|_| or |__|__|
	
	[Header("Effects")]
	public AudioClip shootSfx;
}
