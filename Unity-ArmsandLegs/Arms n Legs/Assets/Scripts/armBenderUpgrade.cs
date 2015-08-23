using UnityEngine;
 using System.Collections;
 
 public class armBenderUpgrade : MonoBehaviour {
     
     //Rotation Sensitivity
     public float RotationSensitivity = 35.0f;
     public float minAngle = -45.0f;
     public float maxAngle = 45.0f;
     
     //Rotation Value
     float yRotate = 0.0f;
     float zRotate = 0.0f;
	 
     // Update is called once per frame
     void Update () {
         
		 //Rotate Y view
         yRotate += Input.GetAxis ("Horizontal") * RotationSensitivity * Time.deltaTime;
         yRotate = Mathf.Clamp (yRotate, minAngle, maxAngle);
         transform.eulerAngles = new Vector3 (0.0f, yRotate, 0.0f);
		 
		 //Rotate Z view
         zRotate += Input.GetAxis ("Vertical") * RotationSensitivity * Time.deltaTime;
         zRotate = Mathf.Clamp (zRotate, minAngle, maxAngle);
         transform.eulerAngles = new Vector3 (0.0f, 0.0f, zRotate);
     }
 }
 //http://answers.unity3d.com/questions/762475/limit-range-camera-rotation.html