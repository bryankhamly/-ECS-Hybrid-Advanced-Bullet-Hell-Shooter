using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjectSpawns
{
	public string objectName;
	public PooledObject objectToSpawn;
	public int amount;
}