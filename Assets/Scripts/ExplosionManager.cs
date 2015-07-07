using UnityEngine;
using System.Collections;

public class ExplosionManager : MonoBehaviour {

	public static ExplosionManager S;

	//souds
	public AudioClip hitSound;
	private AudioSource source;
	private float volLowRange = 2.0f;
	private float volHighRange = 3.0f;
	private float lowPitchRange = .75F;
	private float highPitchRange = 1.5F;

	void Awake() {
		S = this;

		//load sound source
		source = GetComponent<AudioSource>();
	}

	public void PlaySoundExplosion(){
		//Play spund when crashing into planet
		float vol = Random.Range (volLowRange, volHighRange);
		source.pitch = Random.Range (lowPitchRange,highPitchRange);
		source.PlayOneShot(hitSound,vol);
	}
}
