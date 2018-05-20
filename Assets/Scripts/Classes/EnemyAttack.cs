using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAttack
{
	[SerializeField]
	public string attackName;
	[SerializeField]
	public List<TouhouPattern> patterns;
	[SerializeField]
	public float timeToWait;
}