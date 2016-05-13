namespace Assets.Factories
{
	using UnityEngine;

	public class GameObjectFactory
	{
		public GameObject GameObjectInstance { get; private set; }

		public T Create<T>(Object gameObject, Vector3 vector3, Quaternion quaternion) where T : Component
		{
			this.GameObjectInstance = Object.Instantiate(gameObject, vector3, quaternion) as GameObject;

			var o = this.GameObjectInstance;
			
			var component = o.GetComponent<T>();

			return component;
		}
	}
}