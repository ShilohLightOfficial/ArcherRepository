using UnityEngine;
using System.Collections;
using DG.Tweening;
public class appleScript : MonoBehaviour {
	public Rigidbody2D rb;
	public BlipSpawnScript spawnerScript;
	public GameObject spawner;
	public GameObject coin;
	public AudioSource increasesound;
	//To check wether this specific apple has already given a point (avoid double points)
	public bool alreadyEarned = false;
	// Use this for initialization
	void Start () {
		spawner = GameObject.FindGameObjectWithTag ("Spawner");
		spawnerScript = spawner.GetComponent<BlipSpawnScript> ();
		rb = this.GetComponent<Rigidbody2D> ();
		if (this.transform.position.x > 0) {
			rb.AddForce (new Vector2 (Random.Range (-4, -6), Random.Range (5, 7)), ForceMode2D.Impulse);
		} else if(this.transform.position.x < 0){
			rb.AddForce (new Vector2 (Random.Range (4, 6), Random.Range (5, 7)), ForceMode2D.Impulse);
		}
		rb.AddTorque (Random.Range(-20,20));

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D coll) {
		//Once the arrow collides and it hasent already collided
		if (coll.gameObject.tag == "Arrow" && !alreadyEarned){
			alreadyEarned = true;
			spawnerScript.score = spawnerScript.score + 1;
			increasesound.Play ();
			Instantiate (coin, transform.position, transform.rotation);
			spawnerScript.scoreanm.Play ("Score Increase");
			rb.AddForce (new Vector2 (Random.Range (-1, 1), Random.Range (20, 50)), ForceMode2D.Impulse);
		}

	}
}
