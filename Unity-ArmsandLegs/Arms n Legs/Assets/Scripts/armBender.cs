using UnityEngine;
using System.Collections;

public class armBender : MonoBehaviour {
    public float torque;
	
	public float turnHor = 0;
	public float turnVer = 0;

    void FixedUpdate() {
		
		float turnHor = Input.GetAxis("Horizontal");
		float turnVer = Input.GetAxis("Vertical");
        transform.Rotate(Vector3.right * turnHor * torque);
		transform.Rotate(Vector3.back * turnVer * torque);
    }
}