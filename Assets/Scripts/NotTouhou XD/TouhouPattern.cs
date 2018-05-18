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

	[Header ("[Bullet List]")]
	public List<TouhouBullet> bulletsShot; //In Touhou, when unit dies, all its shots turn into stuff. Gunna use this list to keep track

	public TouhouBullet CreateBullet (Vector2 pos, Quaternion rot)
	{
		var bullet = bulletPrefab.GetPooledInstance<TouhouBullet> ();
		bullet.transform.position = pos;
		bullet.transform.rotation = rot;
		return bullet;
	}

	public void InitBullet (TouhouPattern pattern, TouhouBullet bullet, float angle, float speed, float accel)
	{
		bulletsShot.Add (bullet);
		bullet.Initialize (pattern, angle, speed, accel);
	}

	public abstract void ShootBullet ();

	private void OnDisable ()
	{
		available = true;
	}
}