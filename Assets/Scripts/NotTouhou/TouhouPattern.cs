using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TouhouPattern : MonoBehaviour
{
	public bool available = true;
	public TouhouBullet bulletPrefab;
	public float bulletSpeed;
	public float bulletsToShoot;

	public TouhouBullet CreateBullet (Vector2 pos, Quaternion rot)
	{
		var bullet = bulletPrefab.GetPooledInstance<TouhouBullet> ();
		bullet.transform.position = pos;
		bullet.transform.rotation = rot;
		return bullet;
	}

	public void InitBullet (TouhouBullet bullet, float angle, float speed)
	{
		bullet.Initialize (angle, speed);
	}

	public abstract void ShootBullet ();

	private void OnDisable ()
	{
		available = true;
	}
}