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

    public List<GameObject> attachedLimbs;
	
	// Use this for initialization
	protected virtual void Start () {
		camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		animator = this.GetComponent<Animator>();
		rb = this.GetComponent<Rigidbody>();
		attachedLimbs = new List<GameObject>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	    //default behaviours go in here
		//probably code for sticking arms&legs on to things would go in here
		if(attachedLimbs.Count == 4)
		{
			changeState(STATE_WPAL);
		}
		if(Input.GetKeyDown("space") && _currentAnimationState == STATE_WPAL)
		{
			//eject limbs, arms and legs shoot off in different directions
		}

		transform.LookAt(transform.position + camera.transform.rotation * Vector3.back,
						 camera.transform.rotation * Vector3.up);
	}

    protected virtual void changeState(int state){
		if(_currentAnimationState == state) return;

		switch(state){
			case STATE_IDLE:
			{
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
