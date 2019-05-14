using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrowd : MonoBehaviour {
	//public static float currentZ;

	public float speed;

	private bool touchingPlatform;

	new Rigidbody rigidbody;



	void Update () {
		rigidbody = GetComponent<Rigidbody> ();
		//currentZ = transform.localPosition.z;
		// updates speed to keep up with character
		if (MoveCharacter.distanceTraveled < MoveCharacter.mediumDistance) {
			rigidbody.velocity = new Vector3 (0, 0, speed);
		} else if (MoveCharacter.distanceTraveled < MoveCharacter.hardDistance) {
			rigidbody.velocity = new Vector3 (0, 0, speed + 5);
		} else if (MoveCharacter.distanceTraveled >= MoveCharacter.hardDistance) {
			rigidbody.velocity = new Vector3 (0, 0, speed + 10);
		}
	}
		
}