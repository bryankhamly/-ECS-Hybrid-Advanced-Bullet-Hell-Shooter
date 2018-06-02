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

	[Header ("[AutoAim]")]
	public bool autoAim;
	public Transform target;

	[Header ("[Spin]")]
	public bool spin;
	public float spinAngle;


	public override void ShootBullet ()
	{
		 if (target == null)
        {
            target = FindObjectOfType<PlayerHealth> ().transform;
        }

		if (autoAim)
		{
			LookAtTarget ();
		}

		StartCoroutine (Shoot ());

		if (autoAim)
		{
			StartCoroutine (Aim ());
		}
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

			float spinDankAngle = aimAngle + (spinAngle * Mathf.Floor (i / directionCount));
			float dankAngle;
			
			if (spin)
			{
				dankAngle = directionCount % 2 == 0 ? spinDankAngle - (angleOffset / 2f) : spinDankAngle;

			}
			else
			{
				dankAngle = directionCount % 2 == 0 ? aimAngle - (angleOffset / 2f) : aimAngle;

			}

			float angle = CalculateOffset (dirIndex, dankAngle, angleOffset);

			InitBullet (this, bullet, angle, bulletSpeed, bulletAccel, bulletDamage, bulletStrafe);

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

	void LookAtTarget ()
	{
		if (target)
		{
			aimAngle = TrigStuff.CalculateZAngleDifference (transform.position, target.transform.position);
		}
	}

	IEnumerator Aim ()
	{
		while (autoAim)
		{
			if (available)
			{
				yield break;
			}

			LookAtTarget ();

			yield return 0;
		}
		yield break;
	}
}