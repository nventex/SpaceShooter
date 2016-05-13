using System.Collections;

using Assets.GameObjects;

using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameObject Hazard, EnemyShip;

	public Vector3 SpawnValues;

	public int HazardCount;

	public float SpawnWait;

	public float StartWait;

	public float WaveWait;

	public GUIText ScoreText;

	public GUIText RestartText;

	public GUIText GameOverText;

	public Transform Boundary;

	private int score;

	private bool GameOver;

	private bool Restart;

	private FixedPathEnemies fixedPathEnemies;

	private int enemiesPerWave;

	private FixedPathEnemies fixedPathEnemies2;

	void Start()
	{
		this.enemiesPerWave = Random.Range(3, 8);
		this.GameOver = false;
		this.Restart = false;
		RestartText.text = string.Empty;
		GameOverText.text = string.Empty;
		this.score = 0;
		this.UpdateScore();
		this.BeginSpawn();
	}

	void Update()
	{
		if (this.Restart)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	void BeginSpawn()
	{
		StartCoroutine(this.SpawnEnemyShips());
		StartCoroutine(this.SpawnAsteroids());
	}

	IEnumerator SpawnEnemyShips()
	{
		while (true)
		{
			this.fixedPathEnemies = new FixedPathEnemies(this.Boundary, this.EnemyShip, SpawnValues.x, -SpawnValues.x, 2, 14);
			this.fixedPathEnemies2 = new FixedPathEnemies(this.Boundary, this.EnemyShip, SpawnValues.x, -SpawnValues.x, 2, 14);

			for (var i = 0; i < this.enemiesPerWave; i++)
			{
				this.fixedPathEnemies.Spawn();
				var secondsBetweenEnemies = this.fixedPathEnemies2.Spawn();
				yield return new WaitForSeconds(secondsBetweenEnemies);
			}

			if (this.GameOver)
			{
				this.HandleGameOver();
				break;
			}

			yield return new WaitForSeconds(this.WaveWait);
		}
	}

	IEnumerator SpawnAsteroids()
	{
		while (true)
		{
			for (var i = 0; i < HazardCount; i++)
			{
				yield return this.InstantiateAsteroids();
			}

			if (this.GameOver)
			{
				this.HandleGameOver();
				break;
			}

			yield return new WaitForSeconds(this.WaveWait + 30);
		}
	}

	private object InstantiateAsteroids()
	{
		var spawnPostion = new Vector3(Random.Range(-this.SpawnValues.x, this.SpawnValues.x), this.SpawnValues.y, this.SpawnValues.z);
		var spawnRotation = Quaternion.identity;
		Instantiate(this.Hazard, spawnPostion, spawnRotation);
		return new WaitForSeconds(this.SpawnWait);
	}

	void HandleGameOver()
	{
		this.RestartText.text = "Press 'R' for Restart";
		this.Restart = true;
	}

	void UpdateScore()
	{
		ScoreText.text = "Score: " + this.score;
	}

	public void SetGameOver()
	{
		this.GameOver = true;

		var text = string.Empty;

		if (this.score <= 1000)
		{
			text = "NOT Awesome";
		}
		else if (this.score > 1000 && this.score <= 5000)
		{
			text = "Awesome";
		}
		else if (this.score > 5000)
		{
			text = "You are as good as Edmond!";
		}

		this.GameOverText.text = text;
	}

	public void AddScore(int newScoreValue)
	{
		this.score += newScoreValue;
		this.UpdateScore();
	}
}