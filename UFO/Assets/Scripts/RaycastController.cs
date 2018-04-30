using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour {

	public float maxDistanceRay = 100f;
	public static RaycastController instance;
	//public Text ufoName;
	public Transform gunFlashTarget;
	public float fireRate = 1f;
	private bool nextShot = true;
	private string objName = "";

	AudioSource audio;
	public AudioClip[] clips;

	void Awake (){
		if (instance == null) {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (spawnNewUfo ());
		audio = GetComponent <AudioSource> ();

	}


	public void playSound(int sound){
		audio.clip = clips[sound];
		audio.Play ();
	}



	// Update is called once per frame
	void Update () {

	}

	public void Fire(){
		if (nextShot) {
			StartCoroutine (takeShot ());
			nextShot = false;
		}
	}

	private IEnumerator takeShot(){

		gunScript.instance.fireSound ();

		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
		RaycastHit hit;

		gameController.instance.shotsPerRound--;

		int layer_mask = LayerMask.GetMask ("ufoLayer");
		if (Physics.Raycast (ray, out hit, maxDistanceRay, layer_mask)) {


			objName = hit.collider.gameObject.name;
			//Debug		//ufoName.text = objName;
			Vector3 ufoPosition = hit.collider.gameObject.transform.position;

			if(objName == "UFO(Clone)"){
				GameObject Boom = Instantiate (Resources.Load ("boom", typeof(GameObject))) as GameObject;
				Boom.transform.position = ufoPosition;

				playSound (1); //UFO hit

				Destroy (hit.collider.gameObject);
				StartCoroutine(spawnNewUfo ());
				StartCoroutine (clearBoom ());
				gameController.instance.shotsPerRound = 3;
				gameController.instance.playerScore++;
				gameController.instance.roundScore++;

			}
		}

		GameObject gunFlash = Instantiate (Resources.Load ("gunFlashSmoke", typeof(GameObject))) as GameObject;
		gunFlash.transform.position = gunFlashTarget.position;

		yield return new WaitForSeconds (fireRate);

		nextShot = true;
		GameObject[] gunSmokeGroup = GameObject.FindGameObjectsWithTag ("GunSmoke");
		foreach (GameObject theSmoke in gunSmokeGroup) {
			Destroy (theSmoke.gameObject);
		}

	}

	private IEnumerator clearBoom(){
		yield return new WaitForSeconds (1f);

		GameObject[] smokeGroup = GameObject.FindGameObjectsWithTag ("Boom");
		foreach (GameObject smoke in smokeGroup) {
			Destroy (smoke.gameObject);
		}
	}

	private IEnumerator spawnNewUfo(){
		yield return new WaitForSeconds (3f);

		//Spawns newUfo
		GameObject newUfo = Instantiate(Resources.Load("UFO", typeof(GameObject))) as GameObject;

		// Make UFO Child of ImageTarget 
		newUfo.transform.parent = GameObject.Find("ImageTarget").transform;

		//Scale of UFO
		newUfo.transform.localScale = new Vector3(10f,10f,10f);

		//Random Start Postion
		Vector3 temp;
		temp.x = Random.Range (-50f, 50f);
		temp.y = Random.Range (5f, 40f);
		temp.z = Random.Range (-50f, 50f);
		newUfo.transform.position = new Vector3 (temp.x, temp.y, temp.z);
	}


}
