using UnityEditor;
using UnityEngine;

public class PlayerBullet : PooledObject
{
    public BulletStats bulletStat;

    [HideInInspector]
    public float degree;

    bool IsVisibleFromCamera ()
    {
        bool visible;
        var rend = GetComponent<Renderer> ();
        if (rend.IsVisibleFrom (Camera.main))
        {
            visible = true;
        }
        else
        {
            visible = false;
        }
        return visible;
    }

    void Update ()
    {
        if (!IsVisibleFromCamera ())
        {
            ReturnToPool();
        }
    }
}