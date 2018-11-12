using UnityEngine;
using Assets.Scripts;

public class BulletScript : MonoBehaviour
{
    [System.NonSerialized]
    public float Speed;
    [System.NonSerialized]
    public Vector2 Direction;
    [System.NonSerialized]
    public float Range;
    [System.NonSerialized]
    public float Damage;

    private float distance;
    
	// Use this for initialization
	void OnEnable ()
    {
        distance = 0.0f;
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
