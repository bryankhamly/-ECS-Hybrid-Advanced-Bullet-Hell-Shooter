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

            for (int j = 0; j < playerWeapons.weaponList.Count; j++)
            {
                Weapon weapon = playerWeapons.weaponList[j];
                if (weapon.active)
                {
                    weapon.timer += dt;
                    if (weapon.timer >= weapon.fireRate)
                    {
                        Shoot (i, j);
                    }
                }
            }
        }
    }

    void Shoot (int index, int weaponIndex)
    {
        var playerWeapons = data.PlayerWeapons[index];
        var currentWeapon = playerWeapons.weaponList[weaponIndex];
        Transform shootPoint = playerWeapons.ShootPoint;

        if (currentWeapon.muzzleFlash)
        {
            var flash = currentWeapon.muzzleFlash.GetPooledInstance<Muzzleflash> ();
            flash.transform.position = (Vector2) shootPoint.position + currentWeapon.flashOffset;
        }

        Vector2 shootDir;

        if (currentWeapon.autoAim)
        {
            Vector3 diff = playerWeapons.Target.position - data.GameObject[index].transform.position;
            float zRot = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
            shootPoint.rotation = Quaternion.Euler (0, 0, zRot);
            shootDir = shootPoint.right;
        }
        else
        {
            shootDir = Vector2.up;
        }

        float angleRadian;

        float2 bulletSpread;
        bulletSpread = new float2 (0, 0);
        float2 bulletXOffset;
        bulletXOffset = new float2 (0, 0);

        angleRadian = Mathf.Atan2 (shootDir.y, shootDir.x);

        if (angleRadian < 0f)
        {
            TrigStuff.CalculateComplementary (ref angleRadian);
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
            BulletType bulletType = currentWeapon.bulletType;
            float bulletAngle = angleRadian + bulletSpread.x + (i * bulletSpread.y);
            float bulletSpeed = currentWeapon.bulletSpeed;
            int bulletDamage = currentWeapon.bulletDamage;
            var bulletSpray = CalculateSpray (currentWeapon.bulletSpray);

            var bullet = currentWeapon.bullet.GetPooledInstance<PlayerBullet> ();
            PlayerBullet playerBullet = bullet.GetComponent<PlayerBullet> ();

            var cosPosition = Mathf.Cos (angleRadian - Mathf.PI / 2) * (bulletXOffset.x - i * bulletXOffset.y);
            var sinPosition = Mathf.Sin (angleRadian - Mathf.PI / 2) * (bulletXOffset.x - i * bulletXOffset.y);

            //Initialize Bullet (Man I love constructors but monobehaviours are lame sometimes.)
            playerBullet.bulletStat = new BulletStats (bulletType, bulletSpeed, bulletAngle, bulletDamage);

            //Set Position
            var startPos = shootPoint.position + (shootPoint.transform.forward * (bulletXOffset.x - i * bulletXOffset.y));
            var bulletPos = new Vector2 (startPos.x + cosPosition + bulletSpray.x, startPos.y + sinPosition + bulletSpray.y);

            bullet.transform.position = bulletPos;

            //Set Angle
            bullet.transform.eulerAngles = new Vector3 (0, 0, TrigStuff.Radian2Degrees (bulletAngle));

            currentWeapon.timer = 0;
        }
    }

    public Vector2 CalculateSpray (float value)
    {
        float xSpray = UnityEngine.Random.Range (0f, 1f) * value - value / 2;
        float ySpray = UnityEngine.Random.Range (0f, 1f) * value - value / 2;
        return new Vector2 (xSpray, ySpray);
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