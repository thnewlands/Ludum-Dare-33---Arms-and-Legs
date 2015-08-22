using UnityEngine;
using System.Collections;

public class Eyes : Thing {
	
	//additional states not held by base
	const int STATE_HOP = 1;
	
    //timers
	float tHop = 0;
	
	protected override void Start () {
		base.Start();
		tHop = Time.time + Random.Range(2f, 4f);
	}
	
    protected override void Update () {
		base.Update();
		if (tHop < Time.time)
		{
			changeState(STATE_HOP);
			StartCoroutine(hopMovement());
			tHop = Time.time + Random.Range(2f, 4f);
		}
	}

	//handle additional states only
    protected override void changeState(int state){
		base.changeState(state);
		if(_currentAnimationState == state) return;
		switch(state){
			case STATE_HOP:
			{
				animator.SetInteger("state", STATE_HOP);
				break;
			}
			default:
				return; //so _currentAnimationState won't ever get set to
				        //non-existant states
		}
		_currentAnimationState = state;
	}

	IEnumerator hopMovement(){
		Vector3 start = transform.position;
		Vector3 end = new Vector3(transform.position.x + Random.Range(-5, 5),
								  transform.position.y,
								  transform.position.z + Random.Range(-5, 5));
		for(float i = 0f; i < 1f; i += .1f)
		{
			transform.position = Vector3.Lerp(start, end, i);
			yield return new WaitForSeconds(.05f);
		}
	}
}
