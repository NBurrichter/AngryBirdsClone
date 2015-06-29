using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public static bool goalMet;
	public GameObject explosion;
	public string nextLevel;

	public float timer = 100.0f;

	void Start(){
		goalMet = false;
	}

	void OnTriggerEnter(Collider other) {
		//check if the object is a projectile
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

	void FixedUpdate(){
	if (goalMet == true) {
			timer -= 1;
			if (timer <= 0){
				Application.LoadLevel (nextLevel);
			}
		}
	}
}
