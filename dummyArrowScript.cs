using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyArrowScript : MonoBehaviour {
	public BowControlMouse bowscript;
	// Use this for initialization
	void Start () {
		bowscript = GameObject.FindGameObjectWithTag ("Bow").GetComponent<BowControlMouse> ();
	}
	
	// Update is called once per frame
	void Update () {
		

		if (this.transform.localPosition.y <= -1.55f) {
			this.transform.localPosition = new Vector2 (0, -1.55f);
			bowscript.overStretched = true;
		} else {
			bowscript.overStretched = false;

		}
			
	}
}
