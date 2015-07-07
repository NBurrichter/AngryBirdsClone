using UnityEngine;
using System.Collections;

public class ProjectilePhysics : MonoBehaviour {

	public GameObject attractedTo;
	public float strengthOfAttraction = 5.0f;
	public float distanceOfAttraction = 10.0f;

	private bool collided;

	public float fadeTimer = 0.1f;

	//souds
	public AudioClip hitSound;
	public AudioClip shipHitSound;
	private AudioSource source;
	private float volLowRange = .5f;
	private float volHighRange = 1.0f;
	private float lowPitchRange = .75F;
	private float highPitchRange = 1.5F;

	void Awake(){
		attractedTo = GameObject.Find ("Planet");
		collided = false;

		//load audio source 
		source = GetComponent<AudioSource>();
	}

	void Update() {
		Vector3 direction = (attractedTo.transform.position - transform.position);
		if(!collided && direction.magnitude < distanceOfAttraction) {
			Vector3 normalDirection = direction.normalized;
			GetComponent<Rigidbody> ().AddForce (strengthOfAttraction * normalDirection);
		}

		fadeTimer -= 1 * Time.deltaTime;
		if (fadeTimer < 0 && GetComponent<MeshRenderer> ().enabled == false) {
			GetComponent<MeshRenderer> ().enabled = true;
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.transform.tag == "Planet") {
			GetComponent<Rigidbody>().Sleep();
			collided = true;

			//Play spund when crashing into planet
			float vol = Random.Range (volLowRange, volHighRange);
			source.pitch = Random.Range (lowPitchRange,highPitchRange);
			source.PlayOneShot(hitSound,vol);
		}
		if (col.transform.tag == "Enemy") {
			//Play spund when crashing into spaceship
			float vol = Random.Range (volLowRange, volHighRange);
			source.pitch = Random.Range (lowPitchRange,highPitchRange);
			source.PlayOneShot(shipHitSound,vol);
		}
	}

	void OnCollisionExit(Collision col){
			collided = false;
	}
}
