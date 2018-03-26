using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellExplosive : MonoBehaviour {

	[SerializeField] private float radius = 3.0f; // explosive radius
	[SerializeField] private float power = 10.0f; // explosive power
	[SerializeField] private float explosiveLift = 1.0f; // higher number the higher it will lift

	private Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody>();
//		rb.centerOfMass = new Vector3(0.0f,0.0f,-5.0f);
	}

	void OnCollisionEnter(){
		Detonate();
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}

	void Detonate () {
		
		Vector3 explosionPosition = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);

		foreach(Collider hit in colliders){

			Rigidbody rb = hit.GetComponent<Rigidbody>();

			if(rb != null){				
				rb.AddExplosionForce(power, explosionPosition, radius, explosiveLift, ForceMode.Impulse);
			}
		}

		Destroy(this.gameObject, 0.01f);

	}

	void Update(){
		transform.rotation = Quaternion.LookRotation(rb.velocity);	
	}

}
