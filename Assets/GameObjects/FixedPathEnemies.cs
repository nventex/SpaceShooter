namespace Assets.GameObjects
{
	using System.Collections.Generic;

	using Assets.EnemyFlightPaths;
	using Assets.Factories;

	using UnityEngine;

	public class FixedPathEnemies
	{
		private Dictionary<int, IFlightPath> flightPathCatalog = new Dictionary<int, IFlightPath>();

		private readonly float fireRate;

		private readonly int coolDownSeconds;

		private readonly int shotsBetweenCoolDown;

		private readonly int enemiesPerWave;

		private readonly float secondsBetweenEnemies;

		private readonly float secondsBetweenWaves;

		private float maxX, minX, maxZ, minZ;

		private readonly float spawnX;

		private float spawnZ;

		private readonly GameObjectFactory gameObjectFactory;

		private readonly GameObject enemyShip;

		private readonly Vector3 spawnPosition;

		private readonly int flightId;

		public FixedPathEnemies(Transform boundary, GameObject enemyShip, float maxX, float minX, float maxZ, float minZ)
		{
			this.enemyShip = enemyShip;
			
			// Generate random values to be applied
			var randomDirectionX = Random.Range(0, 2) * 2 - 1;
			var randomVelocityX = Random.Range(2f, 8f) * randomDirectionX;
			var randomVelocityZ = Random.Range(2f, 8f) * -1;
			var maxDistancePercent = Random.Range(0.1f, 0.8f);

			this.fireRate = Random.Range(0.25f, 1.0f);
			this.coolDownSeconds = Random.Range(2, 10);
			this.shotsBetweenCoolDown = Random.Range(2, 6);
			this.secondsBetweenEnemies = Random.Range(0.5f, 3.0f);

			this.spawnX = Random.Range(minX, maxX);
			this.spawnZ = Random.Range(minZ, maxZ);

			this.spawnPosition = new Vector3(this.spawnX, 0, 16);

			var rightAngle = new RightAnglePath(boundary, this.spawnPosition, randomVelocityX, randomVelocityZ, maxDistancePercent);
			var wavy = new WavyFlightPath(enemyShip.rigidbody, randomVelocityX, randomVelocityZ);

			this.gameObjectFactory = new GameObjectFactory();

			Debug.Log("---------ctor values---------");
			Debug.Log(string.Format("randomVelocityX={0}, randomVelocityZ={1}, maxDistancePercent={2}, fireRate={3}, coolDownSeconds={4}, shotsBetweenCoolDown={5}, secondsBetweenEnemies={6}, spawnX={7}, spawnZ={8}",
				randomVelocityX, randomVelocityZ, maxDistancePercent, fireRate, coolDownSeconds, shotsBetweenCoolDown, secondsBetweenEnemies, spawnX, spawnZ));

			this.flightPathCatalog.Add(rightAngle.PathId, rightAngle);
			this.flightPathCatalog.Add(wavy.PathId, wavy);

			this.flightId = Random.Range(0, 2);

			Debug.Log("flightId=" + flightId);
		}

		public float Spawn()
		{
			var controller = this.gameObjectFactory.Create<EnemyController>(this.enemyShip, this.spawnPosition, this.enemyShip.transform.rotation);

			controller.Initialize(this.flightPathCatalog[this.flightId], this.fireRate, this.coolDownSeconds, this.shotsBetweenCoolDown);

			return this.secondsBetweenEnemies;
		}
	}
}