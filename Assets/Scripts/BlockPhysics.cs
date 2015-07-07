using UnityEngine;
using System.Collections;

public class BlockPhysics : MonoBehaviour {
	
	public GameObject attractedTo;
	public float strengthOfAttraction = 5.0f;
	public float distanceOfAttraction = 10.0f;
	public GameObject explosion;

	private bool canBeAttracted = false;

	//souds
	//public AudioClip hitSound;
	//private AudioSource source;
	//private float volLowRange = .5f;
	//private float volHighRange = 1.0f;
	//private float lowPitchRange = .75F;
	//private float highPitchRange = 1.5F;

	void Awake(){
		attractedTo = GameObject.Find ("Planet");

		//load audio source 
		//source = GetComponent<AudioSource>();
	}

	void Update() {
		Vector3 direction = (attractedTo.transform.position - transform.position);
		if (direction.magnitude < distanceOfAttraction && canBeAttracted ) {
			Vector3 normalDirection = direction.normalized;
			GetComponent<Rigidbody> ().AddForce (strengthOfAttraction * normalDirection);
		}
	}

	void OnCollisionEnter(Collision other) 
	{
		if (other.transform.tag == "Planet") {
			ExplosionManager.S.PlaySoundExplosion ();
			
			//destroy self in a big explosion
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}

		if (other.transform.tag == "Projectile" || other.transform.tag == "Enemy") {
			canBeAttracted  = true;
		}


	}
}