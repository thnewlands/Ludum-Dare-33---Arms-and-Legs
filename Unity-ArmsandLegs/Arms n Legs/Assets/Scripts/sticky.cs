using UnityEngine;
using System.Collections;

public class sticky : MonoBehaviour {

public bool jointCollided = false;
public GameObject rigidBodysource;

void OnTriggerEnter(Collider other) {
		  if(other.GetComponent<Collider>().tag == "Stickable")
			 {
				print("Stickable");
				rigidBodysource.GetComponent<Rigidbody>().isKinematic = true;
				jointCollided = true; // stop physics
			 }
	}
}

//transform.parent = other.transform; // doesn't move yet, but will move w/what it hit