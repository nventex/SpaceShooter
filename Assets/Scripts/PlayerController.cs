using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public float Speed;

	public float Tilt;
	public Boundary Boundary;

	public GameObject Shot;
	public Transform ShotSpawn;

	public float FireRate;

	private float nextFire;

	void FixedUpdate()
	{
		var moveHorizontal = Input.GetAxis("Horizontal");
		var moveVertical = Input.GetAxis("Vertical");

		var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		rigidbody.velocity = movement * this.Speed;

		rigidbody.position = new Vector3
			(
				Mathf.Clamp(rigidbody.position.x, Boundary.xMin, Boundary.xMax), 
				0.0f,
				Mathf.Clamp(rigidbody.position.z, Boundary.zMin, Boundary.zMax)
			);

		rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -this.Tilt);
	}

	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			this.nextFire = Time.time + this.FireRate;
			Instantiate(Shot, ShotSpawn.position, ShotSpawn.rotation);
			audio.Play();
		}
	}
}