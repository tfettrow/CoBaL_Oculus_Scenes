using UnityEngine;
using System.Collections;

public class UpdatePlaneONcollide : MonoBehaviour {

	Vector3 currentposition;
	GameObject OrigObject;

	void Start()
	{
		OrigObject = this.gameObject;

		//GameObject.Find("OrigCube");
		//transform.parent = OrigObject.transform;
		currentposition = OrigObject.transform.position; // transform.position;
	}

	void OnTriggerEnter(Collider ObjectTriggered)
	{
		if(ObjectTriggered.gameObject.name == "ObjectBoundary")
		{
			currentposition.z = 70; // Find a way to automate.. possibly globabl Distance variable.
			transform.position = currentposition;

			this.gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", Color.gray);
		}       
	}
}