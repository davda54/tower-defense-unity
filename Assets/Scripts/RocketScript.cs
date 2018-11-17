using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : FlyingShotScript
{
    public float Inertia;
    public float InitialAcceleration;

    private Vector2 velocity;
    private float acceleration;

	// Use this for initialization
	void OnEnable ()
    {
        velocity = Direction.normalized;
        acceleration = InitialAcceleration;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(Target == null || !Target.activeSelf)
        {
            Target = EnemyManagerScript.Instance.GetClosestEnemyInRange(transform.position, float.PositiveInfinity);
            if (Target == null) BlowUp();
        }

        var direction = Target.transform.position - transform.position;
        var angle = MathHelpers.Angle(direction, transform.up) * Time.deltaTime * Inertia * Mathf.Pow(velocity.sqrMagnitude, 0.5f);

        velocity = velocity.Rotate(angle);
        acceleration *= 0.95f;
        velocity += velocity * acceleration * Time.deltaTime;
        
        transform.position += (Vector3)velocity;
        transform.up = velocity;
	}
}
