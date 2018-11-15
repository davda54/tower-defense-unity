using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int Value;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var scale = 1.0f + 0.2f * Mathf.Sin(5*Time.realtimeSinceStartup);
        transform.localScale = new Vector3(scale, scale, 1.0f);
	}

    void OnMouseOver()
    {
        GameManager.Instance.CoinCollected(gameObject);
        Pool.Instance.DeactivateObject(gameObject);
    }
}
