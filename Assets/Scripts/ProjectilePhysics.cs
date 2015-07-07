using UnityEngine;
using System.Collections;

public class ProjectilePhysics : MonoBehaviour {

	public GameObject attractedTo;
	public float strengthOfAttraction = 5.0f;
	public float distanceOfAttraction = 10.0f;

	private bool collided;

	void Awake(){
		attractedTo = GameObject.Find ("Planet");
		collided = false;
	}

	void Update() {
		Vector3 direction = (attractedTo.transform.position - transform.position);
		if(!collided && direction.magnitude < distanceOfAttraction) {
			Vector3 normalDirection = direction.normalized;
			GetComponent<Rigidbody> ().AddForce (strengthOfAttraction * normalDirection);
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.transform.tag == "Planet") {
			GetComponent<Rigidbody>().Sleep();
			collided = true;
		}
	}

	void OnCollisionExit(Collision col){
			collided = false;
	}
}
