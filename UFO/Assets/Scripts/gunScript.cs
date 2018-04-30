using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour {
	AudioSource audio;
	public static gunScript instance;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		audio = GetComponent <AudioSource> ();
	}

	public void fireSound (){
		audio.Play ();
	}

}