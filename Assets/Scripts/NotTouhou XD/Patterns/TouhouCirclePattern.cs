using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouhouCirclePattern : TouhouPattern
{
    public override void ShootBullet ()
    {
        float angleOffset = 360f / (float) bulletsToShoot;

		 for (int i = 0; i < bulletsToShoot; i++)
        {
            var bullet = CreateBullet(transform.position, transform.rotation);
            float angle = angleOffset * i;

            InitBullet(this, bullet, angle, bulletSpeed, bulletAccel, bulletDamage, bulletStrafe);  
        }

		available = true;
    }
}