using UnityEngine;
using Assets.Scripts;

public class BulletScript : FlyingShotScript
{   
    private float distance;

    void Start()
    {
    }
    
	// Use this for initialization
	void OnEnable ()
    {
        distance = 0.0f;
        Pool.Instance.ActivateObject("rifleSoundEffect").SetActive(true);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        var diff = Time.deltaTime * Speed;
        distance += diff;
        transform.position += (Vector3)Direction * diff;

        if(distance > Range)
        {
            Pool.Instance.DeactivateObject(gameObject);
        }
	}
}
