using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public GameObject explosion;
	public float shakeMagnitudePlanet = 10.0f;
	public float shakeTimePlanet = 0.75f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other) 
	{
		if (other.transform.tag == "Projectile") {
			//create explosion
			ExplosionManager.S.PlaySoundExplosion ();
			
			//Screenshake
			FollowCam.S.ShakeScreen(shakeTimePlanet,shakeMagnitudePlanet);
			
			//destroy self in a big explosion
			//Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
		
	}
}
