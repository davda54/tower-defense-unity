using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float MaxHealth;
    public int Money;

    private float health;

    private void OnEnable()
    {
        health = MaxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "finish")
        {
            GameManager.Instance.EnemyEscaped(gameObject);
        }

        else if(collision.tag == "bullet")
        {
            var damage = collision.gameObject.GetComponent<BulletScript>().Damage;
            health -= damage;
            Pool.Instance.DeactivateObject(collision.gameObject);
            
            if(health <= 0)
            {
                GameManager.Instance.EnemyKilled(gameObject);
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
