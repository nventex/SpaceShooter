namespace Assets.EnemyFlightPaths
{
	using UnityEngine;

	public interface IFlightPath
	{
		void Execute(Rigidbody rigidbody);

		int PathId { get; }
	}
}