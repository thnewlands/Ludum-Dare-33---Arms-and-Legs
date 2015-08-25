using UnityEngine;
using System.Collections;

public class levelLoader : MonoBehaviour {

	void Update () {
		if(Input.GetButton("Jump")){
			Application.LoadLevel (1);
		}
	}
}

