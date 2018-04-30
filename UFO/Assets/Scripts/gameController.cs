using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.SceneManagement;

public class gameController : DefaultTrackableEventHandler {

	public static gameController instance;


	public int playerScore = 0;
	public Text scoreCountText;
	public Text livesCountText;
	public int round = 1;
	public Text roundTextTargetText;
	public Text roundTextNumber;
	public Text GameOverScoreText;
	public int shotsPerRound = 3;
	private int lives = 2;
	public GameObject[] shells;

	//Show Gameobjects
	public GameObject GUIScoreText;
	public GameObject GUILivesText;
	public GameObject GUICenterTarget;
	public GameObject GUIFireButton;
	public GameObject GUIRoundText;
	public GameObject GUIGameOverPanel;
	public GameObject startPanel;
	public GameObject Terrain;
	public GameObject GUIGun;

	AudioSource audio;
	public AudioClip[] clips;


	//Rules
	private int roundTargetScore = 3;
	public int roundScore = 0;
	private int scroeIncrement = 2;
	public bool playerStarted = false;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		playerScore = int.Parse (scoreCountText.text);
		showStartPanel ();
		audio = GetComponent <AudioSource> ();
		livesCountText.text = lives.ToString ();
	}


	private void playFX(int sound){
		audio.clip = clips[sound];
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {

		if (DefaultTrackableEventHandler.trueFalse == true) {
			hideStartPanel ();
			showItems ();
			if (playerStarted == false) {
				StartCoroutine (playRound ());

			}
			playerStarted = true;
		} 
		else {
			showStartPanel ();
			hideItems ();
		}
		if (roundScore == roundTargetScore) {
			playFX (0);
			StartCoroutine (newRound ());
			roundScore = 0;
			roundTargetScore = roundTargetScore + scroeIncrement;
		}

		//checking shots per round
		if(shotsPerRound == 0){
			shells [0].SetActive (false);
			StartCoroutine (loseLife ());
			shotsPerRound = 3;
		}
		//Shot score on screen
		scoreCountText.text = playerScore.ToString();
	}

	public void showItems(){
		GUIScoreText.SetActive(true);
		GUILivesText.SetActive(true);
		GUICenterTarget.SetActive(true);
		GUIFireButton.SetActive(true);
		//GUIRoundText.SetActive(true);
		Terrain.SetActive(true);
		GUIGun.SetActive (true);
		showShells ();
	}

	public void hideItems(){
		GUIScoreText.SetActive(false);
		GUILivesText.SetActive(false);
		GUICenterTarget.SetActive(false);
		GUIFireButton.SetActive(false);
		//GUIRoundText.SetActive(false);
		Terrain.SetActive(false);
		GUIGun.SetActive (false);
	}

	public IEnumerator playRound() {
		yield return new WaitForSeconds (0.5f);
		roundTextTargetText.text = "SHOOT "+roundTargetScore+" UFOS";
		GUIRoundText.SetActive(true); //I need to fix this or keep it as a feature
		playFX (0);
		StartCoroutine (hideRoundText ());
	}
	private IEnumerator newRound(){
		yield return new WaitForSeconds (1f);
		round++;
		GUIRoundText.SetActive (true);
		roundTextTargetText.text = "SHOOT "+roundTargetScore+" UFOS";
		roundTextNumber.text = round.ToString ();
		StartCoroutine (hideRoundText ());
	}

	private IEnumerator hideRoundText(){
		yield return new WaitForSeconds (3f);
		GUIRoundText.SetActive(false);
	}

	public void showShells(){
		if (shotsPerRound == 3) {
			shells [0].SetActive (true);
			shells [1].SetActive (true);
			shells [2].SetActive (true);
		}
		else if (shotsPerRound == 2) {
			shells [0].SetActive (true);
			shells [1].SetActive (true);
			shells [2].SetActive (false);
		} 
		else if (shotsPerRound == 1) {
			shells [0].SetActive (true);
			shells [1].SetActive (false);
			shells [2].SetActive (false);
		}
	}

	private void showStartPanel(){
		startPanel.SetActive (true);
	}

	private void hideStartPanel(){
		startPanel.SetActive (false);
	}

	private IEnumerator loseLife(){
		lives--;
		if (lives == 0) {
			GUIFireButton.SetActive (false);
			playFX (2);
			GUIGameOverPanel.SetActive (true);
			GameOverScoreText.text = playerScore.ToString ();
			lives = 0;
		}

		yield return new WaitForSeconds (0);
		livesCountText.text = lives.ToString ();
	}

	public void restart(){
		hideItems ();
		lives = 2;
		livesCountText.text = lives.ToString();
		playerScore = 0;
		scoreCountText.text = playerScore.ToString ();
		roundTargetScore = 3;
		roundScore = 0;
		GameOverScoreText.text = "0";
		round = 1;
		roundTextNumber.text = round.ToString();
		GUIGameOverPanel.SetActive (false);
		StartCoroutine (playRound ());

	}

	public void quit(){
		SceneManager.LoadScene ("intro");
	}


}
