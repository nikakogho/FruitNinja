using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour {

	[Header("Default")]
	public GameObject parentSpawner;
	public float minForce = 8, maxForce = 15;
	public FruitBlueprint[] fruits;
	public float minTime, maxTime;
	private int currentWave = 0;
	private int amount = 0;
	private float countdown, spawnTime, total;
	private List<FruitBlueprint> toSpawn = new List<FruitBlueprint>();
	private Transform[] spawnPoints;
	public int minAmount, maxAmount;
	[HideInInspector]public bool gameOver = false;
	public static WaveSpawner instance;
	[Header("Lives")]
	public bool livesDecrease = true;
	public int startLives = 5;
	public Image healthBar;
	[HideInInspector]public int lives;
	[Header("Countdown")]
	public bool hasEnd = false;
	public float timeLeft;
	public Text countdownText;

	void Awake()
	{
		lives = startLives;
		instance = this;
		spawnPoints = new Transform[parentSpawner.transform.childCount];

		for (int i = 0; i < spawnPoints.Length; i++) 
		{
			spawnPoints [i] = parentSpawner.transform.GetChild (i);
		}
	}

	void Update()
	{
		if (gameOver)
			return;
		if (amount <= 0) 
		{
			NextWave ();
		}

		countdown -= Time.deltaTime;

		if (countdown <= 0) 
		{
			countdown = spawnTime;

			int spawnAmount = Random.Range (minAmount, maxAmount);

			for(int i = 0; i < spawnAmount; i++)
			Spawn ();
		}

		if (livesDecrease) 
		{
			lives = Mathf.Clamp (lives, 0, startLives);

			if (lives == 0) 
			{
				End ();
			}

			healthBar.fillAmount = (float)lives / (float)startLives;
		}

		if (hasEnd) 
		{
			timeLeft -= Time.deltaTime;

			if (timeLeft < 0)
				timeLeft = 0;

			countdownText.text = string.Format ("{00:00.00}", timeLeft);

			if (timeLeft == 0) 
			{
				End ();
			}
		}
	}

	void End()
	{
		gameOver = true;
		FindObjectOfType<Animator> ().SetTrigger ("End");
		Time.timeScale = 0.1f;
	}

	void Spawn()
	{
		int index = 0;
		float sum = 0;
		float rand = Random.Range (0, total);

		for (int i = 0; i < toSpawn.Count; i++) 
		{
			if (rand <= sum + toSpawn [i].chance) 
			{
				index = i;
				break;
			}

			sum += toSpawn [i].chance;
		}

		GameObject g = toSpawn [index].prefab;
		Transform t = spawnPoints [Random.Range (0, spawnPoints.Length)];

		GameObject clone = Instantiate (g, t.position, t.rotation);
		clone.GetComponent<Rigidbody2D> ().AddForce (t.up * Random.Range(minForce, maxForce), ForceMode2D.Impulse);

		amount--;
	}

	void NextWave()
	{
		currentWave++;
		toSpawn = new List<FruitBlueprint> ();

		total = 0;

		foreach (FruitBlueprint fruit in fruits) 
		{
			if (currentWave >= fruit.unlockWave)
			{
				toSpawn.Add (fruit);
				total += fruit.chance;
			}
		}

		amount = currentWave * 8;
		float MinTime = minTime * (1f + Mathf.Sin (currentWave) * 0.1f);
		float MaxTime = maxTime * (1f + Mathf.Sin (currentWave) * 0.15f);
		spawnTime = Random.Range (MinTime, MaxTime);
		countdown = spawnTime;
	}
}
