using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerBulletSystem : ComponentSystem
{
    private struct Data
    {
        public int Length;
        public EntityArray Entity;
        public GameObjectArray GameObject;

        public ComponentArray<PlayerBullet> PlayerBullet;
    }

    [Inject] private Data data;

    protected override void OnUpdate ()
    {
        for (var i = 0; i < data.Length; i++)
        {
            var bullet = data.PlayerBullet[i];
            var transformXD = data.GameObject[i].transform;
            bullet.degree = bullet.Radian2Degrees (bullet.bulletStat.angle);
            transformXD.position = new Vector2 (transformXD.position.x + bullet.CalculateCosinePos (), transformXD.position.y + bullet.CalculateSinePos ());
        }
    }
}