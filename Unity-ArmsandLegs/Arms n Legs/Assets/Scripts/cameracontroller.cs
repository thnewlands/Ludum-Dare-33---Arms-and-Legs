using UnityEngine;
using System.Collections;

public class cameracontroller : MonoBehaviour {

	public GameObject[] targets;
	int currentTarget = 0;
	Vector3 offset;
	bool flying = false;
    Coroutine flyingCoroutine = null;
	
	void Start () {
		targets = new GameObject[4];
		targets[0] = GameObject.Find("AllEyes/Eyes1");
		targets[1] = GameObject.Find("AllEyes/Eyes2");
		targets[2] = GameObject.Find("AllEyes/Eyes3");
		targets[3] = GameObject.Find("AllEyes/Eyes4");
		offset = transform.position;
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
	}
	
	void LateUpdate () {
		if(flying == false)
		{
			transform.position = targets[currentTarget].transform.position + offset;
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
		Vector3 endPosition = targets[newTarget].transform.position + offset;
		while(Time.time < endTime)
		{
			float p = (Time.time - startTime) / (endTime - startTime);
			p = p*p * (3f - 2f*p);
			p = p*p * (3f - 2f*p);
			p = p*p * (3f - 2f*p);
			transform.position = Vector3.Lerp(startPosition, endPosition, p);
			yield return 0;
		}
		flying = false;
		flyingCoroutine = null;
	}

	float map(float x, float a1, float a2, float b1, float b2){
		return (x-a1)/(a2-a1) * (b2-b1) + b1;
	}
}
