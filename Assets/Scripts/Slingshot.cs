using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	//Inspector Variables
	public GameObject prefabProjectile;
	public float velocityMultiplier = 4.0f;
	public GameObject cannon;

	//Internal state Variables
	private GameObject launchPoint;
	private bool aimingMode;

	private GameObject projectile;
	private Vector3 launchPos;

	//Blendshape controllers
	SkinnedMeshRenderer skinnedMeshRenderer;
	//Mesh skinnedMesh;

	void Awake() {
		Transform launchPointTrans = transform.Find("Launchpoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive(false);
		launchPos = launchPointTrans.position;

		//Blendshapes:
		skinnedMeshRenderer = cannon.GetComponent<SkinnedMeshRenderer> ();
		//skinnedMesh = cannon.GetComponent<SkinnedMeshRenderer> ().sharedMesh;
	}

    void OnMouseEnter() {
		//print ("Slingshot:MouseEnter");
		launchPoint.SetActive (true);	
	}

	void OnMouseExit(){
		//print ("Slingshot:MouseExit");
		launchPoint.SetActive (false);
	}

	void OnMouseDown() {
		//set the game to aiming mod
		aimingMode = true;

		//instantiate a projectile at launchpoint
		projectile = Instantiate (prefabProjectile) as GameObject;
		projectile.transform.position = launchPos; 

		//Switch off physics for now
		projectile.GetComponent<Rigidbody> ().isKinematic = true;

	}

	void OnMouseOver(){
		launchPoint.SetActive (true);	
	}

	void Update() {
		if (!aimingMode)
			return;

		launchPoint.SetActive (true);	
		//get mouse pos and convert it to 3D
		Vector3 mousePos2D = Input.mousePosition;
		mousePos2D.z = - Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);     

		//Calculate the delta between launch position and mouse position
		Vector3 mouseDelta = mousePos3D - launchPos;

		//cannon looking into direction of shooting
		Debug.DrawLine (cannon.transform.position,cannon.transform.position + mouseDelta);
		cannon.transform.LookAt (mouseDelta);

		//Constrain the delta to the maximum of the sphere collider
		float maxMagnitude = this.GetComponent<SphereCollider> ().radius;
		mouseDelta = Vector3.ClampMagnitude (mouseDelta,maxMagnitude);

		//Changing blenshape
		skinnedMeshRenderer.SetBlendShapeWeight (0, Vector3.Magnitude((mouseDelta))*25);

		//set projectile position to new position and fire it
		projectile.transform.position = launchPos + mouseDelta;

		if (Input.GetMouseButtonUp (0)) {
			aimingMode = false;
			projectile.GetComponent<Rigidbody> ().isKinematic = false;

			projectile.GetComponent<Rigidbody> ().velocity = -mouseDelta * velocityMultiplier;
			launchPoint.SetActive (false);

			//Reset Trail
			ProjectileTrail.S.Clear();

			//Add shot to counter
			GameController.ShotFired();

			//set poi for cam
			FollowCam.S.poi = projectile;

			//Reset blenshape to default
			skinnedMeshRenderer.SetBlendShapeWeight (0, 0);
		}
	}
	
}
