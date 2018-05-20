using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAttack
{
	public string attackName;
	public List<TouhouPattern> patterns;
	public float timeToWait;
}