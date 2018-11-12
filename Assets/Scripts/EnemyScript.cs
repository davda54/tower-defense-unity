using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float MaxHealth;

    private float health;

    private void OnEnable()
    {
        health = MaxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "finish")
        {
        }
        else if(collision.tag == "bullet")
        {
            var damage = collision.gameObject.GetComponent<BulletScript>().Damage;
            health -= damage;
            Pool.Instance.DeactivateObject(collision.gameObject);
            
            if(health <= 0)
            {
                Pool.Instance.DeactivateObject(gameObject);
                EnemyManagerScript.Instance.DeleteEnemy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "finish")
        {
            EnemyManagerScript.Instance.DeleteEnemy(gameObject);
            Pool.Instance.DeactivateObject(gameObject);
        }
    }
}
