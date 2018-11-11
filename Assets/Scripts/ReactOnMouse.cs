using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactOnMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        gameObject.transform.Translate(0, -3f, 0);
    }

    void OnMouseUp()
    {
        gameObject.transform.Translate(0, 3f, 0);
    }
}
