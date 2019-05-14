using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCharacter : MonoBehaviour {
	public static float distanceTraveled;

	public float push;

	private bool touchingPlatform;

	public static float startDistance;

	public static float mediumDistance;

	public static float hardDistance;

	public static float winDistance;

	public static float currentVelocity;

	public static float currentZ;

	new Rigidbody rigidbody;

	public Camera vrcam;

	public AudioClip jump;

	private AudioSource source;

	private float vol = 1.0f;




	void Start(){
		source = GetComponent<AudioSource> ();
		mediumDistance = 500;
		hardDistance = 100;
		winDistance = 1500;
		touchingPlatform = true;
		rigidbody = GetComponent<Rigidbody> ();
		startDistance = transform.localPosition.z; // in case we didn't start at zero
	}

	void Update () {
		

		rigidbody = GetComponent<Rigidbody> ();
		distanceTraveled = transform.localPosition.z + startDistance;
		currentVelocity = rigidbody.velocity.z;
		currentZ = rigidbody.position.z;
		// if we are not in the air and click the mouse, jump. we are no longer touching platform
		// jumping triggers the sound effect "jump"
		if(touchingPlatform && Input.GetMouseButton(0) && rigidbody.position.y < 10){
			rigidbody.AddForce(0,7,0, ForceMode.VelocityChange);
			source.PlayOneShot (jump, vol);
			touchingPlatform = false;
		}
		// if we rotate left or right, move left or right
		if (vrcam.transform.localRotation.z > 0 && rigidbody.position.x < 3) {
			rigidbody.position += new Vector3 (3, 0, 0);
		}
		if (vrcam.transform.localRotation.z <= 0 && rigidbody.position.x > -3) {
			rigidbody.position += new Vector3 (-3, 0, 0);
		}
		// if we pass the distance to win, go to win scene
		if (distanceTraveled >= winDistance) {
			print ("YOU WIN!");
			Debug.Log ("Loading win scene");
			SceneManager.LoadScene ("SkedaddleWin");
		}


	}

	void FixedUpdate () {

		// character gets progressively faster as you get closer to winning

		if (touchingPlatform && distanceTraveled < mediumDistance) {
			rigidbody.velocity = new Vector3 (0, 0, push);
		}

		else if (touchingPlatform && distanceTraveled < hardDistance) {
			rigidbody.velocity = new Vector3 (0, 0, push + 5);
		}
		else if (touchingPlatform && distanceTraveled >= hardDistance) {
			rigidbody.velocity = new Vector3 (0, 0, push + 10);
		}
	}

	void OnCollisionEnter () {
		// if we touch the ground or obstacle, we are touching the platform
		touchingPlatform = true;

	}


	void OnTriggerEnter(Collider other){
		// if we hit the crowd, you lose
		var crowd = GameObject.Find("Crowd");
		if (other.gameObject == crowd) {
			print ("GAME OVER");
			Debug.Log ("Loading lose scene");
			SceneManager.LoadScene ("SkedaddleLose");
		}

	}
		
}



