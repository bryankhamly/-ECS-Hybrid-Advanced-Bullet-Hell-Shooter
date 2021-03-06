﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouhouNormalPattern : TouhouPattern
{
    [Header ("[Pattern]")]
    [Range (0f, 360f)]
    public float angle = 180f; // _____
    public float fireRate = 0.5f;

    [Header ("[AutoAim]")]
    public bool autoAim;
    public Transform target;

    private void Start ()
    {
        StartCoroutine(GetMeTarget());
    }

    IEnumerator GetMeTarget ()
    {
        yield return new WaitForSeconds (1);
        if (autoAim)
        {
            target = transform.root.GetComponent<BossHealth> ().target;
        }
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

        for (int i = 0; i < bulletsToShoot; i++)
        {
            if (0 < i && 0f < fireRate)
            {
                yield return new WaitForSeconds (fireRate);
            }
            var bullet = CreateBullet (transform.position, transform.rotation);
            InitBullet (this, bullet, angle, bulletSpeed, bulletAccel, bulletDamage, bulletStrafe);
        }

        available = true;

        yield break;
    }

    void LookAtTarget ()
    {
        if (target)
        {
            angle = TrigStuff.CalculateZAngleDifference (transform.position, target.transform.position);
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