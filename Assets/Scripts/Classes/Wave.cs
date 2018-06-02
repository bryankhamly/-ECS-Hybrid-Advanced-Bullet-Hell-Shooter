using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string waveName;
    public SpawnData[] spawns;
}

[System.Serializable]
public struct SpawnData
{
    public string enemyName;
    public int waypointIndex; //Which waypoint to use
    public PooledObject enemy;
    public int enemyCount;
    public float timePerSpawn;
    public bool aim;
}