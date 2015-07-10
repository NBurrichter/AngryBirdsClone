using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

	public static FollowCam S; //Singleton Follow Cam Instance

	public GameObject poi;

	private float camZ;

	public float easing = 0.05f;

	public Vector2 minXY;

	//screenshake
	public Vector2 onCircle;
	public Vector3 shakeTarget;
	public float targetDistance = 0.1f;
	public float shakeIntensity = 2.0f;
	public float shakeTime = 0.0f;
	private float shakeMaxTime = 0.0f;
	public float decreaseFactor = 1.0f;
	public float shakeEasing = 0.1f;
	public float shakeTargetDistance = 0.1f;

	void Awake() {
		S = this;
		camZ = transform.position.z;

	}

	void FixedUpdate() {

		Vector3 destination;

		//Check if the poi exists
		if (poi == null) {
			//set the poi to the slingshot(zero vector)
			destination = Vector3.zero;
		} else {
			//else (there is a poi)

			destination = poi.transform.position;

			//is the poi a projectile
			if (poi.tag == "Projectile") {

				//check if it is restin(sleeping)
				if (poi.GetComponent<Rigidbody> ().IsSleeping ()) {

					//set the poi to default
					poi = null;
					return;
				}
			}
					
		}

		destination.x = Mathf.Max (minXY.x, destination.x);
		destination.y = Mathf.Max (minXY.y, destination.y);

		destination = Vector3.Lerp (transform.position, destination, easing);

		destination.z = camZ;

		transform.position = destination;

		this.GetComponent<Camera> ().orthographicSize = 10 + destination.y;

		//Screenshake
		if (shakeTime > 0) {

			onCircle = Random.insideUnitCircle;
			shakeTarget = new Vector3 (transform.localPosition.x + onCircle.x * shakeIntensity  * shakeTime/shakeMaxTime, transform.localPosition.y + onCircle.y * shakeIntensity * shakeTime/shakeMaxTime, transform.localPosition.z);

			transform.localPosition = Vector3.Lerp (transform.localPosition, shakeTarget, shakeEasing);
			shakeTime -= Time.deltaTime * decreaseFactor;
		
		} else {
			shakeTime = 0.0f;
			shakeTarget = transform.position;
			transform.localPosition = Vector3.Lerp (transform.localPosition, shakeTarget, shakeEasing);
		}

	}

	public void ShakeScreen(float time, float magnitude){
		if (shakeIntensity < magnitude) {
			shakeIntensity = magnitude;
		} else {
			if (shakeTime <= 0){
				shakeIntensity = magnitude;
			}
		}
		shakeTime = time;
		shakeMaxTime = time;
	}
}
