using UnityEngine;
using System.Collections.Generic;

public class GroundGenerator : MonoBehaviour {

	public Transform prefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;

	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;

	void Start () {
		// creates a queue of ground objects 250 units apart
		objectQueue = new Queue<Transform>(numberOfObjects);
		nextPosition = startPosition;
		for (int i = 0; i < numberOfObjects; i++) {
			Transform o = (Transform)Instantiate(prefab);
			o.localPosition = nextPosition;
			nextPosition.z += 250;
			objectQueue.Enqueue(o);
		}
	}

	void Update () {
		// recycles the generated ground objects
		if (objectQueue.Peek().localPosition.z + recycleOffset < MoveCharacter.distanceTraveled) {
			Transform o = objectQueue.Dequeue();
			o.localPosition = nextPosition;
			nextPosition.z += 250;
			objectQueue.Enqueue(o);
		}
	}
}