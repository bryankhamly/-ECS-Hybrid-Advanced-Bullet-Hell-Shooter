using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouhouCurvePattern : TouhouPattern
{
	[Header ("[Pattern]")]
	public float directionCount = 3f;
	[Range (0f, 360f)]
	public float aimAngle = 180f;
	public float curveSize = 45f;
	public float curveSpeed = 5f;
	public float angleOffset = 20f;
	public float fireRate = 0.075f;

	[Header ("[AutoAim]")]
	public bool autoAim;
	public Transform target;

	public override void ShootBullet ()
	{
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

	IEnumerator Shoot ()
	{
		if (!available)
			yield break;

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

			EnemyBehavior eb = GetComponentInParent<EnemyBehavior> ();
			float centerAngle = aimAngle + (curveSize / 2f * Mathf.Sin (eb.frames * curveSpeed / 100f));

			float baseAngle = directionCount % 2 == 0 ? centerAngle - (angleOffset / 2f) : centerAngle;

			float angle = CalculateOffset (dirIndex, baseAngle, angleOffset);

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