using UnityEngine;
using System.Collections;

public class Trashcan : Thing {

	//additional states not held by base
	
    //timers
	
	protected override void Start () {
		base.Start();
	}
	
    protected override void Update () {
		base.Update();
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
