using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpinDirection
{
	CounterClockwise = 1,
	ClockWise = -1
}

public class TouhouSpinPattern : TouhouPattern
{
	[Header ("[Pattern]")]
	public SpinDirection spinDirection;
	[Range(1, 180)]
	public float angleOffset;
	public float fireRate;

	public override void ShootBullet ()
	{
		StartCoroutine (Shoot ());
	}

	IEnumerator Shoot ()
	{
		if (!available)
			yield break;

		available = false;

		for (int i = 0; i < bulletsToShoot; i++)
		{
			if (0 < i && 0f < fireRate)
			{
				yield return new WaitForSeconds (fireRate);
			}
			var bullet = CreateBullet (transform.position, transform.rotation);

			float angle = 0 + (angleOffset * i) * ((int)spinDirection);

			InitBullet (this, bullet, angle, bulletSpeed, bulletAccel, bulletDamage, bulletStrafe);
		}

		available = true;

		yield break;
	}
}