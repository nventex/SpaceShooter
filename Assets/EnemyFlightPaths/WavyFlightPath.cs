namespace Assets.EnemyFlightPaths
{
	using UnityEngine;

	public class WavyFlightPath : IFlightPath
	{
		private readonly Rigidbody rigidbody;

		private Vector3 velocity;

		private readonly float velocityX;

		private readonly float velocityZ;

		public WavyFlightPath(Rigidbody rigidbody, float velocityX, float velocityZ)
		{
			this.rigidbody = rigidbody;
			this.velocityX = velocityX;
			this.velocityZ = velocityZ;
			this.velocity = new Vector3(0, 0, 0);
		}
		
		public void Execute(Rigidbody rigidbody)
		{
			this.velocity.x = Mathf.Sin(Time.time * velocityX) + velocityX;
			this.velocity.z = velocityZ;

			rigidbody.velocity = velocity;
		}

		public int PathId
		{
			get
			{
				return 1;
			}
		} 
	}
}
