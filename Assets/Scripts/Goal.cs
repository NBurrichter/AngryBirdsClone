using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	//physics
	public GameObject attractedTo;
	public float strengthOfAttraction = 5.0f;
	public float distanceOfAttraction = 10.0f;
	public bool canBeAttracted;

	//next level variables
	public static bool goalMet;
	public GameObject explosion;
	public string nextLevel;

	public float timer = 100.0f;

	void Start(){
		goalMet = false;
	}

	void OnTriggerEnter(Collider other) {
		//check if the object is a projectile
		if (other.transform.tag == "Planet") {
			GetComponent<Rigidbody> ().Sleep ();
		}
		
		if (other.tag == "Projectile") {

			//If so set goalMet to true
			goalMet = true;	

			//Also set the goals alpha to a higher opacity
			Color c = this.GetComponent<Renderer>().material.color;

			c.a = 0;

			this.GetComponent<Renderer>().material.color = c;

			Debug.Log("JA");
			Instantiate(explosion, transform.position, transform.rotation);

		}
	}

	void Update(){
		Vector3 direction = (attractedTo.transform.position - transform.position);
		if (direction.magnitude < distanceOfAttraction && canBeAttracted ) {
			Vector3 normalDirection = direction.normalized;
			GetComponent<Rigidbody> ().AddForce (strengthOfAttraction * normalDirection);
		}
	}

	void FixedUpdate(){
	if (goalMet == true) {
			timer -= 1;
			if (timer <= 0){
				Application.LoadLevel (nextLevel);
			}
		}
	}

	void OnCollisionEnter(Collision other) 
	{		
		if (other.transform.tag == "Projectile" || other.transform.tag == "Enemy") {			
			canBeAttracted  = true;
		}
		
		
	}
}
