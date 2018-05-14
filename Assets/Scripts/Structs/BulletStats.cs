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