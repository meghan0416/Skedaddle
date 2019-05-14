using UnityEngine;
using System.Collections.Generic;

public class ObstacleGenerator : MonoBehaviour {

	// prefabs of obstacles
	public Transform prefab_cake;
	public Transform prefab_banner;
	public Transform prefab_spikes;


	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;
	public Vector3 minGap, maxGap;
	public float minX, maxX;


	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;
	private Transform obj;
	private int selection;

	void Start () {
		// create a queue of obstacles with a random combination
		objectQueue = new Queue<Transform>(numberOfObjects);
		for(int i = 0; i < numberOfObjects; i++){
			selection = Random.Range (0, 3);
			switch (selection) {
				case 0: 
					obj = prefab_cake;
					break;
				case 1:
					obj = prefab_banner;
					break;
				case 2:
					obj = prefab_spikes;
					break;
			}
			objectQueue.Enqueue((Transform)Instantiate(obj));
		}
		// create all the obstacles
		nextPosition = startPosition;
		for(int i = 0; i < numberOfObjects; i++){
			Recycle();
		}
	}

	void Update () {
		// if the character travels far enough, recycle the obstacles to the front
		if(objectQueue.Peek().localPosition.z + recycleOffset < MoveCharacter.distanceTraveled){
			Recycle();
		}
	}


	// makes infinite obstacles
	private void Recycle () {



		Vector3 position = nextPosition;


		Transform o = objectQueue.Dequeue();


		// moves / corrects objects that have weird pivot points
		if (o.gameObject == GameObject.Find("FinalBannerPrefab(Clone)")) {
			position += new Vector3 (-5.5f, 0, 0);
	
		} else if (o.gameObject == GameObject.Find("FinalPartyHatsPrefab(Clone)")) {
			position += new Vector3 (10f, 0, 0);

		}


		// assigns the new position for the object and adds it back to the queue
		o.localPosition = position;
		objectQueue.Enqueue(o);



		// find the next position within the xmin and xmax
		nextPosition += new Vector3(
			Random.Range(minGap.x, maxGap.x),
			0,
			Random.Range(minGap.z, maxGap.z));

		if(nextPosition.x < minX){
			nextPosition.x = minX + maxGap.x;
		}
		else if(nextPosition.x > maxX){
			nextPosition.x = maxX - maxGap.x;
		}
			
	}
}
