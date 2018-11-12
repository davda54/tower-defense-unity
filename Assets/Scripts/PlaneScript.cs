using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    public Vector2 ShadowOffset;

    private Transform shadow;
    private Quaternion prevRotation = Quaternion.identity;
    
	void Start ()
    {
        shadow = transform.Find("Shadow");
	}
	
	void FixedUpdate ()
    {
        var rotationChange = Mathf.Abs(prevRotation.eulerAngles.z - transform.rotation.eulerAngles.z);

        var scale = transform.localScale.y * 0.9f + (1.0f - 0.5f * Mathf.Min(rotationChange, 1.0f)) * 0.1f;
        transform.localScale = new Vector3(1.0f, scale, 1.0f);
        shadow.position = transform.position + (Vector3)ShadowOffset;
        shadow.rotation = transform.rotation;

        prevRotation = transform.rotation;
	}
}
