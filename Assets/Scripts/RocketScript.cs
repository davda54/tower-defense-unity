using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : FlyingShotScript
{
    public float Inertia;
    public float InitialAcceleration;
    public Vector2 ShadowOffset;

    private Vector2 velocity;
    private float acceleration;
    private Transform shadow;

    private float startTime;

	// Use this for initialization
	void OnEnable ()
    {
        Pool.Instance.ActivateObject("missileSoundEffect").SetActive(true);

        shadow = transform.Find("Shadow");
        shadow.position = transform.position;
        acceleration = InitialAcceleration;
        startTime = 1.0f;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (startTime >= 0)
        {
            startTime -= Time.deltaTime;
            transform.rotation = Turret.rotation;
            Direction = Turret.up;
            velocity = Direction.normalized;
            return;
        }

        if (Target == null || !Target.activeSelf)
        {
            Target = EnemyManagerScript.Instance.GetClosestEnemyInRange(transform.position, float.PositiveInfinity, EnemyTags);
            if (Target == null) BlowUp();
        }

        var direction = Target.transform.position - transform.position;
        var angle = MathHelpers.Angle(direction, transform.up) * Time.deltaTime * Inertia * Mathf.Pow(velocity.sqrMagnitude, 0.5f);

        velocity = velocity.Rotate(angle);
        acceleration *= 0.95f;
        velocity += velocity * acceleration * Time.deltaTime;
        
        transform.position += (Vector3)velocity;
        transform.up = velocity;

        shadow.position = transform.position + (Vector3)ShadowOffset * (InitialAcceleration - acceleration) / InitialAcceleration;
        shadow.rotation = transform.rotation;
    }

    public override void BlowUp()
    {
        Pool.Instance.ActivateObject("shortExplosionSoundEffect").SetActive(true);
        base.BlowUp();
    }
}
