using UnityEngine;

public class AutoDisableScript : MonoBehaviour
{
    public float TimeOut;
    private float remainingTime;

	// Use this for initialization
	void OnEnable ()
	{
	    remainingTime = TimeOut;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (remainingTime < 0) gameObject.SetActive(false);
	    else remainingTime -= Time.deltaTime;
	}
}
