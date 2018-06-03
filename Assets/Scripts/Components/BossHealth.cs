using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bad coding practices made me copy pasta from EnemyHealth :<

public class BossHealth : MonoBehaviour, IDamageable
{
	public Transform target;
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
	public TMPro.TextMeshProUGUI diffText;

	public GameObject bossHealthUI;
	public Transform bossHealthbar;

	public GameManager gameManager;
	int health1;
	int health2;
	int health3;

	void InitializeBossHealth ()
	{
		WaveManager wave = gameManager.waveManager;

		if (wave.difficulty == Difficulty.Normal)
		{
			health1 = wave.easyHealth1;
			health2 = wave.easyHealth2;
			health3 = wave.easyHealth3;
		}
		else
		{
			health1 = wave.hardHealth1;
			health2 = wave.hardHealth2;
			health3 = wave.hardHealth3;
		}

		maxHealth = health1;
		health = maxHealth;
	}

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
		InitializeBossHealth ();
		gameManager.audioSource.clip = gameManager.phase1;
		gameManager.audioSource.Play ();
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
			if (health <= health2 && phase1)
			{
				gameManager.audioSource.clip = gameManager.phase2;
				gameManager.audioSource.Play ();
				phase1 = false;
				phase2 = true;
				phase3 = false;
				behavior.StopPhase ();
				behavior.StartPhase (1);
			}

			if (health <= health3 && phase2)
			{
				gameManager.audioSource.clip = gameManager.phase3;
				gameManager.audioSource.Play ();
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
				if (gameManager.waveManager.difficulty == Difficulty.Normal)
				{
					diffText.text = "Difficulty: " + "Normal";
				}
				else
				{
					diffText.text = "Difficulty: " + "Hard";
				}

				bossHealthUI.SetActive (false);
				gameManager.player.GetComponent<PlayerHealth> ().invuln = true;
				Destroy (gameObject);
			}
		}
	}
}