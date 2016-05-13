using UnityEngine;

public class Mover : MonoBehaviour
{
	public float Speed;
	
	void Start()
	{
		rigidbody.velocity = transform.forward * this.Speed;
	}
}