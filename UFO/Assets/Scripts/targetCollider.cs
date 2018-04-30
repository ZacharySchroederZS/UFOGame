using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class targetCollider : DefaultTrackableEventHandler {

	public static targetCollider instance;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	void OnTriggerEnter(Collider other){
		moveTarget();
	}
	public void moveTarget(){
		Vector3 temp;
		temp.x = Random.Range (-50f, 50f);
		temp.y = Random.Range (5f, 40f);
		temp.z = Random.Range (-50f, 50f);
		transform.position = new Vector3 (temp.x, temp.y, temp.z);
		if (DefaultTrackableEventHandler.trueFalse == true) {
			RaycastController.instance.playSound (0);
		}
	}
}
