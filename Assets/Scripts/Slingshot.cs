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

	//Sounds
	public AudioClip shootSound;
	public AudioClip reloadSound;
	public float reloadSpeed = 1.0f;
	private float reloadSoundTimer = 0.0f;
	private bool reload = false;
	private AudioSource source;
	private float volLowRange = .5f;
	private float volHighRange = 1.0f;
	private float lowPitchRange = .75F;
	private float highPitchRange = 1.5F;

	void Awake() {
		Transform launchPointTrans = transform.Find("Launchpoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive(false);
		launchPos = launchPointTrans.position;

		//Blendshapes:
		skinnedMeshRenderer = cannon.GetComponent<SkinnedMeshRenderer> ();
		//skinnedMesh = cannon.GetComponent<SkinnedMeshRenderer> ().sharedMesh;

		//Find audio source
		source = GetComponent<AudioSource>();
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
		//projectile.GetComponent<MeshRenderer> ().enabled = false;
	}

	void OnMouseOver(){
		launchPoint.SetActive (true);	
	}

	void Update() {
		//reload timer
		if (reloadSoundTimer > 0) {
			reloadSoundTimer -= 1 * Time.deltaTime;
		} else {
			if (reload == true){
				float vol = Random.Range (volLowRange, volHighRange);
				source.pitch = Random.Range (lowPitchRange,highPitchRange);
				source.PlayOneShot(reloadSound,vol);
				reload = false;
			}

		}

		if (!aimingMode)
			return;

		launchPoint.SetActive (true);	
		//get mouse pos and convert it to 3D
		Vector3 mousePos2D = Input.mousePosition;
		mousePos2D.z = - Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);     

		//Calculate the delta between launch position and mouse position
		Vector3 mouseDelta = mousePos3D - launchPos;

		//Constrain the delta to the maximum of the sphere collider
		float maxMagnitude = this.GetComponent<SphereCollider> ().radius;
		mouseDelta = Vector3.ClampMagnitude (mouseDelta,maxMagnitude);

		//Changing blenshape
		skinnedMeshRenderer.SetBlendShapeWeight (0, Vector3.Magnitude((mouseDelta))*25);

		//cannon looking into direction of shooting
		Debug.DrawLine (cannon.transform.position,cannon.transform.position + mouseDelta);
		cannon.transform.LookAt (cannon.transform.position + mouseDelta);
		cannon.transform.rotation = cannon.transform.rotation * new Quaternion (180,0,0,0);

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

			//show projectile
			projectile.GetComponent<MeshRenderer> ().enabled = true;

			//Reset blenshape to default
			skinnedMeshRenderer.SetBlendShapeWeight (0, 0);

			//Play Sound with random volume and pitch
			float vol = Random.Range (volLowRange, volHighRange);
			source.pitch = Random.Range (lowPitchRange,highPitchRange);
			source.PlayOneShot(shootSound,vol);

			//reload sound
			reload = true;
			reloadSoundTimer = reloadSpeed;
		}
	}
	
}
