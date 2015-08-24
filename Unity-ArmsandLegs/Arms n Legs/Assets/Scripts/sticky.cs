using UnityEngine;
using System.Collections;

public class sticky : MonoBehaviour {

public GameObject rigidBodysource;
public GameObject rootTransform;
public bool jointCollided = false;
public float gracePeriodTimer = 0f;

void OnTriggerEnter(Collider other) {
	if(gracePeriodTimer < Time.time){
		if(other.GetComponent<Collider>().tag == "Stickable"){
				print("Stickable");
				Destroy (rigidBodysource.GetComponent<Rigidbody>());
				rootTransform.transform.parent = other.transform;
				jointCollided = true;
		}
	}
}

void Update() {
	if (jointCollided = true){
		if (Input.GetKeyDown("space")){
			rigidBodysource.AddComponent<Rigidbody>();
			rootTransform.transform.parent = rootTransform.transform.parent;
			jointCollided = false;
			gracePeriodTimer = Time.time + 3f;
		}
	}
}

}