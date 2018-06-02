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
	[Range (-90f, 90f)]
	public float bulletStrafe;

	float oldSpeed;

	[HideInInspector]
	public bool destroyOnDone;

	[Header ("[Bullet List]")]
	public List<TouhouBullet> bulletsShot; //In Touhou, when unit dies, all its shots turn into stuff. Gunna use this list to keep track - not doing this anymore XDD

	private void Awake ()
	{
		oldSpeed = bulletSpeed;
	}

	private void Update ()
	{
		if (available == true && destroyOnDone == true)
		{
			Destroy (gameObject);
		}
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

	public void InitBullet (TouhouPattern pattern, TouhouBullet bullet, float angle, float speed, float accel, int damage, float strafe)
	{
		bulletsShot.Add (bullet);
		bullet.Initialize (pattern, angle, speed, accel, damage, strafe);
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