using UnityEngine;
using System.Collections;

public class simpleCameraFollow : MonoBehaviour {

	public GameObject player;
	private Vector3 offsetP;
	private Vector3 offsetR;
	// Use this for initialization
	void Start () {
		offsetP = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//float rotator = player.transform.eulerAngles.x + offsetR;
		transform.position = player.transform.position + offsetP;
		transform.eulerAngles = player.transform.position + offsetR;

		transform.LookAt(player.transform.position);
	}
}