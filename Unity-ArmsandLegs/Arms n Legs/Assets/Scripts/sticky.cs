using UnityEngine;
using System.Collections;

public class sticky : MonoBehaviour {

public GameObject rigidBodysource;
public GameObject rootTransform;
public bool jointCollided = false;
public float gracePeriodTimer = 0f;

public AudioSource connectAudio;
public AudioSource disconnectAudio;

 void Start() {
     AudioSource[] audios = GetComponents<AudioSource>();
     connectAudio = audios[0];
     disconnectAudio = audios[1];
 }

void OnTriggerEnter(Collider other) {
	if(gracePeriodTimer < Time.time && jointCollided == false){
		if(other.GetComponent<Collider>().tag == "Stickable"){
				print("Stickable");
				Destroy (rigidBodysource.GetComponent<Rigidbody>());
				rootTransform.transform.parent = other.transform;
				jointCollided = true;
				connectAudio.Play();
		}
	}
}

void Update() {
	if (jointCollided == true){
		if (Input.GetKeyDown("space")){
			rigidBodysource.AddComponent<Rigidbody>();
			rootTransform.transform.parent = rootTransform.transform.parent;
			jointCollided = false;
			gracePeriodTimer = Time.time + 3f;
			disconnectAudio.Play();
		}
	}
}

}