using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingRotator : MonoBehaviour {

    public float Amount;
    public float Speed;

    private float t;
	
	void FixedUpdate () {
        t += Time.deltaTime;
        transform.Rotate(0, 0, Mathf.Sin(t*Speed)*Amount);
	}
}
