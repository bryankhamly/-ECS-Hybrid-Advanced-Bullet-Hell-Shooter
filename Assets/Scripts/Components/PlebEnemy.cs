using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlebEnemy : MonoBehaviour
{
    [Header ("Weapon")]
    public bool aim;
    public TouhouPattern weapon; //Slap it as a child.
    public TouhouPattern aimWeapon;

    [Header ("AI")]
    public float fireRate;

    private float timer;

    private void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            ShootWeapon ();
        }
    }

    public void ShootWeapon ()
    {
        if (aim)
        {
            aimWeapon.ShootBullet ();
        }
        else
        {
            weapon.ShootBullet ();
        }

        timer = 0;
    }

}