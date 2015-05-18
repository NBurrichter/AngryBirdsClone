using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

	public static FollowCam S; //Singleton Follow Cam Instance

	public GameObject poi;

	private float camZ;

	public float easing = 0.05f;

	public float speed = 0.1f;
	public float startTime;

	public Vector2 minXY;


	void Awake() {
		S = this;
		camZ = transform.position.z;
	}

	void FixedUpdate() {

		//Check if the poi exists
		if (poi == null) {
			return;
		}

		Vector3 destination = poi.transform.position;

		destination.x = Mathf.Max (minXY.x, destination.x);
		destination.y = Mathf.Max (minXY.y, destination.y);

		destination = Vector3.Lerp(transform.position,destination,easing);

		destination.z = camZ;

		transform.position = destination;

		this.GetComponent<Camera> ().orthographicSize = 10 + destination.y;
	}
}
