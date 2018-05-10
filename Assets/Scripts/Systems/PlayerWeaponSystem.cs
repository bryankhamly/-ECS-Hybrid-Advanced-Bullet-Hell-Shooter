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

    protected override void OnUpdate()
    {
        for (var i = 0; i < data.Length; i++)
        {
            var playerWeapons = data.PlayerWeapons[i];
            var dt = Time.deltaTime;
            playerWeapons.timer+=dt;

            if(playerWeapons.timer >= playerWeapons.attackSpeed)
            {
                Shoot(i);
            }
        }
    }

    void Shoot(int index)
    {
        var playerWeapons = data.PlayerWeapons[index];
        var bullet = GameObject.Instantiate(playerWeapons.Bullet, data.GameObject[index].transform.position, Quaternion.identity);
		var playerBullet = bullet.GetComponent<PlayerBullet>();
		playerBullet.shootDir = Vector2.up;
		playerWeapons.timer = 0;
    }
}