using UnityEngine;
using System.Collections;
using DG.Tweening;
public class BowControlMouse : MonoBehaviour {
	//inside class
	public Vector3 firstPressPos;
	public Vector3 secondPressPos;
	public Vector2 drawbackPosition;
	public Vector2 drawbackResetPosition;
	public Vector2 centerPos;
	public Vector2 testpos;

	public float amountToRotate;
	public float TESTNUMBER;
	public float swipePower;
	public float reloadTime;
	private float lastInteraction;
	public float testx = 1;
	public float testy = 1;

	//Bow string
	public LineRenderer bowString;

	[Header("Objects")]
	public GameObject drawbackIndicator;
	public GameObject drawresetIndicator;
	public GameObject centerPosIndicator;
	public GameObject SecondPressIndicator;
	public GameObject rightAnchor; 
	public GameObject leftAnchor;
	public GameObject bowStringAnimationObj;
	public GameObject bow;
	public GameObject arrow;
	public GameObject dummyArrow;
	public GameObject dummyArrowAnimation;
	public GameObject BowStringObject;
	public GameObject StringAnimation;
	public GameObject TESTPosition;
	[Space]
	[Header("Bools")]
	public bool bowisLocked;
	public bool canFire;
	public bool reloaded;
	public bool overStretched;

	public Rigidbody2D arrowRb;


	// Use this for initialization
	void Start () {

		centerPos = new Vector2(0, -2.5f);
		secondPressPos = new Vector2(0, -2.5f);
		bowString = BowStringObject.GetComponent<LineRenderer> ();
		drawbackResetPosition = new Vector2(0, -3.13f);
	//bowString.SetPosition(1, new Vector2(0,10f));
		//***bowStringAnimationObj = GameObject.FindGameObjectWithTag("String Animation");
		canFire = false;
		StringAnimation.SetActive (false);
		bowString.enabled = true;
		reloaded = true;
		bowisLocked = false;
		dummyArrowAnimation.SetActive(false);
		reloadTime = 0.5f;
	}



	
	// Update is called once per frame
	void Update () {
		

		TESTPosition.transform.position = testpos;
		if (Time.time - lastInteraction >= reloadTime) {
			reloaded = true;

		}

		//Move DummyArrow to follow drawback

		drawbackIndicator.transform.position = drawbackPosition;
		drawresetIndicator.transform.position = drawbackResetPosition;
		centerPosIndicator.transform.localPosition = centerPos;
		SecondPressIndicator.transform.position = secondPressPos;

		//When Clicked
		if(Input.GetMouseButtonDown(0))
		{
			
			if (reloaded) {
				lastInteraction = Time.time;
				//firstPressPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				StringAnimation.SetActive (false);
				bowString.enabled = true;
				dummyArrowAnimation.SetActive (false);
				dummyArrow.SetActive (true);
				drawbackPosition = drawbackResetPosition;
				dummyArrow.transform.position = drawbackPosition;

			} else {
				print ("Not reloaded");
			}



			//save began touch 2d point
			//firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);


			//Debug.Log ("started");


		}

		//When mouse released fire shot
		if(Input.GetMouseButtonUp(0))
		{
			//When Mouse is released fire shot
			FireShot (swipePower);

		}


		//When user draws back too far fireshot automaticly
		if (swipePower >= 2.2) {
			FireShot (swipePower);

		}
		//If the user drags up instead of down disable canfire
		if (secondPressPos.y > drawbackResetPosition.y + 0.5) {

			canFire = false;
		}

			
		//WHEN MOUSE MOVES 

		if (Input.GetMouseButton (0)) {
				
			if (Input.GetAxis ("Mouse X") < 0 || Input.GetAxis ("Mouse X") > 0 || Input.GetAxis ("Mouse Y") < 0 || Input.GetAxis ("Mouse Y") > 0 ) {


				//Depending on if the game is being played with touch or mouse
				if (Input.touchCount > 0) {
					//make then end of the vector where the mouse is
					secondPressPos = Camera.main.ScreenToWorldPoint (Input.GetTouch(0).position);
				} else {
					//make then end of the vector where the mouse is
					secondPressPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				}

				//drawbackposition should be where the finger is
				if (canFire && secondPressPos.y < -3.13f) {
					amountToRotate = (Mathf.Rad2Deg * Mathf.Atan ((secondPressPos.y - centerPos.y) / (secondPressPos.x - centerPos.x)));
				}



				//THIS MIGHT CAUSE TROUBLE \/ IF SCALE CHANGES!
				if (secondPressPos.y < -3.13f) {
					if (amountToRotate > -90 && amountToRotate < -45 && canFire && reloaded) {


						drawbackPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						bow.transform.eulerAngles = new Vector3 (0, 0, amountToRotate + 90);

					} else if (amountToRotate < 90 && amountToRotate > 45 && canFire && reloaded) {
						drawbackPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						bow.transform.eulerAngles = new Vector3 (0, 0, amountToRotate - 90);

					} else if (amountToRotate < 45 && amountToRotate > 0 && canFire && reloaded) {
						//Make drawback stop at diagonal
						Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

						drawbackPosition.y = mousePos.y;
						drawbackPosition.x = (drawbackPosition.y * 1) + 2.65f;

						print ("Bow locked");
					} else if (amountToRotate > -45 && amountToRotate < 0 && canFire && reloaded) {
						Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

						drawbackPosition.y = mousePos.y;
						drawbackPosition.x = (drawbackPosition.y * -1) - 2.65f;
					
						print ("Bow locked");

					}

					if (!overStretched) {
						dummyArrow.transform.position = drawbackPosition;

					}


				}
			

				//drawback reset position should be where the midpoint of the bow ends are
				drawbackResetPosition = (bowString.GetPosition (0) + bowString.GetPosition (2)) / 2;

				swipePower = (Vector3.Distance (centerPos, drawbackPosition));
				//normalize the 2d vector

			}


			//START HERE       <-------
			if (canFire && reloaded) {
				bowString.SetPosition (1, new Vector3 (dummyArrow.transform.position.x, dummyArrow.transform.position.y, 0.0f));
			}
			bowString.SetPosition (0, leftAnchor.transform.position);
			bowString.SetPosition (2, rightAnchor.transform.position);

			


		} else {
			canFire = false;
		}
		//create a variable - Stretch value - equal to the swipepower over 20
		float stretchvalue = swipePower /20;
		bow.transform.localScale = new Vector2 (1 - stretchvalue, 1 + stretchvalue*TESTNUMBER);

	}

	IEnumerator UnStretchBow(){

		while (true) {
			if (swipePower >= 0 && !canFire) {
				swipePower = swipePower - 1f;
			} else if (swipePower < 0) {
				swipePower = 0;
				StopCoroutine ("UnStretchBow");
			}

			yield return new WaitForSeconds (0.001f);
		}
	}

	void FireShot(float swipepower) {
		
		//Fire Shot
		if (canFire && reloaded && swipePower > 0.01f) {
			lastInteraction = Time.time;


			Instantiate (arrow, new Vector2 (transform.position.x, transform.position.y), Quaternion.identity);
			dummyArrow.SetActive(false);
			dummyArrowAnimation.SetActive(true);
			StringAnimation.SetActive (true);
			bowString.enabled = false;
			reloaded = false;
			secondPressPos = drawbackResetPosition;
			overStretched = false;
			StartCoroutine ("UnStretchBow");
		}

		canFire = false;


		//Reset String Position to midpoint of the two bowstring points
		bowString.SetPosition (1, drawbackResetPosition);

		//Reset drawback position


	}


}
