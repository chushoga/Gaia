using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRotAgent : MonoBehaviour {

	// When using the navmesh this will rotate the transform to match the normal so 
	// that when you go up a slope it will match the slop and not be stuck on one axis.
	// Tested on tank treds.
	void FixedUpdate()
	{
		RaycastHit hit;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
		if (Physics.Raycast(transform.position + transform.forward, Vector3.down, out hit)){
			Vector3 fHit = hit.point; // get the point where the front ray hits the ground
			if (Physics.Raycast(transform.position - transform.forward, Vector3.down, out hit)){
				Vector3 bHit = hit.point; // get the back hit point
				transform.forward = fHit - bHit; // align the object to these points
			}
		}
	}
}
