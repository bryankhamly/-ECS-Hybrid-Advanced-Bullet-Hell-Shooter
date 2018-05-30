using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlebEnemy : MonoBehaviour
{
    [Header("Weapon")]
    public TouhouPattern weapon; //Slap it as a child.

    [Header("AI")] 
    public float fireRate;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            ShootWeapon();
        }
    }

    public void ShootWeapon()
    {
        weapon.ShootBullet();
        timer = 0;
    }
    
}
