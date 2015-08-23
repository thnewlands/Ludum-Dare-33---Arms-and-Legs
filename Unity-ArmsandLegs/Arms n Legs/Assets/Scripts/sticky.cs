using UnityEngine;
using System.Collections;

public class sticky : MonoBehaviour {

public bool jointCollided = false;
public GameObject rigidBodysource;
public GameObject rootTransform;

void OnTriggerEnter(Collider other) {
		  if(other.GetComponent<Collider>().tag == "Stickable")
			 {
				print("Stickable");
				Destroy (rigidBodysource.GetComponent<Rigidbody>());
				rootTransform.transform.parent = other.transform;
				jointCollided = true;
			 }
	}
}

