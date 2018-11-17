using Assets.Scripts;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public GameObject BulletPrototype;
    public float ShootingPeriod;
    public float Range;
    public float BulletSpeed;
    public float Damage;
    public float RotationSpeed;

    private float timeToShoot = 0.0f;

	void Start ()
    {
        var enemy = EnemyManagerScript.Instance.GetEnemyInRange(transform.position, float.PositiveInfinity);
        var angle = MathHelpers.Angle(enemy.transform.position - transform.position, transform.up);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void FixedUpdate ()
    {
        var enemy = EnemyManagerScript.Instance.GetEnemyInRange(transform.position, Range);

        if (enemy != null)
        {
            TurnToEnemy(enemy.transform.position + enemy.transform.right * 32);

            if (timeToShoot < 0)
            {
                var bullet = Pool.Instance.ActivateObject(BulletPrototype.tag);
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;

                var bulletScript = bullet.GetComponent<FlyingShotScript>();
                bulletScript.Speed = BulletSpeed;
                bulletScript.Range = Range;
                bulletScript.Direction = transform.transform.up;
                bulletScript.Damage = Damage;
                bulletScript.Target = enemy;

                bullet.SetActive(true);

                timeToShoot = ShootingPeriod;
                return;
            }
        }
        else
        {
            var closestEnemy = EnemyManagerScript.Instance.GetClosestEnemyInRange(transform.position, Range*2);
            if (closestEnemy != null) TurnToEnemy(closestEnemy.transform.position + closestEnemy.transform.right * 32);
        }

        timeToShoot -= Time.deltaTime;
    }

    private void TurnToEnemy(Vector2 position)
    {
        var direction = position - (Vector2)transform.position;
        var angle = MathHelpers.Angle(direction, transform.up);
        transform.Rotate(0, 0, Mathf.Clamp(angle, -10, 10) * RotationSpeed * Time.deltaTime);
    }
}
