using UnityEngine;
using System.Collections;

public class sticky : MonoBehaviour {

	public GameObject rigidBodysource;
	public GameObject rootTransform;
	public bool jointCollided = false;
	public float gracePeriodTimer = 0f;

	public AudioSource connectAudio;
	public AudioSource disconnectAudio;

	public GameObject sparksPrefab;

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
			    RaycastHit hit;
				Physics.Raycast(transform.position, transform.forward, out hit);
				print("arm: " + transform.position + "other: " + other.transform.position);
				Vector3 offset = transform.parent.transform.position - other.transform.position;
				rootTransform.transform.parent = other.transform;
				transform.position = offset;
				jointCollided = true;
				connectAudio.Play();
				
				StartCoroutine(emitSparks(hit.point));
			}
		}
	}

	void Update() {
		if (jointCollided == true){
			if (Input.GetKeyDown("space")){
				rigidBodysource.AddComponent<Rigidbody>();
				Vector3 worldPos = transform.position;
				rootTransform.transform.parent = null;
				transform.position = transform.parent.transform.position;
				jointCollided = false;
				gracePeriodTimer = Time.time + 3f;
				disconnectAudio.Play();
				StartCoroutine(emitSparks(transform.position));
				print(transform.position);
			}
		}
	}

	IEnumerator emitSparks(Vector3 origin){
		GameObject sparks = Instantiate(sparksPrefab, origin, transform.rotation) as GameObject;
		yield return new WaitForSeconds(2);
	    Destroy(sparks);
	}

}