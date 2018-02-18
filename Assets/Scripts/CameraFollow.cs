using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject player;
	private float cameraHeight = 6.0f;
	private float cameraOffset = -6.0f;

	// Update is called once per frame
	void Update () {
		Vector3 pos = player.transform.position;
		pos.y += cameraHeight;
		pos.z += cameraOffset;
		pos.x += cameraOffset;
		transform.position = pos;
	}
}
