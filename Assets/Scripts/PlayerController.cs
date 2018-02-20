using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
	
	Camera cam;

	public LayerMask groundLayer;
	public NavMeshAgent playerAgent;


	// ************************************************************************************
	// GameObject
	// **********

	public bool bombardMode = false; // Bombard mode
	public float bombardMovementSpeed = 0.5f;
	public float movementSpeed = 10f; // player movement speed
	public GameObject turret;

	// ************************************************************************************
	// Turret
	// ******
	// TODO: Move these to a separate weapon class after some testing and have that class handle
	// the firing. Can keep the turret rotation here though.... This is in case there is more
	// than one weapon.

	[SerializeField] private float turretRotSpeed = 2.0f; // Rotation speed for the turret
	[SerializeField] private float turretTiltSpeed = 2.0f; // Tilt speed for the turret.	

	public float shotForce = 100.0f; // Force of the turret shot.
	public GameObject turretSpawnPoint; // SpawnPoint for the projectile.
	public GameObject projectile; // Projectile here.

	// ************************************************************************************


	void Start(){
		cam = Camera.main;
		//navAgent = GetComponent<NavMeshAgent>(); // Grab the NaveMeshAgent from the GameObject
		playerAgent.acceleration = movementSpeed; // set the acceleration of the agent here.
	}
	
	// Update is called once per frame
	void Update () {

		// Click to move player
		if(Input.GetMouseButtonDown(0)){
			playerAgent.SetDestination(GetPointUnderCursor());
		}

		// Check if in bombard mode or not.
		// If in bombard mode then:
		// - slow movement speed
		// - allow turret to be rotated
		// - increase shot damage
		if(bombardMode == true){
			playerAgent.acceleration = bombardMovementSpeed; // Reduce the movement speed
		} else {
			playerAgent.acceleration = movementSpeed;
		}

		// ********************************************************
		// TURRET CONTROL
		// ********************************************************

		// Rotate Turret left
		if(Input.GetKey(KeyCode.D)){
			Debug.Log("Turn Me");
			turret.transform.Rotate(Vector3.up * turretRotSpeed * Time.deltaTime);

		}

		// Rotate Turret right
		if(Input.GetKey(KeyCode.A)){
			turret.transform.Rotate(-Vector3.up * turretRotSpeed * Time.deltaTime); 
			Debug.Log("Turn Me Left");
		}

		// ********************************************************
		// shoot function here to be accessed by the buttons
		// **********
		if(Input.GetMouseButtonDown(1)) {
			GameObject clone;
			clone = Instantiate(projectile, turretSpawnPoint.transform.position, transform.rotation);
			clone.transform.Rotate(Vector3.left * 90);
			clone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * shotForce);
		}
		// ********************************************************

	}

	private Vector3 GetPointUnderCursor(){
		Vector2 screenPosition = Input.mousePosition;
		Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(screenPosition);

		RaycastHit hitPosition;

		Physics.Raycast(mouseWorldPosition, cam.transform.forward, out hitPosition, 100, groundLayer);

		return hitPosition.point;
	}
}
