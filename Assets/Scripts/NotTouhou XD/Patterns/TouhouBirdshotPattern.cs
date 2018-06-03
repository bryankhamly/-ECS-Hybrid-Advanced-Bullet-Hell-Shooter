using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouhouBirdshotPattern : TouhouPattern
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

	private void Start ()
	{
		target = transform.root.GetComponent<BossHealth> ().target;
	}

	public override void ShootBullet ()
	{
		if (target == null)
		{
			//target = transform.root.GetComponent<BossHealth>().target;
		}

		if (autoAim)
		{
			LookAtTarget ();
		}

		Shoot ();

		if (autoAim)
		{
			StartCoroutine (Aim ());
		}
	}

	private void Shoot ()
	{
		if (!available)
		{
			return;
		}

		available = false;

		int dirIndex = 0;
		float bSpeed = bulletSpeed;

		for (int i = 0; i < bulletsToShoot; i++)
		{
			if (directionCount <= dirIndex)
			{
				dirIndex = 0;

				bSpeed -= fireRate;

				while (bSpeed <= 0)
				{
					bSpeed += Mathf.Abs (fireRate);
				}
			}

			var bullet = CreateBullet (transform.position, transform.rotation);

			float dankAngle = directionCount % 2 == 0 ? aimAngle - (angleOffset / 2f) : aimAngle;

			float angle = CalculateOffset (dirIndex, dankAngle, angleOffset);

			InitBullet (this, bullet, angle, bSpeed, bulletAccel, bulletDamage, bulletStrafe);

			dirIndex++;
		}

		available = true;
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