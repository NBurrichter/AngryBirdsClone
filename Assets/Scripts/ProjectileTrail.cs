using UnityEngine;
using System.Collections;

public class ProjectileTrail : MonoBehaviour {

/*
	public static ProjectileTrail S;
	
	//Public inspector field
	public float minDistance = 0.1f;
	
	
	//Internal fields
	private LineRenderer line;

	private GameObject _poi;

	private Vector3 lastPoint;

	private int pointsCount;

	//A Property: Looks like a field to the outside, but internally cals set/get
	public GameObject poi{
		get {
			return _poi;
		}

		set{
			//use "Value" to accecs the value to set the property to
			_poi = value;

			//check if the poi is to something 
			if (_poi == null){
				line.enabled = false;
				pointsCount = 0;
				line.SetVertexCount(0);
			//reset the whole line rendererererererererer thingy

		    
			}
		}
	}


	void Awake() {
		S = this;

		line = GetComponent<LineRenderer> ();

		//initialize

		pointsCount = 0;

		line.enabled = false;

		Color c1 = Color.yellow;
		Color c2 = Color.red;

		line.SetColors (c1,c2);
	}

	void FixedUpdate(){
		//is there a poi
		if (poi == null) {
			//if not,try using the cameras poi(if its a projectile)
			if(FollowCam.S.poi != null){
				if(FollowCam.S.poi.tag == "Projectile"){
					poi = FollowCam.S.poi;
				}else{
					return;
				}
				   
			}else{
				return;
			}
			//poi = FollowCam.poi;
		}
		//At this point the poi has a value and its a projectile

		Addpoint ();
		if (poi.GetComponent<Rigidbody> ().IsSleeping ()) {

		}
	}

	void Addpoint() {
		Vector3 pt = _poi.transform.position;

		//if  the point isnt far enough from the last point, do nothing
		if (pointsCount > 0 && (pt - lastPoint).magnitude < minDistance) {
			return;
		}

			//if our current point is the first (launchpoint) point
		if (pointsCount == 0) {
			// add the first point
			line.enabled = true;
		}

		pointsCount++;
		line.SetVertexCount(pointsCount);
		line.SetPosition (pointsCount - 1, pt);
			//else, its not the first point

			//Add another point to the line renderer

		lastPoint = pt;
	}
}

*/

	public static ProjectileTrail S; // Singleton instance

	// Fields in Inspector pane
	public float minDist = 0.1f;

	// Dynamic fields
	private LineRenderer line;
	private GameObject _poi;
	private Vector3 lastPoint;
	private int pointsCount;

	void Awake() {
		S = this; // Set the singleton instnce
		// Get a reference to the LineRenderer
		line = GetComponent<LineRenderer>();
		//line.material = new Material(Shader.Find("Mobile/Particles/Additive"));
		Color c1 = Color.yellow;
		Color c2 = Color.red;
		line.SetColors(c1,c2);
		pointsCount = 0;
		// Disable until its needed
		line.enabled = false;
	}

	// A property: Looks to the outside like a field but internally calls get/set
	public GameObject poi {
		get {
			return _poi;
		}
		set {
			_poi = value;
			if(_poi != null) {
				// If poi was set to something (and now to something new), reset everything
				line.enabled = false;
				pointsCount = 0;
				line.SetVertexCount(0);
			}
		}
	}

	void FixedUpdate() {
		if(poi == null) {
			// If there is no poi yet, try looking at the camera
			if(FollowCam.S.poi != null) {
				if(FollowCam.S.poi.tag == "Projectile") {
					poi = FollowCam.S.poi;
				} else {
					return; // Give up, no poi found
				}
			} else {
				return; // Give up, no poi found
			}
		}

		// Now poi definitely has a value and its a projectile
		// So add a point in every FixedUpdate()
		AddPoint();
		if(poi.GetComponent<Rigidbody>().IsSleeping()){
			// The poi is resting, so clear it
			poi = null;
		}
	}

	public void AddPoint(){
		Vector3 pt = _poi.transform.position;
		// If the point isnt far enough from the last one, do nothing
		if(pointsCount > 0 && (pt - lastPoint).magnitude < minDist) {
			return;
		}

		if(pointsCount == 0){
			// If its the launch point (first)
			line.SetVertexCount(1);
			line.SetPosition(0, pt);
			//line.SetPosition(1, pt);
			pointsCount += 1;
			line.enabled = true;
		} else {
			// Not the first point
			pointsCount++;
			line.SetVertexCount(pointsCount);
			line.SetPosition(pointsCount - 1, pt);
		}

		lastPoint = pt;
	}

	public void Clear(){
		_poi = null;
		line.enabled = false;
		pointsCount = 0;
		line.SetVertexCount(0);
	}
}
