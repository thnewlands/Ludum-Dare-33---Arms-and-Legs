using UnityEngine;
using System.Collections;

public class armBenderMesh : MonoBehaviour {

     public float RotationSensitivity_y = 35.0f;
     public float minAngle_y = -45.0f;
     public float maxAngle_y = 45.0f;
	 
	 public float RotationSensitivity_z = 35.0f;
	 public float minAngle_z = -45.0f;
     public float maxAngle_z = 45.0f;
     
     float yRotate = 0.0f;
	 float zRotate = 0.0f;

     // Update is called once per frame
     void Update () {
         
         yRotate += Input.GetAxis ("Vertical") * RotationSensitivity_y * Time.deltaTime;
         yRotate = Mathf.Clamp (yRotate, minAngle_y, maxAngle_y);
		 
		 zRotate += Input.GetAxis ("Horizontal") * RotationSensitivity_z * Time.deltaTime;
         zRotate = Mathf.Clamp (zRotate, minAngle_z, maxAngle_z);
		 
		 transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, yRotate, zRotate);
		 
		 
		 //transform.Rotate(0f, yRotate, 0f, Space.Self);
		 //transform.eulerAngles = new Vector3 (0f, yRotate, 0f);
	}
}
