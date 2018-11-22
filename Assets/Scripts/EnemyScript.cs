using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public float MaxHealth;
    public int Money;
    public GameObject Coin;
    public float SpawnedCoinMean;
    public float SpawnedCoinStd;

    private Transform canvas;
    private Slider healthBar;
    private float health;

    private void OnEnable()
    {
        canvas = transform.Find("Canvas");
        healthBar = canvas.Find("HealthBar").GetComponent<Slider>();
        canvas.gameObject.SetActive(false);

        health = MaxHealth;
        healthBar.maxValue = MaxHealth;
        healthBar.value = health;
    }

    private void Update()
    {
        canvas.rotation = Quaternion.identity;
        canvas.localScale = Vector3.one * 0.5f;
    }

    private void SpawnCoins()
    {
        var num = (int)(MathHelpers.NextGaussianDouble() * SpawnedCoinStd + SpawnedCoinMean + 0.5f);

        for(int i = 0; i < num; i++)
        {
            var x = MathHelpers.NextGaussianDouble() * Mathf.Log(i + 1) * 4.0f;
            var y = MathHelpers.NextGaussianDouble() * Mathf.Log(i + 1) * 4.0f;

            var coin = Pool.Instance.ActivateObject(Coin.tag);
            coin.transform.position = transform.position + new Vector3(x, y, 0);
            coin.SetActive(true);
        }

        GameManager.Instance.AddMoney(Money);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("finish"))
        {
            GameManager.Instance.EnemyEscaped(gameObject);
        }

        else if((collision.CompareTag("bullet") && !CompareTag("plane"))|| (collision.CompareTag("rocket") && !CompareTag("soldier")))
        {
            var flyingShot = collision.gameObject.GetComponent<FlyingShotScript>();
            var damage = flyingShot.Damage;
            health -= damage;
            healthBar.value = health;
            canvas.gameObject.SetActive(true);
            flyingShot.BlowUp();
            
            if(health <= 0)
            {
                SpawnCoins();
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
