using UnityEngine;
using System.Collections;

public class BlendshapeTest : MonoBehaviour {
	
	SkinnedMeshRenderer skinnedMeshRenderer;
	float blendOne = 0f;
	float blendSpeed = 0.1f;
	
	void Awake ()
	{
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
	}
	
	void Start ()
	{

	}
	
	void Update ()
	{
		blendOne += blendSpeed;
		skinnedMeshRenderer.SetBlendShapeWeight (0, blendOne);

	}
}
