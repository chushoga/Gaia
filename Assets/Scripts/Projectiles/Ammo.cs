using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class Ammo : MonoBehaviour {

	// GENERAL
	public string ammoName;              // name
	//public Image iconImage;              // iconImage
	private Rigidbody rb;				 // rigidbody

	// SOUND
	public AudioSource shotSound;        // shot sound
	public AudioSource hitSounds;        // hit sound

	// OBJECTS TO ADD 
	[SerializeField] private GameObject mesh;              // mesh
	[SerializeField] private TrailRenderer trail;          // trail renderer
	[SerializeField] private Particle shotPart;            // shot particle
	[SerializeField] private Particle explodePart;         // explode particle	
	[SerializeField] private GameObject cMass;             // center of mass

	// PARAMETERS
	[SerializeField] private float weight = 1.0f;          // ammo weight
	[SerializeField] private float shotForce = 1.0f;       // shot force
	[SerializeField] private float angularDrag = 0.0f;     // angularDrag
	[SerializeField] private float explosiveForce = 0.0f;  // explosive force of shell
	[SerializeField] private float explosiveLift = 0.0f;   // explosive lift
	[SerializeField] private float explosiveRadius = 0.0f; // explosive radius
	[SerializeField] private float critChance = 1.0f;      // the crit chance for the particular shell

	void Start(){
		rb = GetComponent<Rigidbody>();
		//rb.centerOfMass = cMass.transform.position;
	}


	void OnCollisionEnter(){
		Detonate();
	}

	void Detonate(){

		Vector3 explosionPosition = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosiveRadius);

		foreach(Collider hit in colliders){

			Rigidbody rb = hit.GetComponent<Rigidbody>();

			if(rb != null){				
				rb.AddExplosionForce(explosiveForce, explosionPosition, explosiveRadius, explosiveLift, ForceMode.Impulse);
			}
		}

		Destroy(this.gameObject, 0.01f);
	}


}
