using UnityEngine;
using System.Collections;

public class Floatingdog : Thing {
	
	//additional states not held by base
	
    //timers
	float tMove = 0;

	Vector3 randomPosition;
	
	protected override void Start () {
		base.Start();
		randomPosition = transform.position;
	}
	
    protected override void Update () {
		base.Update();
		if(attachedLimbs.Count == 0){
		}

		if(tMove < Time.time)
		{
			tMove += 10;
			float min = 500;
			float max = 800;
			rb.AddForce(Random.Range(min,max), 0, Random.Range(min,max));
		}
	}

	//handle additional states only
    protected override void changeState(int state){
		base.changeState(state);
		if(_currentAnimationState == state) return;
		switch(state){
			default:
				return; //so _currentAnimationState won't ever get set to
				        //non-existant states
		}
		_currentAnimationState = state;
	}
}

