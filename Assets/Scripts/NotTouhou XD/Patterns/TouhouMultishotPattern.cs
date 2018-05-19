using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouhouMultishotPattern : TouhouPattern
{
	[Header ("[Pattern]")]
	public int directionCount;
	[Range (0f, 360f)]
	public float aimAngle;
	[Range (0f, 360f)]
	public float angleOffset;
	public float fireRate;

	public override void ShootBullet ()
	{
		StartCoroutine (Shoot ());
	}

	private IEnumerator Shoot ()
	{
		if (!available)
		{
			yield break;
		}

		available = false;

		int dirIndex = 0;

		for (int i = 0; i < bulletsToShoot; i++)
		{
			if (directionCount <= dirIndex)
			{
				dirIndex = 0;

				if (0f < fireRate)
				{
					yield return new WaitForSeconds (fireRate);
				}
			}

			var bullet = CreateBullet (transform.position, transform.rotation);

			float dankAngle = directionCount % 2 == 0 ? aimAngle - (angleOffset / 2f) : aimAngle;

			float angle = CalculateOffset (dirIndex, dankAngle, angleOffset);

			InitBullet (this, bullet, angle, bulletSpeed, bulletAccel, bulletDamage);

			dirIndex++;
		}

		available = true;

		yield break;
	}

	public float CalculateOffset (int index, float aimAngle, float angleOffset)
	{
		float angle = index % 2 == 0 ?
			aimAngle - (angleOffset * (float) index / 2f) :
			aimAngle + (angleOffset * Mathf.Ceil ((float) index / 2f));
		return angle;
	}
}