using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public GameObject BulletPrototype;
    public float ShootingPeriod;
    public float Range;
    public float BulletSpeed;
    public float Damage;

    private float timeToShoot = 0.0f;

	void Start ()
    {
        Pool.Instance.RegisterObject(BulletPrototype);
	}
	
	void FixedUpdate ()
    {
        var enemy = EnemyManagerScript.Instance.GetEnemyInRange(transform.position, Range);

        if (enemy != null)
        {
            var direction = enemy.transform.position - transform.position;
            transform.eulerAngles = new Vector3(0, 0, MathHelpers.Angle(direction, Vector2.up));

            if (timeToShoot < 0)
            {
                var bullet = Pool.Instance.ActivateObject(BulletPrototype.tag);
                bullet.transform.position = transform.position;
                bullet.transform.eulerAngles = new Vector3(0, 0, MathHelpers.Angle(direction, Vector2.up));

                var bulletScript = bullet.GetComponent<BulletScript>();
                bulletScript.Speed = BulletSpeed;
                bulletScript.Range = Range;
                bulletScript.Direction = (enemy.transform.position - transform.position).normalized;
                bulletScript.Damage = Damage;

                bullet.SetActive(true);

                timeToShoot = ShootingPeriod;
                return;
            }
        }

        timeToShoot -= Time.deltaTime;
	}
}
