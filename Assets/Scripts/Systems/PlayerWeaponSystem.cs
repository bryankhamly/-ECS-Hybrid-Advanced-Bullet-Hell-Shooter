using System;
using System.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

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
        Transform shootPoint = currentWeapon.shootPoint;

        float angleRadian;

        float2 bulletSpread;
        bulletSpread = new float2 (0, 0);
        float2 bulletXOffset;
        bulletXOffset = new float2 (0, 0);

        angleRadian = Mathf.Atan2 (shootDir.y, shootDir.x);

        if (angleRadian < 0f)
        {
            CalculateComplementary (ref angleRadian);
        }

        if (currentWeapon.bulletsToShoot > 1)
        {
            var spread = CalculateSpread (currentWeapon.bulletsToShoot, currentWeapon.angleSpread);
            var offset = CalculateOffset (currentWeapon.bulletsToShoot, currentWeapon.xOffset);
            bulletSpread = new float2 (spread.Item1, spread.Item2);
            bulletXOffset = new float2 (offset.Item1, offset.Item2);
        }

        for (int i = 0; i < currentWeapon.bulletsToShoot; i++)
        {
            float bulletAngle = angleRadian + bulletSpread.x + (i * bulletSpread.y);
            float bulletSpeed = currentWeapon.bulletSpeed;
            float bulletDamage = currentWeapon.bulletDamage;

            var bullet = GameObject.Instantiate (playerWeapons.Bullet, data.GameObject[index].transform.position, Quaternion.identity);
            PlayerBullet playerBullet = bullet.GetComponent<PlayerBullet> ();

            var cosPosition = Mathf.Cos (angleRadian - Mathf.PI / 2) * (bulletXOffset.x - i * bulletXOffset.y);
            var sinPosition = Mathf.Sin (angleRadian - Mathf.PI / 2) * (bulletXOffset.x - i * bulletXOffset.y);

            playerBullet.bulletStat = new BulletStats (bulletSpeed, bulletAngle, bulletDamage);

            var startPos = shootPoint.position + (shootPoint.transform.forward * (bulletXOffset.x - i * bulletXOffset.y));
            var bulletPos = new Vector2 (startPos.x + cosPosition, startPos.y + sinPosition);

            bullet.transform.position = bulletPos;

            playerWeapons.timer = 0;
        }
    }

    public float CalculateComplementary (ref float angle)
    {
        angle += Mathf.PI * 2; //pi = 180 degrees half a circle
        return angle;
    }

    public Tuple<float, float> CalculateSpread (float bulletCount, float value)
    {
        float baseValue = -value / 2;
        float iteration = value / (bulletCount - 1);
        return Tuple.Create (baseValue, iteration);
    }

    public Tuple<float, float> CalculateOffset (float bulletCount, float value)
    {
        float baseValue = value / 2;
        float iteration = value / (bulletCount - 1);
        return Tuple.Create (baseValue, iteration);
    }
}