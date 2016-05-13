using UnityEngine;

public class RandomRotator : MonoBehaviour
{
	public float Tumble;

	void Start()
	{
		rigidbody.angularVelocity = Random.insideUnitSphere * this.Tumble;
	}
}
