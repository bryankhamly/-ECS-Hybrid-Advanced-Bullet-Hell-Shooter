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
	[Range (1, 180)]
	public float angleOffset;
	public float fireRate;

	[Header ("[Sprinkler]")]
	public bool sprinkler;
	public int sprinkly;

	public override void ShootBullet ()
	{
		StartCoroutine (Shoot ());
	}

	IEnumerator Shoot ()
	{
		if (!available)
			yield break;

		available = false;

		float sprinklerOffset = 360f / sprinkly;

		int sprinklerIndex = 0;

		if (sprinkler)
		{
			for (int i = 0; i < bulletsToShoot; i++)
			{
				if (sprinkly <= sprinklerIndex)
				{
					sprinklerIndex = 0;
					if (0f < fireRate)
					{
						yield return new WaitForSeconds (fireRate);
					}
				}

				var bullet = CreateBullet (transform.position, transform.rotation);

				float angle = 180 + (sprinklerOffset * sprinklerIndex) + (angleOffset * Mathf.Floor (i / sprinkly)) * ((int) spinDirection);
				InitBullet (this, bullet, angle, bulletSpeed, bulletAccel, bulletDamage, bulletStrafe);
				sprinklerIndex++;
			}

		}
		else
		{
			for (int i = 0; i < bulletsToShoot; i++)
			{
				if (0 < i && 0f < fireRate)
				{
					yield return new WaitForSeconds (fireRate);
				}
				var bullet = CreateBullet (transform.position, transform.rotation);

				float angle = 0 + (angleOffset * i) * ((int) spinDirection);

				InitBullet (this, bullet, angle, bulletSpeed, bulletAccel, bulletDamage, bulletStrafe);
			}
		}

		available = true;
		yield break;
	}
}