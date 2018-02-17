using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class BlipSpawnScript : MonoBehaviour {
	public GameObject[] blips;
	public GameObject mainMenuPannel;

	public float delay;
	public int leftOrRight;
	public int score;

	public Vector3 otherposition;
	public GameObject otherpositionobj;

	public Text scoreLabel;


	public Animator mainMenuanm;
	public Animator scoreanm;

	public bool GamePlaying;
	public bool ReadyToStart;
	public bool GameRestarting;


	// Use this for initialization
	void Start () {
		
		ReadyToStart = false;
		GamePlaying = false;
		mainMenuanm = mainMenuPannel.GetComponent<Animator> ();
		//***mainMenuanm.Play ("Main Menu Drop Down");
		delay = 2;
		scoreanm = scoreLabel.GetComponent<Animator> ();
		scoreanm.Play ("Score Stay Down");
		scoreLabel.transform.position = new Vector2 (0, -500);
		scoreLabel.fontSize = 130;


		otherposition = otherpositionobj.transform.position;

	}

	void FixedUpdate () {

				//Slowly Increase the rate of spawning
				if (delay < 1) {
				delay = delay - 0.0001f;
				}
				//Set the scorelabel to the score variable
				scoreLabel.text = score.ToString ();
				
				//Reduce font size when score gets too high
				if (scoreLabel.text.Length == 3) {
				scoreLabel.fontSize = 120;

				}
				
				if (ReadyToStart && !GamePlaying) {
				StartCoroutine ("SpawnApple");
				GamePlaying = true;
				}


	}


	//Official Target spawning function
	IEnumerator SpawnApple(){
		while (true) {
			print ("Spawn Apple Coroutine is running");
				yield return new WaitForSeconds (Random.Range (delay - 0.1f, delay + 0.1f));

				leftOrRight = Random.Range (0, 2);
				if (leftOrRight == 0) {
				Instantiate (blips [Random.Range(0,blips.Length)], new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
				print ("Spawned Thing");
				}
				if (leftOrRight == 1) {
				Instantiate (blips [Random.Range(0,blips.Length)], new Vector3 (otherposition.x, transform.position.y, transform.position.z), Quaternion.identity);
				print ("Spawned Thing");

				}

		}

	}
	// Update is called once per frame

}
