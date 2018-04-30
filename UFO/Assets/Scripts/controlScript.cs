using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlScript : MonoBehaviour {

	AudioSource audio;
	public AudioClip[] clips;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		StartCoroutine (introJingle ());
	}

	private void playSound(int sound){
		audio.clip = clips [sound];
		audio.Play ();
	}

	private IEnumerator introJingle(){
		yield return new WaitForSeconds (.5f);
		playSound (0);
		StartCoroutine (AHH ());

	}

	private IEnumerator AHH(){
		yield return new WaitForSeconds (2.45f);
		playSound (1);

	}

	public void ChangeScene(){
		SceneManager.LoadScene ("main");
	}
}
