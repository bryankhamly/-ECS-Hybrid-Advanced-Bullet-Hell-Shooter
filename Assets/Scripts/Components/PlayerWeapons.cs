using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
	public WaveManager waveManager;

	public Transform ShootPoint;
	public Transform Target;

	public Weapon defaultWeapon;

	public WeaponContainer[] weapons;

	public List<Weapon> weaponList;

	bool exist;

	void Awake ()
	{
		weaponList.Add (defaultWeapon);
		foreach (var item in weapons)
		{
			weaponList.Add (item.weapon);
		}
	}

	private void Update ()
	{
		if (waveManager.done)
		{
			Target = waveManager.bossList[0];
			return;
		}

		if (Target != null && waveManager.enemyList.Count > 0)
		{
			exist = waveManager.enemyList.Contains (Target.GetComponent<Enemy> ());
		}

		if (Target == null && waveManager.enemyList.Count > 0 || !exist)
		{
			int random = Random.Range (0, waveManager.enemyList.Count);
			Target = waveManager.enemyList[random].transform;
		}
	}
}