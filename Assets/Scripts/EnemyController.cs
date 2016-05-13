using Assets.EnemyFlightPaths;

using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	public GameObject Shot;

	public Transform ShotSpawn1, ShotSpawn2;

	private float nextGroupFire;

	private IFlightPath flightPath;

	private float fireRate;

	private int shotsBeforeCoolDown, coolDownSeconds;

	void Update()
	{
		if (Time.time > nextGroupFire)
		{
			nextGroupFire = Time.time + this.coolDownSeconds;
			StartCoroutine(this.GroupFire());
		}
		this.flightPath.Execute(rigidbody);
	}

	IEnumerator GroupFire()
	{
		for (var i = 0; i < shotsBeforeCoolDown; i++)
		{
			Instantiate(Shot, ShotSpawn1.position, ShotSpawn1.rotation);
			Instantiate(Shot, ShotSpawn2.position, ShotSpawn2.rotation);
			audio.Play();
			yield return new WaitForSeconds(this.fireRate);
		}
	}

	public void Initialize(IFlightPath flightPath, float fireRate, int coolDownSeconds, int shotsBetweenCoolDown)
	{
		this.flightPath = flightPath;
		this.fireRate = fireRate;
		this.coolDownSeconds = coolDownSeconds;
		this.shotsBeforeCoolDown = shotsBetweenCoolDown;
	}
}