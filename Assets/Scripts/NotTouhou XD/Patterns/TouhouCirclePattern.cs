using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouhouCirclePattern : TouhouPattern
{
    [Header ("[Half Circles]")]
    public bool useHalfCircle;
    [Range (0f, 360f)]
    public float aimAngle = 90f;

    [Header ("[AutoAim (For Half Circles)]")]
    public bool autoAim;
    public Transform target;

	private void Start ()
	{
		if(autoAim)
		{
			target = FindObjectOfType<PlayerHealth>().transform;
		}
	}
    
    public override void ShootBullet ()
    {
        if (autoAim)
        {
            LookAtTarget ();
        }

        float angleOffset;

        if (useHalfCircle)
        {
            angleOffset = 180f / (float) bulletsToShoot;
        }
        else
        {
            angleOffset = 360f / (float) bulletsToShoot;
        }

        for (int i = 0; i < bulletsToShoot; i++)
        {
            var bullet = CreateBullet (transform.position, transform.rotation);

            float angle;

            if (useHalfCircle)
            {
                angle = aimAngle + angleOffset * i;
            }
            else
            {
                angle = angleOffset * i;
            }

            InitBullet (this, bullet, angle, bulletSpeed, bulletAccel, bulletDamage, bulletStrafe);
        }

        available = true;

        if (autoAim)
        {
            LookAtTarget ();
        }
    }

    void LookAtTarget ()
    {
        if (target)
        {
            aimAngle = TrigStuff.CalculateZAngleDifference (transform.position, target.transform.position) - 90f;
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