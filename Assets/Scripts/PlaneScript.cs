using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    public Vector2 ShadowOffset;
    private Transform shadow;
    
	void Start ()
    {
        shadow = transform.Find("Shadow");
	}
	
	void FixedUpdate ()
    {
        shadow.position = transform.position + (Vector3)ShadowOffset;
        shadow.rotation = transform.rotation;
	}
}
