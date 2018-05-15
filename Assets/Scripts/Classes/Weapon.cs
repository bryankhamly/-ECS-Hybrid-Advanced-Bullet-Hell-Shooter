using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
	[Header("Weapon Info")]
	public string name;
	public bool active;
	public bool requireInput; //else autoshoot!
	public bool autoAim;
	public Transform shootPoint;

	[Header("Weapon Stats")]
	[Range(0f, 0.75f)]
	public float fireRate;

	[Header("Bullet Stats")]
	[Range(0f, 100f)]
	public int bulletsToShoot;
	[Range(0f, 100f)]
	public float bulletDamage;
	[Range(0f, 50f)]
	public float bulletSpeed;

	[Header("Bullet Offsets")]
	[Range(0f, 45f)]
	public float angleSpread;//  \ | / etc
	[Range(0f, 15f)]
	public float xOffset;// |_|_| or |__|__|
	[Range(0f, 10f)]
	public float bulletSpray;// Randomized x,y for non-static pattern, basically accuracy?
	
	[Header("Effects")]
	public AudioClip shootSfx;

	[Header("Timer")]
	public float timer;
}
