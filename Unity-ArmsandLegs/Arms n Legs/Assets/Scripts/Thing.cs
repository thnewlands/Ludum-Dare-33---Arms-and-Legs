using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Thing : MonoBehaviour {

	public Camera camera;
	public Animator animator;
	public Rigidbody rb;

	//states all Things have
	protected const int STATE_IDLE = 0;
	protected const int STATE_WPAL = 1;

	protected string _currentDirection = "left";
	protected int _currentAnimationState = STATE_IDLE;

	//universal timers
	//put those here. don't know if we need this but here it is

	protected bool hasChild = false;
	
	// Use this for initialization
	protected virtual void Start () {
		camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		animator = this.GetComponent<Animator>();
		rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	    if(transform.childCount > 0)
		{
			hasChild = true;
			changeState(STATE_WPAL);
		}

		if(transform.childCount == 0 && _currentAnimationState != STATE_IDLE)
		{
			hasChild = false;
			changeState(STATE_IDLE);
		}

		if(!hasChild)
		{
			Vector3 lookPos = camera.transform.position - transform.position;
			lookPos.y = 0;
			Quaternion rotation = Quaternion.LookRotation(lookPos);
			transform.rotation = rotation;
		}
	}

    protected virtual void changeState(int state){
		if(_currentAnimationState == state) return;

		switch(state){
			case STATE_IDLE:
			{
				print("got here");
				animator.SetInteger("state", STATE_IDLE);
				break;
			}
			case STATE_WPAL:
			{
				animator.SetInteger("state", STATE_WPAL);
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
