using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouhouNormalPattern : TouhouPattern
{
    public float angle = 180f; // _____
    public float fireRate = 0.5f;

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
            InitBullet (bullet, angle, bulletSpeed);
        }

        available = true;
        
        yield break;
    }
}