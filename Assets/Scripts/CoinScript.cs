using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public const int Value = 10;
    private float randomNumber;

	// Use this for initialization
	void Start ()
    {
        randomNumber = Random.value * 5;
	}
	
	// Update is called once per frame
	void Update ()
    {
        var scale = 1.0f + 0.2f * Mathf.Sin(5*Time.realtimeSinceStartup + randomNumber);
        transform.localScale = new Vector3(scale, scale, 1.0f);
	}

    void OnMouseOver()
    {
        GameManager.Instance.CoinCollected(gameObject);
        Pool.Instance.DeactivateObject(gameObject);

        Pool.Instance.ActivateObject("coinSoundEffect").SetActive(true);
    }
}
