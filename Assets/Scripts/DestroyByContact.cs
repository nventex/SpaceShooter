using UnityEngine;
using System.Linq;

public class DestroyByContact : MonoBehaviour
{
	public GameObject Explosion;
	public GameObject PlayerExplosion;

	public int ScoreValue;

	private GameController gameController;

	void Start()
	{
		var gameControllerObject = GameObject.FindWithTag("GameController");
		
		if (gameControllerObject != null)
		{
			this.gameController = gameControllerObject.GetComponent<GameController>();
		}

		if (this.gameController == null)
		{
			Debug.Log("Cannot find 'GameController' script");
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		var hazardsForPlayer = new[] { "EnemyBolt", "Asteroid", "Enemy" };
		var playerBoltDestroys = new[] { "Asteroid", "Enemy" };

		if ((hazardsForPlayer.Contains(tag) && other.tag.Equals("Player")) ||
			(hazardsForPlayer.Contains(other.tag) && tag.Equals("Player")))
		{
			Instantiate(PlayerExplosion, transform.position, transform.rotation);
			Destroy(other.gameObject);
			Destroy(gameObject);
			this.gameController.AddScore(this.ScoreValue);
			this.gameController.SetGameOver();
		}
		else if ((playerBoltDestroys.Contains(tag) && other.tag.Equals("Bolt")) ||
			(playerBoltDestroys.Contains(other.tag) && tag.Equals("Bolt")))
		{
			Instantiate(Explosion, transform.position, transform.rotation);
			Destroy(other.gameObject);
			Destroy(gameObject);
			this.gameController.AddScore(this.ScoreValue);
		}
	}
}