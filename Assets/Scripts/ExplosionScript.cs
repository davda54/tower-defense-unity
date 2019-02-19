using UnityEngine;

public class ExplosionScript : MonoBehaviour {

    private ParticleSystem particleSystem;

    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        particleSystem.Play();
    }

    void Update()
    {
        if (!particleSystem.isPlaying)
        {
            Pool.Instance.DeactivateObject(gameObject);
        }
    }
}
