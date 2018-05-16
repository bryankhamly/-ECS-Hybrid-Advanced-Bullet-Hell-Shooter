public enum BulletType
{
    Normal,
    Piercing
}

[System.Serializable]
public struct BulletStats
{
    public BulletType bulletType;
    public float speed;
    public float angle;
    public int damage;

    public BulletStats (BulletType bulletType, float speed, float angle, int damage)
    {
        this.bulletType = bulletType;
        this.speed = speed;
        this.angle = angle;
        this.damage = damage;
    }
}