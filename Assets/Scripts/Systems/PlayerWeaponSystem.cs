using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using System;
using System.Collections;

public class PlayerWeaponSystem : ComponentSystem
{
    private struct Data
    {
        public int Length;
        public EntityArray Entity;
        public GameObjectArray GameObject;

        public ComponentArray<PlayerWeapons> PlayerWeapons;
    }

    [Inject] private Data data;

    protected override void OnUpdate ()
    {
        for (var i = 0; i < data.Length; i++)
        {
            var playerWeapons = data.PlayerWeapons[i];
            var dt = Time.deltaTime;
            playerWeapons.timer += dt;

            if (playerWeapons.timer >= playerWeapons.currentWeapon.fireRate)
            {
                Shoot (i);
            }
        }
    }

    void Shoot (int index)
    {
        Vector2 shootDir = Vector2.up;
        var playerWeapons = data.PlayerWeapons[index];
        var currentWeapon = playerWeapons.currentWeapon;

        float angleRadian;
        float2 bulletSpread;
        bulletSpread = new float2(0,0);

        angleRadian = Mathf.Atan2 (shootDir.y, shootDir.x);

        if (angleRadian < 0f)
        {
            CalculateComplementary (ref angleRadian);
        }

        if (currentWeapon.bulletsToShoot > 1)
        {
            var spread = CalculateSpread(currentWeapon.bulletsToShoot, currentWeapon.angleSpread);
            bulletSpread = new float2(spread.Item1, spread.Item2);     
        }

        for (int i = 0; i < currentWeapon.bulletsToShoot; i++)
        {
            float bulletAngle = angleRadian + bulletSpread.x + (i * bulletSpread.y);
            float bulletSpeed = currentWeapon.bulletSpeed;
            float bulletDamage = currentWeapon.bulletDamage;

            var bullet = GameObject.Instantiate (playerWeapons.Bullet, data.GameObject[index].transform.position, Quaternion.identity);
            var playerBullet = bullet.GetComponent<PlayerBullet> ();
            playerBullet.bulletStat = new BulletStats(bulletSpeed, bulletAngle, bulletDamage);
            playerWeapons.timer = 0;
        }
    }

    public float CalculateComplementary (ref float angle)
    {
        angle = angle + Mathf.PI * 2;
        return angle;
    }

    public Tuple <float,float> CalculateSpread(float bulletsShot, float spreadValue)
    {
        float baseValue = -spreadValue / 2;
        float iteration = spreadValue / (bulletsShot - 1);
        return Tuple.Create(baseValue, iteration);
    }
}