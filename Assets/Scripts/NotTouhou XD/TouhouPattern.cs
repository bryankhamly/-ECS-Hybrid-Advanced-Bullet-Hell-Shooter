using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inspired by Touhou and Bullet Heaven 2
//Bryan Khamly

public abstract class TouhouPattern : MonoBehaviour
{
	[Header ("[Bullet]")]
	public bool available = true;
	public TouhouBullet bulletPrefab;
	public float bulletSpeed;
	public float bulletAccel;
	public float bulletsToShoot;
	public int bulletDamage;

	float oldSpeed;

	[Header ("[Bullet List]")]
	public List<TouhouBullet> bulletsShot; //In Touhou, when unit dies, all its shots turn into stuff. Gunna use this list to keep track

	private void Awake ()
	{
		oldSpeed = bulletSpeed;
	}

	public void ResetBulletSpeed ()
	{
		bulletSpeed = oldSpeed;
	}

	public TouhouBullet CreateBullet (Vector2 pos, Quaternion rot)
	{
		var bullet = bulletPrefab.GetPooledInstance<TouhouBullet> ();
		bullet.transform.position = pos;
		bullet.transform.rotation = rot;
		return bullet;
	}

	public void InitBullet (TouhouPattern pattern, TouhouBullet bullet, float angle, float speed, float accel, int damage)
	{
		bulletsShot.Add (bullet);
		bullet.Initialize (pattern, angle, speed, accel, damage);
	}

	public abstract void ShootBullet ();

	private void OnDisable ()
	{
		available = true;
	}

	public void StopShooting ()
	{
		available = true;
		StopAllCoroutines ();
	}
}