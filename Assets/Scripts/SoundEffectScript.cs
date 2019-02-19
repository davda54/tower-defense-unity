using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectScript : MonoBehaviour
{
    private AudioSource audio;

	void Awake ()
	{
	    audio = GetComponent<AudioSource>();
	}

    void OnEnable()
    {
        audio.Play();
    }
	
	void Update ()
	{
	    if (!audio.isPlaying)
	    {
            Pool.Instance.DeactivateObject(gameObject);
	    }
	}
}
