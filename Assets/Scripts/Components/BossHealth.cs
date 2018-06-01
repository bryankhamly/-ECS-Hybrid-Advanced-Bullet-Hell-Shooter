using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bad coding practices made me copy pasta from EnemyHealth :<

public class BossHealth : MonoBehaviour, IDamageable
{
	public int maxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
	public int health { get { return _health; } set { _health = value; } }

	[SerializeField]
	private int _maxHealth;
	[SerializeField]
	private int _health;

	public bool dead;
	public PooledObject explosion;
	public PooledObject drop;

	public Animator anim;

	EnemyBehavior behavior;

	bool phase1;
	bool phase2;
	bool phase3;

	public GameObject victoryScreen;
	public TMPro.TextMeshProUGUI winScore;
	public TMPro.TextMeshProUGUI score;

	public GameObject bossHealthUI;
	public Transform bossHealthbar;

	public GameManager gameManager;

	void Start ()
	{
		phase1 = true;
		health = maxHealth;
		if (anim == null)
			anim = GetComponentInChildren<Animator> ();

		if (behavior == null)
			behavior = GetComponent<EnemyBehavior> ();
	}

	//This method should be somewhere else but... oh well XD
	public void StartBoss ()
	{
		GetComponentInChildren<BossRotator> ().move = true;
	}

	public void ActualStart ()
	{
		bossHealthUI.SetActive (true);
		GetComponent<BoxCollider2D> ().enabled = true;
		behavior.StartPhase (0);
	}

	public void TakeDamage (int value)
	{
		if (gameManager.player.GetComponent<PlayerHealth> ().dead)
			return;

		if (!dead)
		{
			health -= value;
			anim.SetTrigger ("damage");

			float healthPercent = (float) health / (float) maxHealth;
			bossHealthbar.localScale = new Vector3 (healthPercent, 1, 1);

			//eww hardcoded
			if (health <= 6000 && phase1)
			{
				phase1 = false;
				phase2 = true;
				phase3 = false;
				behavior.StopPhase ();
				behavior.StartPhase (1);
			}

			if (health <= 3000 && phase2)
			{
				phase1 = false;
				phase2 = false;
				phase3 = true;
				behavior.StopPhase ();
				behavior.StartPhase (2);
			}

			if (health <= 0)
			{
				var explosionXD = explosion.GetPooledInstance<PooledObject> ();
				explosionXD.transform.position = transform.position;

				var dropXD = drop.GetPooledInstance<PooledObject> ();
				dropXD.transform.position = transform.position;
				Rigidbody2D dropRb = dropXD.GetComponent<Rigidbody2D> ();
				dropRb.AddForce (new Vector2 (0, 250));

				dead = true;
				behavior.StopPhase ();
				victoryScreen.SetActive (true);
				winScore.text = "Score: " + score.text;
				bossHealthUI.SetActive (false);
				gameManager.player.GetComponent<PlayerHealth> ().invuln = true;
				Destroy (gameObject);
			}
		}
	}
}