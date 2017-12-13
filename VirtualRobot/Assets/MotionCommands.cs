using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionCommands : MonoBehaviour {
	Stack<string> path;
	bool told;
	bool move;
	// Use this for initialization
	void Start () {
		told = true;
		path = new Stack<string> ();
		//move = false;
	}

	void setToZeroRot(){
		Vector3 curAngle = transform.rotation.eulerAngles;

		turn (-curAngle.y);
	}

	public void startMotion(){
		move = false;
		setToZeroRot ();
		Vector3 target = new Vector3 (GlobalUtil.tX, 0.0f, GlobalUtil.tZ);//GlobalUtil.to3d ();
		Vector3 start = transform.position;
		print (start);
		float yAngle = transform.rotation.eulerAngles.y;
		path = findpath (transform.position, target, 0);
		move = true;
		told = false;
		if (path.Count < 1) {
			print ("PATH NOT FOUND");
			return;
		}
	}

	// Update is called once per frame
	void Update () {
		if (path.Count > 0 && move) {	
			StartCoroutine (moveAndWait ());
		} 
		else if(!told){
			Vector3 end = transform.position;
			print (end);
			told = true;
		}
	}

	IEnumerator moveAndWait(){
		step ();
		yield return new WaitForSeconds(2);
	}

	void turn(float theta){//left -90 degree, right +90 degree (90 deg = 1.57 rad)
		transform.Rotate (0, theta, 0);
	}

	void goAhead(){		
		transform.Translate (0.0f, 0.0f, 0.1f);
	}


	Stack<string> findpath(Vector3 a, Vector3 t, float yAngle){
		return Search.doSearch (a, t, yAngle);
	}

	void step(){
		if (path.Count > 0) {
			string m = path.Pop ();
			follow (m);
		}
	}

	void follow(string item){
		print (item);
				switch (item) {

				case "G":
					goAhead ();
					break;
				
				case "E":
					//for animation's sake;
					turn(45);
					goAhead ();
					turn(-45);

					break;

				case "W":
					turn (-45);
					goAhead ();
					turn (45);
					break;
				
				case "R":
					turn (90);
					goAhead ();
					break;

				case "L":
					turn (-90);
					goAhead ();
					break;

				}
		
					
			}
		

}
