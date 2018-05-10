using UnityEditor;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    public Vector2 shootDir;
    [HideInInspector]
    public float angle;
    [HideInInspector]
    public float degree;

    public float CalculateCosinePos ()
    {
        float x;
        x = Mathf.Cos (angle) * speed * Time.deltaTime;
        return x;
    }

    public float CalculateSinePos ()
    {
        float y;
        y = Mathf.Sin (angle) * speed * Time.deltaTime;
        return y;
    }

    public float CalculateComplementary (ref float angle)
    {
        angle = angle + Mathf.PI * 2;
        return angle;
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