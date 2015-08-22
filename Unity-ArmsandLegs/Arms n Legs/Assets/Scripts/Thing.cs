using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Thing : MonoBehaviour {
	
	public Animator animator;
	public Rigidbody rb;

	//states all Things have
	const int STATE_IDLE = 0;

	protected string _currentDirection = "left";
	protected int _currentAnimationState = STATE_IDLE;

	//universal timers
	//put those here. don't know if we need this but here it is

    public List<GameObject> attachedLimbs;
	
	// Use this for initialization
	protected virtual void Start () {
		animator = this.GetComponent<Animator>();
		rb = this.GetComponent<Rigidbody>();
		attachedLimbs = new List<GameObject>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	    //default behaviours go in here
		//probably code for sticking arms&legs on to things would go in here
	}

    protected virtual void changeState(int state){
		if(_currentAnimationState == state) return;

		switch(state){
			case STATE_IDLE:
			{
				animator.SetInteger("state", STATE_IDLE);
				break;
			}
		    default:
				return; //so _currentAnimationState won't ever get set to
				        //non-existant states
		}
		_currentAnimationState = state;
	}

	void changeDirection(string direction){
		if(_currentDirection != direction)
		{
			if(direction == "right")
			{
				transform.Rotate(0, 180, 0);
				_currentDirection = "right";
			}
			else if(direction == "left")
			{
				transform.Rotate(0, -180, 0);
				_currentDirection = "left";
			}
		}
		
	}
}
