using UnityEditor;
using UnityEngine;

[System.Serializable]
public struct BulletStats
{
    public float speed;
    public float angle;
    public float damage;

    public BulletStats (float speed, float angle, float damage)
    {
        this.speed = speed;
        this.angle = angle;
        this.damage = damage;
    }
}

public class PlayerBullet : MonoBehaviour
{
    public BulletStats bulletStat;
    public float degree;

    public float CalculateCosinePos ()
    {
        float x;
        x = Mathf.Cos (bulletStat.angle) * bulletStat.speed * Time.deltaTime;
        return x;
    }

    public float CalculateSinePos ()
    {
        float y;
        y = Mathf.Sin (bulletStat.angle) * bulletStat.speed * Time.deltaTime;
        return y;
    }

    public float Radian2Degrees (float radian)
    {
        float degree;
        degree = radian * Mathf.Rad2Deg;
        return degree;
    }

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
            Destroy (gameObject, 1);
        }
    }
}