namespace Assets.EnemyFlightPaths
{
	using UnityEngine;

	public class RightAnglePath : IFlightPath
	{
		private readonly Transform boundary;

		private readonly float velocityX;

		private readonly float velocityZ;

		private readonly float maxDistancePercent;

		private Vector3 velocity, spawnPosition;

		public RightAnglePath(Transform boundary, Vector3 spawnPosition, float velocityX, float velocityZ, float maxDistancePercent)
		{
			this.boundary = boundary;
			this.velocityX = velocityX;
			this.velocityZ = velocityZ;
			this.maxDistancePercent = maxDistancePercent;
			this.spawnPosition = spawnPosition;
			velocity = new Vector3(0, 0, 0);
		}

		public void Execute(Rigidbody rigidbody)
		{
			var distanceTravelled = (this.spawnPosition.z - Mathf.Abs(rigidbody.position.z)) / boundary.localScale.z;

			if (distanceTravelled > maxDistancePercent)
			{
				velocity.x = velocityX;
				velocity.z = 0;
			}
			else
			{
				velocity.x = 0;
				velocity.z = velocityZ;
			}

			rigidbody.velocity = velocity;
			rigidbody.rotation = Quaternion.Euler(0.0f, 180, rigidbody.velocity.x * -4);
		}

		public int PathId
		{
			get
			{
				return 0;
			}
		} 
	}
}
