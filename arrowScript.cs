using UnityEngine;
using System.Collections;

public class arrowScript : MonoBehaviour {
	public Rigidbody2D rb;
	public GameObject bow;
	public float finalForce;
	public BowControlMouse bowscript;
	// Use this for initialization
	void Start () {
		bow = GameObject.FindGameObjectWithTag ("Bow");
		bowscript = bow.GetComponent<BowControlMouse>();
		//Set rotation to be that of the bow
		transform.rotation = bow.transform.rotation;
		rb = this.GetComponent<Rigidbody2D> ();

		//Get the swipepower from the other script


		//Add the force of the shot

		finalForce = (bowscript.swipePower * 400);
		//Make sure the shot power isnt higher than 600

		if (finalForce > 680){

			finalForce = 680;
		}


		//Add the force of the shot
		print("Fired Shot");
		rb.AddRelativeForce (new Vector2(0,finalForce), ForceMode2D.Impulse);
		bowscript.drawbackPosition = bowscript.drawbackResetPosition;

		StartCoroutine ("Destroy");
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter2D(Collision2D coll) {
		//print ("Hit something");
		//this.GetComponent<Animator> ().Play ("Arrow Fade Animation (Optional)");
	}
	IEnumerator Destroy(){
		yield return new WaitForSeconds (2);
		Destroy (this.gameObject);

	}
}
