using UnityEngine;
using System.Collections;

public class cameracontroller : MonoBehaviour {

	public GameObject[] targets;
	int currentTarget = 0;
	Vector3 offset;
	bool flying = false;
    Coroutine flyingCoroutine = null;
	float rotation = 0; //in radians
	public float rotationSensitivity = 0.01f;
	float cameraHeight = 10;
	Vector3 startOffset; //for rotation stuff
	float rotationRadius = 10;
	
	void Start () {
		/*
		targets = new GameObject[4];
		targets[0] = GameObject.Find("AllEyes/Eyes1");
		targets[1] = GameObject.Find("AllEyes/Eyes2");
		targets[2] = GameObject.Find("AllEyes/Eyes3");
		targets[3] = GameObject.Find("AllEyes/Eyes4");
		*/
		print(transform.position - targets[1].transform.position);
		offset = new Vector3(10, cameraHeight, 0);
	    startOffset = new Vector3(10, cameraHeight, 0);
		print(transform.position);
		print(targets[0].transform.position);
	}

	void Update(){
		if (Input.GetKeyDown("q"))
		{
			switchTarget((currentTarget == 0)
						 ? (targets.Length-1)
						 : (currentTarget-1));
		}
		else if (Input.GetKeyDown("e"))
		{
			switchTarget((currentTarget+1) % targets.Length);
		}

		if(false)//!flying)
		{
			float additionalRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
			rotation += additionalRotation;
			
			float newOffsetX = rotationRadius * Mathf.Cos(rotation);
			float newOffsetZ = rotationRadius * Mathf.Sin(rotation);
			
			offset = new Vector3(newOffsetX, cameraHeight, newOffsetZ);
		}
		
	}
	
	void LateUpdate () {
		if(flying == false)
		{
			transform.position = targets[currentTarget].transform.position + offset;
			transform.LookAt(targets[currentTarget].transform.position);
		}
	}

	void switchTarget(int newTarget){
		if(flyingCoroutine == null)
		{
			flyingCoroutine = StartCoroutine(flyToTarget(newTarget));
		}
		else
		{
			StopCoroutine(flyingCoroutine);
			flyingCoroutine = StartCoroutine(flyToTarget(newTarget));
		}
	}

	IEnumerator flyToTarget(int newTarget){
		flying = true;
		int oldTarget = newTarget;
		currentTarget = newTarget;

		float startTime = Time.time;
		Vector3 t1 = targets[oldTarget].transform.position;
		Vector3 t2 = targets[newTarget].transform.position;
		float distance = Vector3.Distance(t1, t2);
		float trimmedDistance = distance;
		float minDistance = 10;
	    float maxDistance = 100;
		if(trimmedDistance > maxDistance) trimmedDistance = maxDistance;
		else if(trimmedDistance < minDistance) trimmedDistance = minDistance;
		float additionalTime = map(trimmedDistance, minDistance, maxDistance, 1f, 3f);
		float endTime = startTime + additionalTime;
		
		Vector3 startPosition = transform.position;
		Vector3 endPosition;
		float x1 = targets[newTarget].transform.position.x - transform.position.x;
		float z1 = targets[newTarget].transform.position.z - transform.position.z;
		float possibleDestinationRotationAngle1 = Mathf.Atan(z1/x1);
		float possibleDestinationRotationAngle2 = possibleDestinationRotationAngle1 + Mathf.PI;
		float possibleOffsetX1 = rotationRadius * Mathf.Cos(possibleDestinationRotationAngle1);
		float possibleOffsetZ1 = rotationRadius * Mathf.Sin(possibleDestinationRotationAngle1);
		float possibleOffsetX2 = rotationRadius * Mathf.Cos(possibleDestinationRotationAngle2);
		float possibleOffsetZ2 = rotationRadius * Mathf.Sin(possibleDestinationRotationAngle2);
		float distance1 = getDistance(transform.position.x,
									  targets[newTarget].transform.position.x + possibleOffsetX1,
									  transform.position.z,
									  targets[newTarget].transform.position.z + possibleOffsetZ1);
		float distance2 = getDistance(transform.position.x,
									  targets[newTarget].transform.position.x + possibleOffsetX2,
									  transform.position.z,
									  targets[newTarget].transform.position.z + possibleOffsetZ2);
		float newOffsetX, newOffsetZ;
		if(distance1 < distance2)
		{
			newOffsetX = possibleOffsetX1;
			newOffsetZ = possibleOffsetZ1;
		}
		else
		{
			newOffsetX = possibleOffsetX2;
			newOffsetZ = possibleOffsetZ2;
			print(distance1 + " was greater than " + distance2 + ", so newOffset was set to ("
				  + newOffsetX + ", " + newOffsetZ + ")");
		}
		endPosition.x = targets[newTarget].transform.position.x + newOffsetX;
		endPosition.y = targets[newTarget].transform.position.y + cameraHeight;
		endPosition.z = targets[newTarget].transform.position.z + newOffsetZ;
		offset.x = newOffsetX;
		offset.z = newOffsetZ;
		
		Vector3 startRotation = transform.rotation.eulerAngles;
		//move to end position, look at, save new rotation,
		//then move back and go back to startRotation
		transform.position = endPosition;
		transform.LookAt(targets[currentTarget].transform.position);
		Vector3 endRotation = transform.rotation.eulerAngles;
		transform.rotation = Quaternion.Euler(startRotation.x,
											  startRotation.y,
											  startRotation.z);
		transform.position = startPosition;
		while(Time.time < endTime)
		{
			float p = (Time.time - startTime) / (endTime - startTime);
			p = p*p * (3f - 2f*p);
			p = p*p * (3f - 2f*p);
			p = p*p * (3f - 2f*p);
			transform.position = Vector3.Lerp(startPosition, endPosition, p);
			Vector3 updatedRotation = Vector3.Lerp(startRotation, endRotation, p);
			transform.rotation = Quaternion.Euler(updatedRotation.x,
												  updatedRotation.y,
												  updatedRotation.z);
			yield return 0;
		}
		flying = false;
		flyingCoroutine = null;
		//offset = transform.position - targets[newTarget].transform.position;
	}

	float getDistance(float x1, float y1, float x2, float y2){
		return Mathf.Sqrt(Mathf.Pow(x2 - x1, 2) + Mathf.Pow(y2 - y1, 2));
	}
	
	float map(float x, float a1, float a2, float b1, float b2){
		return (x-a1)/(a2-a1) * (b2-b1) + b1;
	}
}
