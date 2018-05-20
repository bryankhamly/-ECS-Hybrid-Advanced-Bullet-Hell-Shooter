using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
	public EnemyPhase[] phases;

	IEnumerator ShootPhase (int phaseIndex)
	{
		List<EnemyAttack> attacks = new List<EnemyAttack> ();

		for (int i = 0; i < phases[phaseIndex].enemyAttacks.Length; i++)
		{
			attacks.Add (phases[phaseIndex].enemyAttacks[i]);

			foreach (var item in phases[phaseIndex].enemyAttacks[i].patterns)
			{
				item.ShootBullet ();
			}

			yield return new WaitForSeconds (phases[phaseIndex].enemyAttacks[i].timeToWait);
			attacks.Remove (phases[phaseIndex].enemyAttacks[i]);
		}
	}

	public void StartPhase(int index)
	{
		StartCoroutine(ShootPhase(index));
	}

	public void StopPhase ()
	{
		StopAllCoroutines ();
	}
}