﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	void Update() {
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			gameObject.GetComponent<AudioSource> ().Play ();
		}
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Rigidbody rigidBody = gameObject.GetComponent<Rigidbody> ();

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidBody.velocity = movement * speed;

		rigidBody.position = new Vector3 (
			Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
		);

		rigidBody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidBody.velocity.x * -tilt);
	}
}
