using UnityEngine;
using System.Collections;

public class BlockPhysics : MonoBehaviour {
	
	public GameObject attractedTo;
	public float strengthOfAttraction = 5.0f;
	public float distanceOfAttraction = 10.0f;
	public GameObject explosion;

	void Awake(){
		attractedTo = GameObject.Find ("Planet");
	}

	void Update() {
		Vector3 direction = (attractedTo.transform.position - transform.position);
		if (direction.magnitude < distanceOfAttraction) {
			Vector3 normalDirection = direction.normalized;
			GetComponent<Rigidbody> ().AddForce (strengthOfAttraction * normalDirection);
		}
	}

	void OnCollisionEnter(Collision other) 
	{
		if (other.transform.tag != "Planet")
		{
			return;
		}

		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}