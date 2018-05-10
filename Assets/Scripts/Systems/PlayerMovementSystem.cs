using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovementSystem : ComponentSystem
{
    private struct Data
    {
        public int Length;
        public EntityArray Entity;
        public GameObjectArray GameObject;

        public ComponentArray<PlayerMovement> PlayerMovement;
        public ComponentArray<Rigidbody2D> Rb;
    }
    
    [Inject] private Data data;

    private string horizontalAxis = "Horizontal";
    private string verticalAxis = "Vertical";

    protected override void OnUpdate()
    {
        for (var i = 0; i < data.Length; i++)
        {
            var speed = data.PlayerMovement[i].speed;
            var t = data.GameObject[i].transform;
            var dt = Time.deltaTime;
            var rb = data.Rb[i];

            float hAxis = Input.GetAxis(horizontalAxis);
            float vAxis = Input.GetAxis(verticalAxis);
            
            rb.velocity = new float2(hAxis, vAxis) * speed;     
        }
    }
}